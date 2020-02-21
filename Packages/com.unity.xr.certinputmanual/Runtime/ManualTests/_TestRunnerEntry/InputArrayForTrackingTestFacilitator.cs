using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.TestRunnerManualTests;

public class InputArrayForTrackingTestFacilitator : XRBaseTestFacilitator
{
    List<InputDevice> m_Devices;
    int m_DeviceIndex = 0;

    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    protected override IEnumerator RunTest()
    {
        List<InputDevice> m_Devices = new List<InputDevice>();
        InputDevices.GetDevices(m_Devices);
        if (m_Devices.Count == 0)
        {
            RecordStatus(OverallTestStatus.Failed, "No devices detected.  Test failing due to invalid setup.");
            yield break;
        }

        instructionCanvas.Instructions.text = "For each device, and each tracking property (position, rotation, velocity, angular velocity, acceleration, and angular acceleration):" +
            "\n - If the tracking property is marked as NOT tracking, its value must default to (0, 0, 0) for Vector3 type usages and (0, 0, 0, 1) for Quaternion usages." + 
            "\n - If the tracking property is marked as YES tracking, verify that its values are update when you move the device." +
            "\n\nActivate \"Continue\" to cycle through devices.";

        m_Devices = new List<InputDevice>();
        InputDevices.GetDevices(m_Devices);
        Debug.Log(m_Devices.Count + " devices detected");

        while (m_DeviceIndex < m_Devices.Count)
        {
            Debug.Log("Testing device [" + m_DeviceIndex + "] " + m_Devices[m_DeviceIndex].name);
            DisplayNextDevice(m_Devices[m_DeviceIndex]);
            yield return WaitForContinue();
        }

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All haptic tests have been manually approved");
    }

    void DisplayNextDevice(InputDevice device)
    {
        // Do stuff
        BroadcastMessage("ClearArrayOfControls");
        BroadcastMessage("FillArrayOfControls", device);

        // Do this last
        m_DeviceIndex++;
    }
}
