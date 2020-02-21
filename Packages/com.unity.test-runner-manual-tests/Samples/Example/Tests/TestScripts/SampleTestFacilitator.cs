using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.TestRunnerManualTests;

public class SampleTestFacilitator : TestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    protected override IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "For every test scene, follow the instructions on this instruction panel.\n\nActivate buttons by pressing the corresponding keyboard key (surrounded by brackets on each button).  If you are using an XR device, you may activate a button by centering your view on it until it activates.\n\nPress one of the control buttons on this panel to complete this test.";

        // This is an example of a manual checkpoint.
        yield return WaitForContinue();

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "Continue was successfully triggered");
    }
}
