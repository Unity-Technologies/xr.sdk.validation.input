using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.TestRunnerManualTests;
using UnityEngine.XR;

public class InputHapticsTestFacilitator : TestFacilitator
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

        for (int i = 0; i < m_Devices.Count; i++)
        {
            Debug.Log("Testing device [" + i + "] " + m_Devices[i].name);

            DisplayNextDevice(m_Devices[i]);

            instructionCanvas.Instructions.text = "Now testing new device \"" + m_Devices[i].name + "\".\n\nIf your device supports haptic impulse, verify that the intensity can be changed and the haptic strength reacts appropriately.  Then either fail or continue.";
            yield return WaitForContinue();
            instructionCanvas.Instructions.text = "Now testing new device \"" + m_Devices[i].name + "\".\n\nIf your device supports haptic impulse, verify that the duration can be changed and the time of the impulse is correct.  Then either fail or continue.";
            yield return WaitForContinue();
            instructionCanvas.Instructions.text = "Now testing new device \"" + m_Devices[i].name + "\".\n\nIf your device supports haptic impulse, verify that the Stop Haptics buttons stop an impulse.  Then either fail or continue.";
            yield return WaitForContinue();
            instructionCanvas.Instructions.text = "Now testing new device \"" + m_Devices[i].name + "\".\n\nIf your device supports buffered haptics, verify that the ramp up and ramp down effects feel correct.  Then either fail or continue.";
            yield return WaitForContinue();
            instructionCanvas.Instructions.text = "Now testing new device \"" + m_Devices[i].name + "\".\n\nIf your device supports buffered haptics, verify that the 1 second clip lasts one second.  Then either fail or continue.";
            yield return WaitForContinue();
            instructionCanvas.Instructions.text = "Now testing new device \"" + m_Devices[i].name + "\".\n\nIf your device supports buffered haptics, verify that the Stop Haptics buttons stop a buffered clip.  Then either fail or continue.";
            yield return WaitForContinue();
        }

        // If your test successfully reaches its conclusion, RecordStatus() as passed.
        // The second parameter of RecordStatus allows you to give more information 
        // for a success or failure in the results log.
        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "All haptic tests have been manually approved");
    }

    void DisplayNextDevice(InputDevice device)
    {
        BroadcastMessage("SetNewHapticDevice", device);
    }
}
