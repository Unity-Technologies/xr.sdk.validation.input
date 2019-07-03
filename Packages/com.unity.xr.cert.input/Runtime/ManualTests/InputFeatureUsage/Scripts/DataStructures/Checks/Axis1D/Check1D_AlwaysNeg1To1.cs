using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check1D_AlwaysNeg1To1 : Check
{
    public Check1D_AlwaysNeg1To1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Failure if a value outside of the range -1.0 to 1.0 is detected.";
        CanForceFailure = true;
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        float value = float.NaN;
        if (!
            (FeatureUsageUnderTest.type == typeof(float)
            && DeviceUnderTest.TryGetFeatureValue(new InputFeatureUsage<float>(FeatureUsageUnderTest.name), out value)
            && value >= -1f
            && value <= 1f)
           )
        {
            ForceFail("Value of " + FeatureUsageUnderTest.name + " is outside of the range -1.0 to 1.0");
        }
    }
}
