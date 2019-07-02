using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckIsQuaternion : Check
{
    public CheckIsQuaternion(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Value is a Quaternion";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        if (FeatureUsageUnderTest.type != typeof(Quaternion)) {
            ForceFail("Type of " + FeatureUsageUnderTest.name + " is not a Quaternion.");
        }

        passed = true;
    }
}
