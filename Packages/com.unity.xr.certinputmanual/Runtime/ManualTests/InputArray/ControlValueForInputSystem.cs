using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlValueForInputSystem : MonoBehaviour
{
    public Text nameText;
    public Text displayNameText;
    public Text layoutText;
    public Text typeText;
    public ArbiterInputSystemDrivesUI valueArbiter;

    public void SetDrivingControl(InputDevice device, InputControl control)
    {
        nameText.text = control.name;
        displayNameText.text = control.displayName;
        layoutText.text = control.layout;

        string typeString = control.GetType().ToString();
        string DefaultControlsPath = "UnityEngine.InputSystem.Controls.";
        if (typeString.StartsWith(DefaultControlsPath))
            typeString = typeString.Substring(DefaultControlsPath.Length);
        typeText.text = typeString;

        valueArbiter.SetDrivingControl(control);
    }
}
