using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using Unity.TestRunnerManualTests;

public abstract class XRBaseTestFacilitator : TestFacilitator
{
    // Some Input Providers need extra time to initialize.  You can add that time here.
    public static float InitializeTime = 0f; // In seconds.

    // Some subsystems need time to initialize.
    // Start when any Input Subsystem has a device.
    public override IEnumerator LaunchRunTestWhenReady()
    {
        Debug.Log("Waiting to RunTest...");

        // Wait for a display subsystem.  Right now all known InputProviders are ready by the time its 
        // DisplaySubsystem is ready because DisplaySubsystem needs a HMD device to drive.
        List<XRDisplaySubsystem> DisplayInstances = new List<XRDisplaySubsystem>();
        bool allDisplayInstancesRunning;
        do
        {
            yield return null;
            SubsystemManager.GetInstances<XRDisplaySubsystem>(DisplayInstances);

            allDisplayInstancesRunning = true;
            for (int i = 0; i < DisplayInstances.Count; i++)
            {
                if (!DisplayInstances[i].running)
                {
                    allDisplayInstancesRunning = false;
                    break;
                }
            }
        } while (DisplayInstances.Count == 0 || !allDisplayInstancesRunning);

        // Still check for devices in case we do not have a XRDisplaySubsystem driven by the same
        // plugin as the input provider.
        List<InputDevice> devices = new List<InputDevice>();
        do
        {
            yield return null;
            List<XRInputSubsystem> InputInstances = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances<XRInputSubsystem>(InputInstances);

            if (InputInstances.Count == 0)
                continue;

            InputInstances[0].TryGetInputDevices(devices);
        } while (devices.Count == 0);

        yield return null;
        yield return new WaitForSeconds(InitializeTime);
        Debug.Log("Starting RunTest");
        yield return base.LaunchRunTestWhenReady();
    }
}
