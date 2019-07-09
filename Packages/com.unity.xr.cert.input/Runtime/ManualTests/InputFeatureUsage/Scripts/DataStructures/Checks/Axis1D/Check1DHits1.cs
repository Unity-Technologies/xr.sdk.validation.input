using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check1DHits1 : Check
{
    public Check1DHits1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to 1.0";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        float value;
        if (FeatureUsageUnderTest.type == typeof(float)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<float>(), out value)
            && value == 1.0f)
        {
            passed = true;
        }
    }
}
