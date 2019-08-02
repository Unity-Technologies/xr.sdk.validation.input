using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestRotationNormalizedXYZW : ControlTest
{
    public ControlTestRotationNormalizedXYZW(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "If this control represents a normalized cartesian coordinate, it must be of the form (x, y, z, w).  Rotate the device under test. If the quaternion is not normalized this test will automatically fail.";
        CertReqID = "1.2.3.x";

        Checks = new Check[2];
        Checks[0] = new CheckRotationIsNormalized(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
