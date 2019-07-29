using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckBinaryValueHitsFalse : Check
{
    public CheckBinaryValueHitsFalse(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to \"false\"";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        bool value;
        if (FeatureUsageUnderTest.type == typeof(bool)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<bool>(), out value)
            && value == false)
        {
            passed = true;
        }
    }
}
