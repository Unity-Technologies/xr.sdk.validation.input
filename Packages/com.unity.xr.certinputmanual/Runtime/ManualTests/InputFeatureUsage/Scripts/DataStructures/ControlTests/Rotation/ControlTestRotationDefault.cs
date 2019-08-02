using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestRotationDefault : ControlTest
{
    public ControlTestRotationDefault(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Rotation control should default to (0.0, 0.0, 0.0, +-1.0) when the control is not active or when the device is pointed straight forward with no roll.";
        CertReqID = "1.2.3.x";

        Checks = new Check[2];
        Checks[0] = new CheckRotationHits0_0_0_1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
