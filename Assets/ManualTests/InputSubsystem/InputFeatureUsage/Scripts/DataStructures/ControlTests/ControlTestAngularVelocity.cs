using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestAngularVelocity : ControlTest
{
    Bearings m_Bearings = null;

    public ControlTestAngularVelocity(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Match the device under test's motion with the cubes orbiting it.  Observe the angular velocity for each vector element ~= 0.2f.";
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
        m_Bearings = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().bearings;
        m_Bearings.EnableAngularVelocityOrbit(DeviceUnderTest, FeatureUsageUnderTest.As<Vector3>());
    }

    public override void Teardown()
    {
        if (m_Bearings != null)
            m_Bearings.DisableAngularVelocityOrbit();
    }
}
