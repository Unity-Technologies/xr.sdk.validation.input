using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureUsageTestFacilitator : TestFacilitator
{
    public bool m_WaitForTestFinish = false;

    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "This test moves through all connected devices and their controls.  Follow the instructions on the UI above to completion.";

        BroadcastMessage("StartTests");   // Kick off DeviceTestManager
        yield return WaitForTestFinish(); // DeviceTestManager will call TestFinish when complete
    }

    // Pass control to DeviceTestManager and wait for it to report finished
    // Don't use continue because it's on the Instructions UI and I don't want to give a "pass all" through that button
    protected IEnumerator WaitForTestFinish()
    {
        m_WaitForTestFinish = true;
        while (overallStatus == OverallTestStatus.NotRun && m_WaitForTestFinish)
            yield return null;
    }

    public void TestFinish(OverallTestStatus status, string statusDetails)
    {
        RecordStatus(status, statusDetails);
        m_WaitForTestFinish = false;

    }
}
