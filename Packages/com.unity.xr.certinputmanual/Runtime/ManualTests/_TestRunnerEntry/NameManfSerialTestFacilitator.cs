using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.TestRunnerManualTests;
using UnityEngine.XR;

public class NameManfSerialTestFacilitator : TestFacilitator
{
    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public override IEnumerator RunTest()
    {
        List<InputDevice> m_Devices = new List<InputDevice>();
        InputDevices.GetDevices(m_Devices);
        if (m_Devices.Count == 0)
        {
            RecordStatus(OverallTestStatus.Failed, "No devices detected.  Test failing due to invalid setup.");
            yield break;
        }

        instructionCanvas.Instructions.text = "Verify that the name for each device is clear, succinct, and recognizeable by mass market consumers (not a code name).  It must not be blank.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "Verify that the manufacturer for each device is clear, succinct, and recognizeable by mass market consumers (not a code name).  It must not be blank.  Then either fail or continue.";

        yield return WaitForContinue();

        instructionCanvas.Instructions.text = "Verify that the serial number for each device matches the serial number on the actual device or is blank.  Then either fail or continue.";

        yield return WaitForContinue();

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All haptic tests have been manually approved");
    }
}
