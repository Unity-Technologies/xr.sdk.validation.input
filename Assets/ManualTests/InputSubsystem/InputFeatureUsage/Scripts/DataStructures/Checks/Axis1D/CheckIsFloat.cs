using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckIsFloat : Check
{
    public CheckIsFloat(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is a float";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override bool RunCheck()
    {
        if (FeatureUsageUnderTest.type != typeof(float)) {
            ForceFail();
            return false;
        }

        passed = true;
        return true;
    }
}
