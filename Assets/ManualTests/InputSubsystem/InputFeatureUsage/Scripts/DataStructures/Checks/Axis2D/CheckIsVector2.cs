using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckIsVector2 : Check
{
    public CheckIsVector2(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is a Vector2";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override bool RunCheck()
    {
        if (FeatureUsageUnderTest.type != typeof(Vector2)) {
            ForceFail();
            return false;
        }

        passed = true;
        return true;
    }
}
