using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestAcceleration : ControlTest
{
    GraphFromVector3FeatureUsage m_Graph = null;

    public ControlTestAcceleration(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Match the motion of each color of cube to test the acceleration.  You should see the magnitude hit a maximum of about 10." +
            "This value is relative to world space, not the device.  So tilt the device under test when doing this." +
            "\nRed corresponds to X" +
            "\nGreen corresponds to Y" +
            "\nBlue corresponds to Z" +
            "\n\nAs a sanity check, you can drop the device (under safe conditions, guaranteeing that you can catch it!)  An object in free fall must have an acceleration of approximately (0, -9.81, 0).";

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
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().bearings.EnableAcceleration = true;

        m_Graph = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().graphVector3;
        if (m_Graph != null)
        {
            m_Graph.gameObject.SetActive(true);
            m_Graph.SetActive(DeviceUnderTest, FeatureUsageUnderTest.As<Vector3>(), 10f);
        }
    }

    public override void Teardown()
    {
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().bearings.EnableAcceleration = false;

        if (m_Graph != null) {
            m_Graph.SetInactive();
            m_Graph.gameObject.SetActive(false);
        }
    }
}
