using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

public class DeviceTestManager : MonoBehaviour
{
    public ContentResize DeviceListContent;
    public GameObject DeviceListUIElement;
    public Text DeviceNameText;
    public ContentResize ControlsListContent;
    public GameObject ControlsListUIElement;
    public ContentResize TestsListContent;
    public GameObject TestsListUIElement;
    public ArbiterFeatureUsageDrivesUI ArbiterUsageUI;
    public Text ControlUnderTestName;
    public Text ControlUnderTestType;
    public Text TestDescriptionBox;

    [Tooltip("unit time is seconds")]
    public float DeviceRefreshRate = 3.0f;

    private List<DeviceContainer> InputDeviceList;
    private int CurrentDeviceIndex;
    private InputDevice CurrentDevice {get { return InputDeviceList[CurrentDeviceIndex].Device; } }

    private List<InputFeatureUsageContainer> InputFeatureUsageList;
    private int CurrentFeatureIndex;
    private InputFeatureUsage CurrentInputFeatureUsage {get { return InputFeatureUsageList[CurrentFeatureIndex].FeatureUsage; } }

    private List<ControlTest> ControlTestList;
    private int CurrentTestIndex;
    private ControlTest CurrentControlTest {get { return ControlTestList[CurrentTestIndex]; } }

    private bool RunningTests = false;

    // Start is called before the first frame update
    private void Start()
    {
        InputDeviceList = new List<DeviceContainer>();
        InputFeatureUsageList = new List<InputFeatureUsageContainer>();

        RefreshDeviceList();

        // TODO
        //InputTracking.nodeAdded += HandleNodeAdded();
        //InputTracking.nodeRemoved += HandleNodeRemoved();
    }

    private void Update()
    {
        if (RunningTests)
        {
            bool testFinished = CurrentControlTest.RunChecks();
            UpdateTestDescription();
            if (testFinished)
                NextTest();
        }
    }

    private void NextTest()
    {
        // Update test status
        Debug.Log("Finished test " + CurrentControlTest.GetType().ToString());
        CurrentControlTest.UIManager.SetStatusTested(CurrentControlTest.AllChecksPassed());
        if (CurrentTestIndex + 1 < ControlTestList.Count)
        {
            ReadyTest(ControlTestList, CurrentTestIndex + 1);
        }
        else
        {
            // Update control status
            Debug.Log("Finished running tests for usage " + CurrentInputFeatureUsage.name);

            bool allPassed = true;
            for (int i = 0; i < ControlTestList.Count; i++)
            {
                if (!ControlTestList[i].AllChecksPassed())
                    allPassed = false;
            }
            InputFeatureUsageList[CurrentFeatureIndex].HaveAllTestsPassed = allPassed;
            InputFeatureUsageList[CurrentFeatureIndex].UIManager.SetStatusTested(allPassed);

            if (!ReadyNextTestableFeature(CurrentDevice))
            {
                // Update device status
                Debug.Log("Finished testing device " + CurrentDevice.name);

                bool allControlTestsPassed = true;
                for (int i = 0; i < InputFeatureUsageList.Count; i++)
                {
                    if (!InputFeatureUsageList[i].HaveAllTestsPassed)
                        allControlTestsPassed = false;
                }

                InputDeviceList[CurrentDeviceIndex].UIManager.SetStatusTested(allControlTestsPassed);

                if (!ReadyNextTestableDevice())
                {
                    MarkTestsFinished();
                }
            }
        }
    }

    public void MarkTestsFinished()
    {
        ControlsListContent.ClearContentItems();
        TestsListContent.ClearContentItems();
        RunningTests = false;
    }

    public void RefreshDeviceList()
    {
        Debug.Log("Refreshing Device List");

        List<InputDevice> tempDeviceList = new List<InputDevice>();
        InputDevices.GetDevices(tempDeviceList);
        InputDeviceList.Clear();
        DeviceListContent.ClearContentItems();


        for (int i = 0; i < tempDeviceList.Count; i++) {
            InputDeviceList.Add(new DeviceContainer(tempDeviceList[i]));
        }

        Debug.Log(InputDeviceList.Count + " devices");

        if (tempDeviceList.Count == 0)
            return;

        for (int i = 0; i < InputDeviceList.Count; i++)
        {
            GameObject tempItem = Instantiate(DeviceListUIElement);
            tempItem.GetComponent<DeviceItemUIManager>().SetDeviceName(InputDeviceList[i].Device.name);
            InputDeviceList[i].UIManager = tempItem.GetComponent<DeviceItemUIManager>();
            DeviceListContent.AddContentItem(tempItem);
        }

        ReadyNextTestableDevice(true);
    }
    
