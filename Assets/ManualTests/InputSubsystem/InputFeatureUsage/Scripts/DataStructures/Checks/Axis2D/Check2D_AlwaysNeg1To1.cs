using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check2D_AlwaysNeg1To1 : Check
{
    public Check2D_AlwaysNeg1To1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Failure if a value outside of the range -1.0 to 1.0 is detected.";
        CanForceFailure = true;
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Vector2 value = new Vector2(float.NaN, float.NaN);
        if (!
            (FeatureUsageUnderTest.type == typeof(Vector2)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Vector2>(), out value)
            && value.x >= -1f
            && value.x <= 1f
            && value.y >= -1f
            && value.y <= 1f)
           )
        {
            ForceFail("Value of " + FeatureUsageUnderTest.name + " is outside of the range -1.0 to 1.0.  " +
                "X = " + value.x.ToString("F8") +
                "Y = " + value.y.ToString("F8"));
        }
    }
}
