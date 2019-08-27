using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class BoolFeatureUsageDrivesUI : MonoBehaviour
{
    public Text DisplayText;
    public Image DisplayImage;

    private InputDevice Device;
    private InputFeatureUsage<bool> FeatureUsage;

    public void SetDrivingUsage(InputDevice device, InputFeatureUsage<bool> featureUsage)
    {
        Device = device;
        FeatureUsage = featureUsage;

        gameObject.SetActive(true);
    }

    void Update()
    {
        bool State;

        if (!Device.isValid || !Device.TryGetFeatureValue(FeatureUsage, out State))
            return;

        if (State)
        {
            DisplayText.text = "true";
            if (DisplayImage != null)
                DisplayImage.color = Color.green;
        }
        else
        {
            DisplayText.text = "false";
            if (DisplayImage != null)
                DisplayImage.color = Color.red;
        }
    }
}
