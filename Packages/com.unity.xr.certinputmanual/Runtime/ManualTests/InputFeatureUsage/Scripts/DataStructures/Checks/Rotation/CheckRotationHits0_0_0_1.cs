﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckRotationHits0_0_0_1 : Check
{
    public CheckRotationHits0_0_0_1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to (0.0, 0.0, 0.0, 1.0)";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Quaternion value;
        if (FeatureUsageUnderTest.type == typeof(Quaternion)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Quaternion>(), out value)
            && value.x == 0.0f
            && value.y == 0.0f
            && value.z == 0.0f
            && value.w == 1.0f)
        {
            passed = true;
        }
    }
}
