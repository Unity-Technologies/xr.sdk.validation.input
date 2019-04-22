using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check1D_0to1 : Check
{
    public Check1D_0to1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value observed between 0.0 and 1.0 (for example, 0.5)";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        float value;
        if (FeatureUsageUnderTest.type == typeof(float)
            && DeviceUnderTest.TryGetFeatureValue(new InputFeatureUsage<float>(FeatureUsageUnderTest.name), out value)
            && value > 0f
            && value < 1f)
        {
            passed = true;
        }
    }
}
