using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class InputExperienceRecenterTrackingOriginModeTestFacilitator : TestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "Activating Recenter corrects your position and yaw." +
                    "\nFor each input subsystem that supports Device TrackingOriginMode, do the following:" +
                    "\n- Move away from your starting position if possible." +
                    "\n- Look away from +Z" +
                    "\n- Recenter" +
                    "\n- Verify that your position is reset and your \"forward\" direction aligns with +Z." +
                    "\n\nThen verify that nothing occurs if you try to recenter in other supported TrackingOriginModes";

        ShowSubsystems();
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
