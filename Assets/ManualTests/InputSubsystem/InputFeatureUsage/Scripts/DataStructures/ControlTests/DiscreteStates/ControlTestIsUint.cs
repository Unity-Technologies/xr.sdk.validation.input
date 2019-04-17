using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestIsUint : ControlTest
{
    public ControlTestIsUint(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "This control must be backed by a uint value.";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckIsUint(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
