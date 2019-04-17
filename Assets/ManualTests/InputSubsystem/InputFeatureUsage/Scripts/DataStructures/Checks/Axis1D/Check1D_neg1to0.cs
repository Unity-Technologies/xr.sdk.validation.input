using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check1D_neg1to0 : Check
{
    public Check1D_neg1to0(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value lies between -1.0 and 0.0 (for example, -0.5)";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override bool RunCheck()
    {
        float value;
        if (FeatureUsageUnderTest.type == typeof(float)
            && DeviceUnderTest.TryGetFeatureValue(new InputFeatureUsage<float>(FeatureUsageUnderTest.name), out value)
            && value > -1f
            && value < 0f)
        {
            passed = true;
            return true;
        }

        return false;
    }
}
