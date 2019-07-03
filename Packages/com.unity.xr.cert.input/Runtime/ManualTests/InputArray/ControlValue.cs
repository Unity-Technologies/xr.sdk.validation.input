using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

public class ControlValue : MonoBehaviour
{
    public Text nameText;
    public ArbiterFeatureUsageDrivesUI valueArbiter;

    public void SetDrivingUsage(InputDevice device, InputFeatureUsage usage)
    {
        valueArbiter.SetDrivingUsage(device, usage);
        nameText.text = usage.name;
    }
}
