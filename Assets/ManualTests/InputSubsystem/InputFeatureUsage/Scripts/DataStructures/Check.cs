using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public abstract class Check
{
    public bool CanForceFailure { get; protected set; }
    public bool RequiresManualPass { get; protected set; }
    protected bool passed;
    public bool Passed { get { return (passed && !ForcedFail); } }
    public bool ForcedFail  { get; protected set; }

    public InputDevice DeviceUnderTest { get; protected set; }
    public InputFeatureUsage FeatureUsageUnderTest { get; protected set; }
    public ControlTest ParentControlTest { get; protected set; }

    public string SuccessConditionDescription { get; protected set; }

    public Check(InputDevice device, InputFeatureUsage featureUsage, ControlTest parentTest)
    {
        CanForceFailure = false;
        RequiresManualPass = false;
        FeatureUsageUnderTest = featureUsage;
        DeviceUnderTest = device;
        ParentControlTest = parentTest;
    }

    // Run the check, which should be a single testable property or characteristic
    
    public abstract void RunCheck();

    public delegate void ForcedFailureHandler(Check sender, string reasonForFailure);
    public event ForcedFailureHandler ForcedFailure;

    protected void ForceFail(string reasonForFailure)
    {
        ForcedFail = true;
        ForcedFailure?.Invoke(this, reasonForFailure);
    }
}
