using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestVelocity : ControlTest
{
    Bearings m_Bearings = null;
    GraphFromVector3FeatureUsage m_Graph = null;

    public ControlTestVelocity(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Match the device under test's motion with the particles moving through the air.  " +
            "Observe the velocity value is approximately (0.2, 0.2, 0.2).  The magnitude is approximately 0.35.";
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
        m_Bearings.EnableXYZVelocityParticles = true;

        m_Graph = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().graphVector3;
        if (m_Graph != null)
        {
            m_Graph.gameObject.SetActive(true);
            m_Graph.SetActive(DeviceUnderTest, FeatureUsageUnderTest.As<Vector3>(), 0.2f);
        }
    }

    public override void Teardown()
    {
        if (m_Bearings != null)
            m_Bearings.EnableXYZVelocityParticles = false;

        if (m_Graph != null) {
            m_Graph.SetInactive();
            m_Graph.gameObject.SetActive(false);
        }
    }
}
