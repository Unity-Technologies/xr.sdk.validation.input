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
    public override bool RunCheck()
    {
        uint value;
        if (FeatureUsageUnderTest.type == typeof(uint)
            && DeviceUnderTest.TryGetFeatureValue(new InputFeatureUsage<uint>(FeatureUsageUnderTest.name), out value)
            && value == 0)
        {
            passed = true;
            return true;
        }

        return false;
    }
}
