using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

using Unity.TestRunnerManualTests;

public class DeviceTestManager : MonoBehaviour
{
    public enum FeatureUsageTestType
    {
        Controls,
        Tracking
    }

    public FeatureUsageTestType testType;

    public ContentResize deviceListContent;
    public ScrollRect deviceScrollRect;
    public GameObject deviceListUIElement;
    public Text deviceNameText;
    public ContentResize controlsListContent;
    public ScrollRect controlsScrollRect;
    public GameObject controlsListUIElement;
    public ContentResize testsListContent;
    public ScrollRect testsScrollRect;
    public GameObject testsListUIElement;
    public ArbiterFeatureUsageDrivesUI arbiterUsageUI;
    public Text controlUnderTestName;
    public Text controlUnderTestType;
    public Text testDescriptionBox;
    public GameObject XRRig;
    public Bearings bearings;
    public GameObject TrackedDeviceVisualizerPositionPrefab;
    public GameObject TrackedDeviceVisualizerRotationPrefab;
    public GameObject TrackedDeviceVisualizerPointerPrefab;
    public GameObject TrackedDeviceVisualizerEyePointerPrefab;
    public GameObject EyeHelper;
    public GameObject FixationPointVisualizer;
    public GraphFromVector3FeatureUsage graphVector3;
    public QuaternionDrivesTextMesh rotationTextOnFace;

    private List<DeviceContainer> m_InputDeviceList;
    private int m_CurrentDeviceIndex;
    private InputDevice m_CurrentDevice {get { return m_InputDeviceList[m_CurrentDeviceIndex].Device; } }

    private List<InputFeatureUsageContainer> m_InputFeatureUsageList;
    private int m_CurrentFeatureIndex;
    public InputFeatureUsage CurrentInputFeatureUsage {get { return m_InputFeatureUsageList[m_CurrentFeatureIndex].FeatureUsage; } }

    private List<ControlTest> m_ControlTestList;
    private int m_CurrentTestIndex;
    private ControlTest m_CurrentControlTest {get { return m_ControlTestList[m_CurrentTestIndex]; } }

    private bool m_StartTestsCalled = false;
    private bool m_RunningTests = false;

    // This method is launched by the TestFacilitator broadcasting a message
    public void StartTests()
    {
        m_InputDeviceList = new List<DeviceContainer>();
        m_InputFeatureUsageList = new List<InputFeatureUsageContainer>();

        RefreshDeviceList();
        m_StartTestsCalled = true;

        if (graphVector3 != null)
            graphVector3.gameObject.SetActive(false);

        if (rotationTextOnFace != false)
            rotationTextOnFace.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (m_RunningTests)
        {
            bool testFinished = m_CurrentControlTest.RunChecks();
            UpdateTestDescription();
            if (testFinished)
                NextTest();
        }
        else if (m_StartTestsCalled)
        {
            bool allPassed = true;
            for (int i = 0; i < m_InputDeviceList.Count; i++)
            {
                if (m_InputDeviceList[i].NoTestsToRun)
                    Debug.Log("Device " + m_InputDeviceList[i].Device.name + " had no tests to run.");
                if (!m_InputDeviceList[i].HaveAllTestsPassed && !m_InputDeviceList[i].NoTestsToRun)
                    allPassed = false;
            }

            if (allPassed)
                GetComponent<FeatureUsageTestFacilitator>().TestFinish(TestFacilitator.OverallTestStatus.Passed, "All control tests on all devices tests report as \"Passed\".");
            else
                GetComponent<FeatureUsageTestFacilitator>().TestFinish(TestFacilitator.OverallTestStatus.Failed, "One or more control tests on one or more devices were skipped or report as failure.  View the log for more information.");

        }
    }

