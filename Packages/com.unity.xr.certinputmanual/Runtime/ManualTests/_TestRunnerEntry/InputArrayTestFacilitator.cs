using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.TestRunnerManualTests;

public class InputArrayTestFacilitator : XRBaseTestFacilitator
{
    List<InputDevice> m_Devices;

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

        instructionCanvas.Instructions.text = "For each device:" +
            "\n - Verify that every hardware sensor or control corresponds to a feature in the CommonUsages table." +
            "\n - Verify that if a feature resembles a usage on the XR SDK Input Usage Table, it is implemented as that feature.  Non-common usages are highlighted red for your convenience. Names must match exactly, including case." + 
            "\n - Verify that verify that each hardware sensor or control changes only its expected usage(s). For example, a trigger should drive Trigger and TriggerButton, but not PrimaryButton." +
            "\n\nActivate \"Continue\" to cycle through devices.";

        m_Devices = new List<InputDevice>();
        InputDevices.GetDevices(m_Devices);
        Debug.Log(m_Devices.Count + " devices detected");

        for (int i = 0; i < m_Devices.Count; i++)
        {
            Debug.Log("Testing device [" + i + "] " + m_Devices[i].name);
            DisplayNextDevice(m_Devices[i]);
            yield return WaitForContinue();
        }

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All Input Array tests have been manually approved");
    }

    void DisplayNextDevice(InputDevice device)
    {
        BroadcastMessage("ClearArrayOfControls");
        BroadcastMessage("FillArrayOfControls", device);
    }
}
