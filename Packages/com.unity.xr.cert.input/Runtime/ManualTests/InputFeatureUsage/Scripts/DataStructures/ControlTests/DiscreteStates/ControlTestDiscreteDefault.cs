using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestDiscreteDefault : ControlTest
{
    public ControlTestDiscreteDefault(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Discrete state should default to 0, which indicates null, none, or invalid.  If this value cannot be driven to 0, check that the current value is logical for the current state.";
        CertReqID = "1.2.3.x";

        Checks = new Check[2];
        Checks[0] = new CheckDiscreteDefault(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
