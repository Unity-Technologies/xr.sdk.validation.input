using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class CheckRequireManualPass : Check
{
    public CheckRequireManualPass(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest) : base(device, featureUsage, parentTest)
    {
        SuccessConditionDescription = "Manually verify the test condition";
        RequiresManualPass = true;
    }
    
    // Run the check, which should be a single testable property or characteristic
    public override void RunCheck()
    {
    }
}
