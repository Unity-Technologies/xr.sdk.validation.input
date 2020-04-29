using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ArbiterInputSystemDrivesUI : MonoBehaviour
{
    public Text valueText;

    private InputControl m_Control = null;

    public void SetDrivingControl(InputControl control)
    {
        m_Control = control;
    }

    private void Update()
    {
        if (m_Control == null)
            return;

        valueText.text = m_Control.ReadValueAsObject().ToString();
    }
}
