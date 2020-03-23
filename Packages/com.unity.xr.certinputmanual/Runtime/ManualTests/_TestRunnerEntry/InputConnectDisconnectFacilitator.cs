using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.TestRunnerManualTests;
using UnityEngine.XR;

public class InputConnectDisconnectFacilitator : XRBaseTestFacilitator
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

        instructionCanvas.Instructions.text = "Verify that a Connect event has fired for each device that was connected when the scene started.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "If possible, disconnect and reconnect devices. You should see disconnect, connect, and configuration change events according to your platform's behavior.  Then either fail or continue.";

        yield return WaitForContinue();

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All haptic tests have been manually approved");
    }
}
