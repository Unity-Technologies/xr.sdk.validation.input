using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestUnsupportedType : ControlTest
{
    public ControlTestUnsupportedType(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "The current control's type is invalid.  This test should automatically fail.";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckAutoForceFail(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
