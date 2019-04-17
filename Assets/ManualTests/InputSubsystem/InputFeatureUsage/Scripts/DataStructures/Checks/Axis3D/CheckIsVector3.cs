using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckIsVector3 : Check
{
    public CheckIsVector3(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is a Vector3";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override bool RunCheck()
    {
        if (FeatureUsageUnderTest.type != typeof(Vector3)) {
            ForceFail();
            return false;
        }

        passed = true;
        return true;
    }
}
