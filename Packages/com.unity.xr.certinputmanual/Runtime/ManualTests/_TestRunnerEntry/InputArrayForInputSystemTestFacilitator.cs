﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.TestRunnerManualTests;

using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;

public class InputArrayForInputSystemTestFacilitator : XRBaseTestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    protected override IEnumerator RunTest()
    {
        // Set up InputSystem situation
        ReadOnlyArray<UnityEngine.InputSystem.InputDevice> ISDevices = InputSystem.devices;
        Debug.Log(ISDevices.Count + " InputSystem devices detected");

        // Set up InputDevice situation
        List<UnityEngine.XR.InputDevice> XRDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevices(XRDevices);
        Debug.Log(XRDevices.Count + " XR devices detected");

        if (XRDevices.Count == 0 || ISDevices.Count == 0)
        {
            RecordStatus(OverallTestStatus.Failed, "No devices detected.  Test failing due to invalid setup.");
            yield break;
        }

        instructionCanvas.Instructions.text = "For each device:" +
            "\n - If you encounter a generic device such as a mouse or keyboard, continue to the next device." + 
            "\n - The first panel to the right lists the InputSystem device controls. If a device with a matching name was found with the XR InputDevice API, verify that the two control lists match. If no matching device was found, or the control lists do not match, follow up to figure out why." + 
            "\n - Verify that every hardware sensor or control corresponds to a listed control." + 
            "\n - Verify that each hardware sensor or control changes only its expected usage(s). For example, a trigger should drive Trigger and TriggerButton, but not PrimaryButton." +
            "\n - Verify that the metadata for each control makes sense." +
            "\n\nActivate \"Continue\" to cycle through devices.";

        for (int i = 0; i < ISDevices.Count; i++)
        {
            Debug.Log("Testing InputSystem device [" + i + "] " + ISDevices[i].name);

            int matchingXRIndex = -1;
            for (int j = 0; j < XRDevices.Count; j++)
            {
                if (XRDevices[j].name == ISDevices[i].displayName)
                {
                    matchingXRIndex = j;
                    break;
                }
            }

            if (matchingXRIndex == -1)
                DisplayNextDevice(ISDevices[i], new UnityEngine.XR.InputDevice());
            else
                DisplayNextDevice(ISDevices[i], XRDevices[matchingXRIndex]);

            yield return WaitForContinue();
        }

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All Input Array tests have been manually approved");
    }

    void DisplayNextDevice(UnityEngine.InputSystem.InputDevice ISDevice, UnityEngine.XR.InputDevice XRDevice)
    {
        BroadcastMessage("ClearArrayOfControls");
        BroadcastMessage("FillArrayOfControls", XRDevice);
        BroadcastMessage("FillArrayOfControlsIS", ISDevice);
    }
}
