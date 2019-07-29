using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.TestRunnerManualTests;

public class InputHapticsTestFacilitator : TestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "If your device supports haptic impulse, verify that the intensity can be changed and the haptic strength reacts appropriately.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "If your device supports haptic impulse, verify that the duration can be changed and the time of the impulse is correct.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "If your device supports haptic impulse, verify that the Stop Haptics buttons stop an impulse.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "If your device supports buffered haptics, verify that the ramp up and ramp down effects feel correct.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "If your device supports buffered haptics, verify that the 1 second clip lasts one second.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "If your device supports buffered haptics, verify that the Stop Haptics buttons stop a buffered clip.  Then either fail or continue.";

        yield return WaitForContinue();

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All haptic tests have been manually approved");
    }
}