    private void NextTest()
    {
        // Update test status
        Debug.Log("Finished test " + m_CurrentControlTest.GetType().ToString());
        m_CurrentControlTest.Teardown();
        m_CurrentControlTest.UIManager.SetStatusTested(m_CurrentControlTest.AllChecksPassed());
        if (m_CurrentTestIndex + 1 < m_ControlTestList.Count)
        {
            ReadyTest(m_ControlTestList, m_CurrentTestIndex + 1);
        }
        else
        {
            NextUsage();
        }
    }

    public void SkipToNextUsage()
    {
        Debug.Log("\n----------------------------------\nManually skipping to next usage.\n----------------------------------");
        m_CurrentControlTest.Teardown();
        NextUsage();
    }

    private void NextUsage()
    {
        // Update control status
        Debug.Log("\n----------------------------------\nFinished running tests for usage " + CurrentInputFeatureUsage.name + "\n----------------------------------");

        bool allPassed = true;
        for (int i = 0; i < m_ControlTestList.Count; i++)
        {
            if (!m_ControlTestList[i].AllChecksPassed())
                allPassed = false;
        }
        m_InputFeatureUsageList[m_CurrentFeatureIndex].HaveAllTestsPassed = allPassed;
        m_InputFeatureUsageList[m_CurrentFeatureIndex].UIManager.SetStatusTested(allPassed);

        if (!ReadyNextTestableFeature(m_CurrentDevice))
        {
            NextDevice();
        }
    }

    public void SkipToNextDevice()
    {
        Debug.Log("\n----------------------------------\nManually skipping to next device.\n----------------------------------");
        NextDevice();
    }

    private void NextDevice()
    {
        // Update device status
        Debug.Log("\n\n==================================\nFinished testing device " + m_CurrentDevice.name + "\n==================================\n\n");

        bool allControlTestsPassed = true;
        for (int i = 0; i < m_InputFeatureUsageList.Count; i++)
        {
            if (!m_InputFeatureUsageList[i].HaveAllTestsPassed)
                allControlTestsPassed = false;
        }
        m_InputDeviceList[m_CurrentDeviceIndex].HaveAllTestsPassed = allControlTestsPassed;
        m_InputDeviceList[m_CurrentDeviceIndex].UIManager.SetStatusTested(allControlTestsPassed);

        if (!ReadyNextTestableDevice())
        {
            MarkTestsAsFinished();
        }
    }

    void MarkTestsAsFinished()
    {
        controlsListContent.ClearContentItems();
        testsListContent.ClearContentItems();
        m_RunningTests = false;
    }

