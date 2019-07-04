using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check2DHitsNegXNegY : Check
{
    public Check2DHitsNegXNegY(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value has negative x and negative y values";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Vector2 value;
        if (FeatureUsageUnderTest.type == typeof(Vector2)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Vector2>(), out value)
            && value.x < 0f
            && value.y < 0f)
        {
            passed = true;
        }
    }
}
