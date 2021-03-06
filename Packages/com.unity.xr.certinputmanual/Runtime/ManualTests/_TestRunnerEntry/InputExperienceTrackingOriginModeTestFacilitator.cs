﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.TestRunnerManualTests;

public class InputExperienceTrackingOriginModeTestFacilitator : XRBaseTestFacilitator
{
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

        instructionCanvas.Instructions.text = "For each input subsystem:" +
            "\n - Verify that a the current TrackingOriginMode is in the list of available tracking modes." +
            "\n - Verify that \"Unknown\" does not appear as the current TrackingOriginMode or in the list of available TrackingOriginModes." +
            "\n - Verify that you can switch to any of the available tracking modes." +
            "\n - Verify that you can NOT switch to any tracking mode that is not listed as available." +
            "\n - Verify that each input subsystem that should have a boundary does. Boundary start points are labeled with its subsystem number and must be enumerated clockwise (from above) and in sequence." +
            "\n - For each supported TrackingOriginMode, try recenter.  If it changes your position or orientation, verify that the bounds are still correct and that both OnBoundaryChanged and TrackingOriginUpdated fire.  If the subsystem under test allows you to recenter through it, verify this line using both that action and through the recenter buttons corresponding to that subsystem." + 
            "\n - If applicable, verify that changing the boundary in the runtime under test while running this test will trigger OnBoundaryChanged." + 
            "\n\nActivate \"Continue\" when finished";

        ShowSubsystems();
        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "In Device TrackingOriginMode, activating Recenter corrects your position and yaw." +
                    "\nFor each input subsystem that supports Device TrackingOriginMode, do the following:" +
                    "\n- Set the TrackingOriginMode to Device." +
                    "\n- Move away from your starting position if possible." +
                    "\n- Look away from +Z" +
                    "\n- Recenter" +
                    "\n- Verify that your position is reset and your \"forward\" direction aligns with +Z." +
                    "\n\nThen verify that nothing occurs if you try to recenter in other supported TrackingOriginModes";

        yield return WaitForContinue();

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All haptic tests have been manually approved");
    }

    void ShowSubsystems()
    {
        // Do stuff
        BroadcastMessage("ClearArrayOfSubsystems");
        BroadcastMessage("FillArrayOfSubsystems");
    }
}
