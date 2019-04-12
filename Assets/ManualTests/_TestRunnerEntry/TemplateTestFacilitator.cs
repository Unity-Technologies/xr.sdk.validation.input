using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateTestFacilitator : TestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        m_Description = "Simple template facilitator that just waits for the user to activate one of the manual status panel buttons.";
        instructionCanvas.Instructions.text = "Activate one of the control buttons below by pressing the corresponding keyboard key or looking directly at it.";

        m_WaitForContinue = true;
        while (overallStatus == OverallTestStatus.NotRun && m_WaitForContinue)
            yield return null;

        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "Continue was successfully triggered");
    }
}
