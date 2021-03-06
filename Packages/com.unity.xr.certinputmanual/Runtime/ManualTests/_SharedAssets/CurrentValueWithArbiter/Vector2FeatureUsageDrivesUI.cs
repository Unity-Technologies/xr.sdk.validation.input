﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class Vector2FeatureUsageDrivesUI : MonoBehaviour
{
    public Text DisplayText;
    
    private string m_FloatFormatString = "+000.00; -000.00; +000.00";
    private InputDevice Device;
    private InputFeatureUsage<Vector2> FeatureUsage;

    public void SetDrivingUsage(InputDevice device, InputFeatureUsage<Vector2> featureUsage)
    {
        Device = device;
        FeatureUsage = featureUsage;

        gameObject.SetActive(true);
    }

    void Update()
    {
        Vector2 State;

        if (!Device.isValid || !Device.TryGetFeatureValue(FeatureUsage, out State))
            return;

        DisplayText.text = "(" + State.x.ToString(m_FloatFormatString) + ", " + State.y.ToString(m_FloatFormatString) + ")";
    }
}
