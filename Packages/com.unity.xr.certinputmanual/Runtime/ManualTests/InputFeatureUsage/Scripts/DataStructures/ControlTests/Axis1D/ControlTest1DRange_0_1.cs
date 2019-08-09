using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTest1DRange_0_1 : ControlTest
{
    public ControlTest1DRange_0_1(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "This control's range must be 0.0 to 1.0.  Check that it hits both extremes and is never outside that range.";
        CertReqID = "1.2.3.x";

        Checks = new Check[5];
        Checks[0] = new Check1DHits0(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new Check1DHits1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[2] = new Check1D_0to1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[3] = new Check1D_Always0to1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[4] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
