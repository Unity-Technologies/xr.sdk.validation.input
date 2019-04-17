using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTest2DXY : ControlTest
{
    public ControlTest2DXY(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "If this control represents a 2d cartesian coordinate, it must be of the form (x, y)";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
