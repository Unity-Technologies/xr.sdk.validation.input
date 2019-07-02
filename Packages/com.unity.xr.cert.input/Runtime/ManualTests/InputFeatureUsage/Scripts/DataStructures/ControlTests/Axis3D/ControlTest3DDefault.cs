using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTest3DDefault : ControlTest
{
    public ControlTest3DDefault(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "3D Axis control should default to (0.0, 0.0, 0.0) when not the control is not actuated or is in the \"default\" position. Verify that the device defaults to (0.0, 0.0, 0.0) and then manually pass this test.";
        CertReqID = "1.2.3.x";

        Checks = new Check[2];
        Checks[0] = new Check3DHits0_0_0(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
