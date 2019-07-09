using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckDiscreteDefault : Check
{
    public CheckDiscreteDefault(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to 0.0";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        uint value;
        if (FeatureUsageUnderTest.type == typeof(uint)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<uint>(), out value)
            && value == 0)
        {
            passed = true;
        }
    }
}
