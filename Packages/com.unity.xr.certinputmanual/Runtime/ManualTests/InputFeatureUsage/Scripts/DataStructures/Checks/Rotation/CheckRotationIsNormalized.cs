﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckRotationIsNormalized : Check
{
    public CheckRotationIsNormalized(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is always normalized";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Quaternion value;
        if (FeatureUsageUnderTest.type == typeof(Quaternion)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Quaternion>(), out value)
            && Mathf.Sqrt(Mathf.Pow(value.x, 2) + Mathf.Pow(value.y, 2) + Mathf.Pow(value.z, 2) + Mathf.Pow(value.w, 2)) != 1.0f)
        {
            ForceFail("ForceFail: quaternion is not normalized. Magnitude = " + (Mathf.Sqrt(Mathf.Pow(value.x, 2.0f) + Mathf.Pow(value.y, 2.0f) + Mathf.Pow(value.z, 2.0f) + Mathf.Pow(value.w, 2.0f))).ToString("F8"));
        }

        passed = true;
    }
}
