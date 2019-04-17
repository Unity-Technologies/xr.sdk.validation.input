using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckIsBool : Check
{
    public CheckIsBool(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is a bool";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override bool RunCheck()
    {
        if (FeatureUsageUnderTest.type != typeof(bool)) {
            ForceFail();
            return false;
        }

        passed = true;
        return true;
    }
}
