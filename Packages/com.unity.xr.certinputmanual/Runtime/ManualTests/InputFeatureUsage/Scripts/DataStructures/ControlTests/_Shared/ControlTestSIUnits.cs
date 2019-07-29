using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestSIUnits : ControlTest
{
    public ControlTestSIUnits(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "If this control represents a physical value, that value must be described in SI units.  The valid SI units are meter, kilogram, second, ampere, kelvin, candela, and mole.  If this control describes a physical value, it must use a valid combination of only those units.";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
