using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class InputExperienceTrackingOriginModeTestFacilitator : TestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "For each input subsystem:" +
            "\n - Verify that a the current TrackingOriginMode is in the list of available tracking modes." +
            "\n - Verify that \"Unknown\" does not appear as the current TrackingOriginMode or in the list of available TrackingOriginModes." +
            "\n - Verify that you can switch to any of the available tracking modes." +
            "\n - Verify that you can NOT switch to any tracking mode that is not listed as available." +
            "\n - Verify that each input subsystem that should have a boundary does. Boundary start points are labeled with its subsystem number and must be enumerated clockwise and in sequence." + 
            "\n\nActivate \"Continue\" when finished";

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
