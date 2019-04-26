using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestPosition : ControlTest
{
    GameObject m_Visualizer = null;

    public ControlTestPosition(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "A visualizer has been added to the device under test.  Verify that the virtual position matches the movements of the real-world position.  " +
            "For all devices except Tracking References, move the device between cubes to verify that X, Y, and Z all update correctly relative to the coordinates reported by the cubes.";

        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }

    public override void Setup()
    {
        DeviceTestManager TestManager = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>();

        // Set up visualizer
        m_Visualizer = Object.Instantiate(TestManager.TrackedDeviceVisualizerPositionPrefab);
        m_Visualizer.transform.SetParent(TestManager.XRRig.transform);
        TrackedDevice Device = m_Visualizer.GetComponent<TrackedDevice>();
        Device.device = DeviceUnderTest;
        Device.positionUsage = FeatureUsageUnderTest.As<Vector3>();

        // Show coordinates
        TestManager.bearings.EnableCoordinates = true;
    }

    public override void Teardown()
    {
        Object.Destroy(m_Visualizer);
        
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().bearings.EnableCoordinates = false;
    }
}
