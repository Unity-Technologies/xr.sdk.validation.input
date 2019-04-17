using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestBinaryDefault : ControlTest
{
    public ControlTestBinaryDefault(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Binary control should default to false when not the control is not actuated. Verify that the device defaults to false and then manually pass this test.";
        CertReqID = "1.2.3.x";

        Debug.Log("ControlTestBinaryDefault usage " + usage.name + ", device " + DeviceUnderTest.name);

        Checks = new Check[2];
        Checks[0] = new CheckBinaryValueHitsFalse(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
