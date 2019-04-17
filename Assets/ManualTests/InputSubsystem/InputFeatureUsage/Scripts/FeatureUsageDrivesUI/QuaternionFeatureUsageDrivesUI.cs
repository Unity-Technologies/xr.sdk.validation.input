using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class QuaternionFeatureUsageDrivesUI : MonoBehaviour
{
    public Text DisplayText;

    private InputDevice Device;
    private InputFeatureUsage<Quaternion> FeatureUsage;

    public void SetDrivingUsage(InputDevice device, InputFeatureUsage<Quaternion> featureUsage)
    {
        Device = device;
        FeatureUsage = featureUsage;

        gameObject.SetActive(true);
    }

    void Update()
    {
        Quaternion State;

        if (!Device.isValid || !Device.TryGetFeatureValue(FeatureUsage, out State))
            return;

        DisplayText.text = State.ToString();
    }
}
