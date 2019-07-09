using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Check3DHits0_0_0 : Check
{
    public Check3DHits0_0_0(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is set to (0.0, 0.0, 0.0)";
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Vector3 value;
        if (FeatureUsageUnderTest.type == typeof(Vector3)
            && DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Vector3>(), out value)
            && value.x == 0.0f
            && value.y == 0.0f
            && value.z == 0.0f)
        {
            passed = true;
        }
    }
}