    void RefreshDeviceList()
    {
        Debug.Log("Refreshing Device List");

        List<InputDevice> tempDeviceList = new List<InputDevice>();
        InputDevices.GetDevices(tempDeviceList);
        m_InputDeviceList.Clear();
        deviceListContent.ClearContentItems();


        for (int i = 0; i < tempDeviceList.Count; i++) {
            m_InputDeviceList.Add(new DeviceContainer(tempDeviceList[i]));
        }

        Debug.Log(m_InputDeviceList.Count + " devices");

        for (int i = 0; i < m_InputDeviceList.Count; i++)
            Debug.Log("Device " + i + ": " + m_InputDeviceList[i].Device.name);

        if (tempDeviceList.Count == 0)
            return;

        for (int i = 0; i < m_InputDeviceList.Count; i++)
        {
            GameObject tempItem = Instantiate(deviceListUIElement);
            tempItem.GetComponent<DeviceItemUIManager>().SetDeviceName(m_InputDeviceList[i].Device.name);
            m_InputDeviceList[i].UIManager = tempItem.GetComponent<DeviceItemUIManager>();
            deviceListContent.AddContentItem(tempItem);
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
            m_CurrentDeviceIndex = -1;

        int NextDeviceIndex = -1;

        //Debug.Log(InputDeviceList.Count + " devices");

        // Cycle through the devices 
        for (int i = Mathf.Max(0, m_CurrentDeviceIndex + 1); i < m_InputDeviceList.Count; i++)
        {
            if (!m_InputDeviceList[i].Device.TryGetFeatureUsages(tempInputFeatureUsages))
                continue;

            for (int j = 0; j < tempInputFeatureUsages.Count; j++) {
                // If we find a single testable feature - hooray!  Start testing at that feature
                if (ShouldAddFeature(tempInputFeatureUsages[j]))
                    ControlToTestLookup.LookupControlTests(m_InputDeviceList[i].Device, tempInputFeatureUsages[j], out tempControlTestList);

                if (tempControlTestList.Count > 0)
                {
                    NextDeviceIndex = i;
                    break;
                }
            }

            if (NextDeviceIndex != -1)
                break;
            else
                m_InputDeviceList[i].NoTestsToRun = true;
        }

        if (NextDeviceIndex == -1)
            return false;
        
        m_CurrentDeviceIndex = NextDeviceIndex;
        m_InputDeviceList[m_CurrentDeviceIndex].UIManager.SetStatusInProgress();
        deviceScrollRect.verticalNormalizedPosition = 1f - ((float)(m_CurrentDeviceIndex) / (float)m_InputDeviceList.Count);
        
        // List out the Feature Usages on this device
        if (m_CurrentDeviceIndex != -1 && m_InputDeviceList.Count != 0 && m_CurrentDevice.TryGetFeatureUsages(tempInputFeatureUsages))
        {
            m_InputFeatureUsageList.Clear();
            for (int i = 0; i < tempInputFeatureUsages.Count; i++)
                if (ShouldAddFeature(tempInputFeatureUsages[i]))
                    m_InputFeatureUsageList.Add(new InputFeatureUsageContainer(tempInputFeatureUsages[i]));

            deviceNameText.text = m_InputDeviceList[m_CurrentDeviceIndex].Device.name;
            controlsListContent.ClearContentItems();

            for (int i = 0; i < m_InputFeatureUsageList.Count; i++)
            {
                GameObject tempItem = Instantiate(controlsListUIElement);
                tempItem.GetComponent<ControlItemUIManager>().SetFeatureName(m_InputFeatureUsageList[i].FeatureUsage.name);
                tempItem.GetComponent<ControlItemUIManager>().SetUsageName(m_InputFeatureUsageList[i].FeatureUsage.type.Name);
                m_InputFeatureUsageList[i].UIManager = tempItem.GetComponent<ControlItemUIManager>();
                controlsListContent.AddContentItem(tempItem);
            }
        }

        m_CurrentFeatureIndex = -1;
        return ReadyNextTestableFeature(m_CurrentDevice);
    }

    // Return true if there is a testable feature, false otherwise.
    // if startAtFirstFeature is not set true, then the search for a testable feature will begin at CurrentFeatureIndex + 1
    private bool ReadyNextTestableFeature(InputDevice device, bool startAtFirstFeature = false)
    {
        List<ControlTest> tempControlTestList = new List<ControlTest>();

        if (startAtFirstFeature)
            m_CurrentFeatureIndex = -1;

        int NextFeatureIndex = -1;

        for (int i = Mathf.Max(0, m_CurrentFeatureIndex + 1); i < m_InputFeatureUsageList.Count; i++)
        {
            if (ShouldAddFeature(m_InputFeatureUsageList[i].FeatureUsage))
                ControlToTestLookup.LookupControlTests(m_CurrentDevice, m_InputFeatureUsageList[i].FeatureUsage, out tempControlTestList);
            
            if (tempControlTestList.Count > 0)
            {
                NextFeatureIndex = i;
                break;
            }
        }

        if (NextFeatureIndex == -1)
            return false;
        
        m_CurrentFeatureIndex = NextFeatureIndex;
        m_InputFeatureUsageList[m_CurrentFeatureIndex].UIManager.SetStatusInProgress();
        controlsScrollRect.verticalNormalizedPosition = 1f - ((float)(m_CurrentFeatureIndex) / (float)m_InputFeatureUsageList.Count);

        testsListContent.ClearContentItems();

        if (ShouldAddFeature(CurrentInputFeatureUsage))
            ControlToTestLookup.LookupControlTests(device, CurrentInputFeatureUsage, out m_ControlTestList);

        if (m_ControlTestList.Count == 0)
            return false;

        // Yes, we have a testable feature
        arbiterUsageUI.SetDrivingUsage(device, CurrentInputFeatureUsage);

        for (int i = 0; i < m_ControlTestList.Count; i++)
        {
            GameObject tempItem = Instantiate(testsListUIElement);
            tempItem.GetComponent<TestItemUIManager>().SetTestName(m_ControlTestList[i].GetType().ToString());
            m_ControlTestList[i].UIManager = tempItem.GetComponent<TestItemUIManager>();
            testsListContent.AddContentItem(tempItem);
        }

        // Have the tests, set up to cycle through them!
        controlUnderTestName.text = CurrentInputFeatureUsage.name;
        controlUnderTestType.text = CurrentInputFeatureUsage.type.ToString();
        ReadyTest(m_ControlTestList, 0);
        m_RunningTests = true;

        return true;
    }

    private void ReadyTest(List<ControlTest> tests, int index)
    {
        m_CurrentTestIndex = index;
        m_ControlTestList[m_CurrentTestIndex].UIManager.SetStatusInProgress();
        ControlTest currentTest = tests[index];

        m_CurrentControlTest.Setup();
        UpdateTestDescription();
        testsScrollRect.verticalNormalizedPosition = 1f - ((float)(m_CurrentTestIndex) / (float)tests.Count);

        Debug.Log("\nStarting to test device: " + m_CurrentDevice.name + ", usage: " + CurrentInputFeatureUsage.name + ", test: " + m_CurrentControlTest.GetType());
    }

    private void UpdateTestDescription() 
    {
        testDescriptionBox.text = m_CurrentControlTest.GetPrintableDescription();
    }

    public void ManuallyApproveCurrentTest()
    {
        if (!m_RunningTests)
            return;

        Debug.Log("ManualPass on test " + m_CurrentControlTest.GetType().ToString());
        m_CurrentControlTest.ManualPass();
        NextTest();
    }

    public void SkipCurrentTest()
    {
        if (!m_RunningTests)
            return;

        Debug.Log("Skip on test " + m_CurrentControlTest.GetType().ToString());
        m_CurrentControlTest.Skip();
        NextTest();
    }

    private bool IsFeatureUsageForTracking(InputFeatureUsage usage)
    {
        switch (usage.name)
        {
            case "IsTracked":
            case "TrackingState":
            
            case "DevicePosition":
            case "ColorCameraPosition":
            case "LeftEyePosition":
            case "RightEyePosition":
            case "CenterEyePosition":
            
            case "DeviceRotation":
            case "ColorCameraRotation":
            case "LeftEyeRotation":
            case "RightEyeRotation":
            case "CenterEyeRotation":
            
            case "DeviceVelocity":
            case "ColorCameraVelocity":
            case "LeftEyeVelocity":
            case "RightEyeVelocity":
            case "CenterEyeVelocity":
            
            case "DeviceAngularVelocity":
            case "ColorCameraAngularVelocity":
            case "LeftEyeAngularVelocity":
            case "RightEyeAngularVelocity":
            case "CenterEyeAngularVelocity":
            
            case "DeviceAcceleration":
            case "ColorCameraAcceleration":
            case "LeftEyeAcceleration":
            case "RightEyeAcceleration":
            case "CenterEyeAcceleration":
            
            case "DeviceAngularAcceleration":
            case "ColorCameraAngularAcceleration":
            case "LeftEyeAngularAcceleration":
            case "RightEyeAngularAcceleration":
            case "CenterEyeAngularAcceleration":

                return true;

            default:
                return false;
        }
    }

    private bool ShouldAddFeature(InputFeatureUsage usage)
    {
        return (testType == FeatureUsageTestType.Tracking && IsFeatureUsageForTracking(usage)) ||
            (testType == FeatureUsageTestType.Controls && !IsFeatureUsageForTracking(usage));
    }
}
