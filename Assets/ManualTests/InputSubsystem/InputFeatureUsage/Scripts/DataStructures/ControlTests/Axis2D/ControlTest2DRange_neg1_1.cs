using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTest2DRange_neg1_1 : ControlTest
{
    public ControlTest2DRange_neg1_1(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "This control's range must be -1.0 to 1.0.  Check that it hits all extremes and is never outside that range.";
        CertReqID = "1.2.3.x";

        Debug.Log("ControlTestBinaryRange usage " + usage.name + ", device " + DeviceUnderTest.name);

        Checks = new Check[11];
        Checks[0] = new Check2DHits0_0(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new Check2DHits1_0(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[2] = new Check2DHits0_1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[3] = new Check2DHits0_neg1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[4] = new Check2DHitsneg1_0(DeviceUnderTest, FeatureUsageUnderTest, this);
        
        Checks[5] = new Check2DHitsPosXPosY(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[6] = new Check2DHitsNegXNegY(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[7] = new Check2DHitsPosXNegY(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[8] = new Check2DHitsNegXPosY(DeviceUnderTest, FeatureUsageUnderTest, this);

        Checks[9] = new Check2D_AlwaysNeg1To1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[10] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
