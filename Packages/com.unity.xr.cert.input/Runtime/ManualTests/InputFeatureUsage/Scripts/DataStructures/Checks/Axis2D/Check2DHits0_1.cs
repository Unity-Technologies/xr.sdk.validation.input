using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check2DHits0_1 : Check
{
    public Check2DHits0_1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to (0.0, 1.0)";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Vector2 value;
        if (FeatureUsageUnderTest.type == typeof(Vector2)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Vector2>(), out value)
            && value.x == 0.0f
            && value.y == 1.0f)
        {
            passed = true;
        }
    }
}
