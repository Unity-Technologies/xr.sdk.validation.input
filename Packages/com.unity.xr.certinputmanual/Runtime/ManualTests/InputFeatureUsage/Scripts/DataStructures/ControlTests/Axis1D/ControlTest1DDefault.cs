using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTest1DDefault : ControlTest
{
    public ControlTest1DDefault(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "1D Axis control should default to 0.0 when the control is not actuated. Verify that the device defaults to 0.0 and then manually pass this test.";
        CertReqID = "1.2.3.x";

        Debug.Log("ControlTestBinaryRange usage " + usage.name + ", device " + DeviceUnderTest.name);

        Checks = new Check[2];
        Checks[0] = new Check1DHits0(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
