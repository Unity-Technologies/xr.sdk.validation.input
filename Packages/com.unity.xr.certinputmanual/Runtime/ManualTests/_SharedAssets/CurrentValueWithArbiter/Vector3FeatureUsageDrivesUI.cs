﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class Vector3FeatureUsageDrivesUI : MonoBehaviour
{
    public Text DisplayText;
    public bool ShowMagnitude = true;

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

        DisplayText.text = State.ToString(m_FloatFormatString);
        if (ShowMagnitude)
            DisplayText.text += "\nMagnitude = " + State.magnitude.ToString(m_FloatFormatString);
    }
}
