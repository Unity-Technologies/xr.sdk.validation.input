using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check1DHits0 : Check
{
    public Check1DHits0(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to 0.0";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        float value;
        if (FeatureUsageUnderTest.type == typeof(float)
            && DeviceUnderTest.TryGetFeatureValue(new InputFeatureUsage<float>(FeatureUsageUnderTest.name), out value)
            && value == 0.0f)
        {
            passed = true;
        }
    }
}
