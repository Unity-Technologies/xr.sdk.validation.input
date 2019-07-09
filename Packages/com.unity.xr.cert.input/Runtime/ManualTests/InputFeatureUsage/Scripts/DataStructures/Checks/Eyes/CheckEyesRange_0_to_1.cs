using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckEyesRange_0_to_1 : Check
{
    public CheckEyesRange_0_to_1(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Eye open values are always within [0, 1]";
        CanForceFailure = true;
    }

    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
        Eyes value = new Eyes();
        float left = 0;
        float right = 0;
        if (DeviceUnderTest.TryGetFeatureValue(FeatureUsageUnderTest.As<Eyes>(), out value))
        {
            value.TryGetLeftEyeOpenAmount(out left);
            value.TryGetRightEyeOpenAmount(out right);

            if (left < 0 || left > 1)
                ForceFail("ForceFail: Left Eye open value is not within [0, 1]. Left Eye Open Value is " + left);
            if (right < 0 || right > 1)
                ForceFail("ForceFail: Right Eye open value is not within [0, 1]. Right Eye Open Value is " + right);
        }

        passed = true;
    }
}