using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestIsTracked : ControlTest
{
    Bearings m_Bearings = null;

    public ControlTestIsTracked(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "If possible, hide this device from tracking sensors or cover device tracking cameras in an attempt to lose tracking.  " +
            "This control, IsTracked, should report false when this happens.";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
