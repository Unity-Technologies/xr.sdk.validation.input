﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.TestRunnerManualTests;

public class InputArrayTestFacilitator : TestFacilitator
{
    List<InputDevice> m_Devices;
    int m_DeviceIndex = 0;

    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "For each device:" +
            "\n - Verify that every hardware sensor or control corresponds to a feature in the CommonUsages table." +
            "\n - Verify that if a feature resembles a usage on the XR SDK Input Usage Table, it is implemented as that feature." + 
            "\n - Verify that verify that each each hardware sensor or control changes only its expected usage(s). For example, a trigger should drive Trigger and TriggerButton, but not PrimaryButton." +
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