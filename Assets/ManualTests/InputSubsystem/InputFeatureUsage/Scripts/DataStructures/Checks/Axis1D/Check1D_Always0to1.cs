using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check1D_Always0to1 : Check
{
    public Check1D_Always0to1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Failure if a value outside of the range 0.0 to 1.0 is detected.";
        CanForceFailure = true;
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        float value = float.NaN;
        if (!
            (FeatureUsageUnderTest.type == typeof(float)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<float>(), out value)
            && value >= 0f
            && value <= 1f)
           )
        {
            ForceFail("Value of " + FeatureUsageUnderTest.name + " is outside of the range 0f to 1f");
        }
    }
}