    // Return true if there is a testable device, false otherwise.
    // if startsAtFirstDevice is not set true, then the search for a testable feature will begin at CurrentDeviceIndex + 1
    private bool ReadyNextTestableDevice(bool startAtFirstDevice = false)
    {
        List<InputFeatureUsage> tempInputFeatureUsages = new List<InputFeatureUsage>();
        List<ControlTest> tempControlTestList = new List<ControlTest>();

        if (startAtFirstDevice)
            CurrentDeviceIndex = -1;

        int NextDeviceIndex = -1;

        //Debug.Log(InputDeviceList.Count + " devices");

        // Cycle through the devices 
        for (int i = Mathf.Max(0, CurrentDeviceIndex + 1); i < InputDeviceList.Count; i++)
        {
            if (!InputDeviceList[i].Device.TryGetFeatureUsages(tempInputFeatureUsages))
                continue;

            for (int j = 0; j < tempInputFeatureUsages.Count; j++) {
                // If we find a single testable feature - hooray!  just put it up for now
                // TODO I'll come back and make this selectable instead later
                //Debug.Log(InputDeviceList[i].name + " - " + InputFeatureUsageList[j].name);
                ControlToTestLookup.LookupControlTests(InputDeviceList[i].Device, tempInputFeatureUsages[j], out tempControlTestList);
                if (tempControlTestList.Count > 0)
                {
                    NextDeviceIndex = i;
                    break;
                }
            }

            if (NextDeviceIndex != -1)
                break;
        }

        if (NextDeviceIndex == -1)
            return false;
        
        CurrentDeviceIndex = NextDeviceIndex;
        InputDeviceList[CurrentDeviceIndex].UIManager.SetStatusInProgress();
        
        // List out the Feature Usages on this device
        if (CurrentDeviceIndex != -1 && InputDeviceList.Count != 0 && CurrentDevice.TryGetFeatureUsages(tempInputFeatureUsages))
        {
            InputFeatureUsageList.Clear();
            for (int i = 0; i < tempInputFeatureUsages.Count; i++)
                InputFeatureUsageList.Add(new InputFeatureUsageContainer(tempInputFeatureUsages[i]));

            DeviceNameText.text = InputDeviceList[CurrentDeviceIndex].Device.name;
            ControlsListContent.ClearContentItems();

            for (int i = 0; i < InputFeatureUsageList.Count; i++)
            {
                GameObject tempItem = Instantiate(ControlsListUIElement);
                tempItem.GetComponent<ControlItemUIManager>().SetFeatureName(InputFeatureUsageList[i].FeatureUsage.name);
                tempItem.GetComponent<ControlItemUIManager>().SetUsageName(InputFeatureUsageList[i].FeatureUsage.type.Name);
                InputFeatureUsageList[i].UIManager = tempItem.GetComponent<ControlItemUIManager>();
                ControlsListContent.AddContentItem(tempItem);
            }
        }

        CurrentFeatureIndex = -1;
        return ReadyNextTestableFeature(CurrentDevice);
    }

    // Return true if there is a testable feature, false otherwise.
    // if startAtFirstFeature is not set true, then the search for a testable feature will begin at CurrentFeatureIndex + 1
    private bool ReadyNextTestableFeature(InputDevice device, bool startAtFirstFeature = false)
    {
        List<ControlTest> tempControlTestList = new List<ControlTest>();

        if (startAtFirstFeature)
            CurrentFeatureIndex = -1;

        int NextFeatureIndex = -1;

        for (int i = Mathf.Max(0, CurrentFeatureIndex + 1); i < InputFeatureUsageList.Count; i++)
        {
            ControlToTestLookup.LookupControlTests(CurrentDevice, InputFeatureUsageList[i].FeatureUsage, out tempControlTestList);
            if (tempControlTestList.Count > 0)
            {
                NextFeatureIndex = i;
                break;
            }
        }

        if (NextFeatureIndex == -1)
            return false;
        
        CurrentFeatureIndex = NextFeatureIndex;
        InputFeatureUsageList[CurrentFeatureIndex].UIManager.SetStatusInProgress();
        TestsListContent.ClearContentItems();

        ControlToTestLookup.LookupControlTests(device, CurrentInputFeatureUsage, out ControlTestList);

        if (ControlTestList.Count == 0)
            return false;

        // Yes, we have a testable feature
        ArbiterUsageUI.SetDrivingUsage(device, CurrentInputFeatureUsage);

        for (int i = 0; i < ControlTestList.Count; i++)
        {
            GameObject tempItem = Instantiate(TestsListUIElement);
            tempItem.GetComponent<TestItemUIManager>().SetTestName(ControlTestList[i].GetType().ToString());
            ControlTestList[i].UIManager = tempItem.GetComponent<TestItemUIManager>();
            TestsListContent.AddContentItem(tempItem);
        }

        // Have the tests, set up to cycle through them!
        ControlUnderTestName.text = CurrentInputFeatureUsage.name;
        ControlUnderTestType.text = CurrentInputFeatureUsage.type.ToString();
        ReadyTest(ControlTestList, 0);
        RunningTests = true;

        return true;
    }

    private void ReadyTest(List<ControlTest> tests, int index)
    {
        CurrentTestIndex = index;
        ControlTestList[CurrentTestIndex].UIManager.SetStatusInProgress();
        ControlTest currentTest = tests[index];

        UpdateTestDescription();
    }

    private void UpdateTestDescription() 
    {
        TestDescriptionBox.text = CurrentControlTest.GetPrintableDescription();
    }

    public void ManuallyApproveCurrentTest()
    {
        if (!RunningTests)
            return;

        Debug.Log("ManualPass on test " + CurrentControlTest.GetType().ToString());
        CurrentControlTest.ManualPass();
        NextTest();
    }

    public void SkipCurrentTest()
    {
        if (!RunningTests)
            return;

        Debug.Log("Skip on test " + CurrentControlTest.GetType().ToString());
        CurrentControlTest.Skip();
        NextTest();
    }
}
