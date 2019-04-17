using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckAutoForceFail : Check
{
    public CheckAutoForceFail(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "This Check automatically fails.";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override bool RunCheck()
    {
        ForceFail();
        return false;
    }
}
