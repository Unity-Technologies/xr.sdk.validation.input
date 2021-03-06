﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestIsVector2 : ControlTest
{
    public ControlTestIsVector2(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "This control must be backed by a Vector2 value.";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckIsVector2(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }
}
