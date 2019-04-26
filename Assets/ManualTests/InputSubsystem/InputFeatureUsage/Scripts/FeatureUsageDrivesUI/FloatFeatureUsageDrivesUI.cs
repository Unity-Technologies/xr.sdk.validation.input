using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class FloatFeatureUsageDrivesUI : MonoBehaviour
{
    public Text DisplayText;

    
    private string m_FloatFormatString = "+000.00; -000.00; +000.00";
    private InputDevice Device;
    private InputFeatureUsage<float> FeatureUsage;

    public void SetDrivingUsage(InputDevice device, InputFeatureUsage<float> featureUsage)
    {
        Device = device;
        FeatureUsage = featureUsage;

        gameObject.SetActive(true);
    }

    void Update()
    {
        float State;

        if (!Device.isValid || !Device.TryGetFeatureValue(FeatureUsage, out State))
            return;

        DisplayText.text = State.ToString(m_FloatFormatString);
    }
}
