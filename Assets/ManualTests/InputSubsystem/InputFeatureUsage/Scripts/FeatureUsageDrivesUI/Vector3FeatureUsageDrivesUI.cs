﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class Vector3FeatureUsageDrivesUI : MonoBehaviour
{
    public Text DisplayText;

    private InputDevice Device;
    private InputFeatureUsage<Vector3> FeatureUsage;
    private string m_FloatFormatString = "+000.00; -000.00; +000.00";

    public void SetDrivingUsage(InputDevice device, InputFeatureUsage<Vector3> featureUsage)
    {
        Device = device;
        FeatureUsage = featureUsage;

        gameObject.SetActive(true);
    }

    void Update()
    {
        Vector3 State;

        if (!Device.isValid || !Device.TryGetFeatureValue(FeatureUsage, out State))
            return;
        
        DisplayText.text = "(" + State.x.ToString(m_FloatFormatString) + ", " + State.y.ToString(m_FloatFormatString) + ", " + State.z.ToString(m_FloatFormatString) + ")";
    }
}
