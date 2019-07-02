using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestBinaryRange : ControlTest
{
    public ControlTestBinaryRange(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Binary range must hit both false and true to pass this test.";
        CertReqID = "1.2.3.x";

        Checks = new Check[3];
        Checks[0] = new CheckBinaryValueHitsFalse(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckBinaryValueHitsTrue(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[2] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
