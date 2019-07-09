using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class ArbiterFeatureUsageDrivesUI : MonoBehaviour
{
    // If you add to this list, update HideAllUI() and SetDrivingUsage() as well
    public BoolFeatureUsageDrivesUI BoolFeatureUI;
    public FloatFeatureUsageDrivesUI FloatFeatureUI;
    public UintFeatureUsageDrivesUI UintFeatureUI;
    public Vector2FeatureUsageDrivesUI Vector2FeatureUI;
    public Vector3FeatureUsageDrivesUI Vector3FeatureUI;
    public QuaternionFeatureUsageDrivesUI QuaternionFeatureUI;
    public Text EyeDataUI;
    // If you add to this list, update HideAllUI() and SetDrivingUsage() as well

    private bool m_HideOnStart = true;

    private void Start()
    {
        if (m_HideOnStart)
            HideAllUI();
    }

    private void HideAllUI()
    {
        BoolFeatureUI.gameObject.SetActive(false);
        FloatFeatureUI.gameObject.SetActive(false);
        UintFeatureUI.gameObject.SetActive(false);
        Vector2FeatureUI.gameObject.SetActive(false);
        Vector3FeatureUI.gameObject.SetActive(false);
        QuaternionFeatureUI.gameObject.SetActive(false);

        // Doesn't make sense to show this in the InputArray scene
        if (EyeDataUI != null)
            EyeDataUI.gameObject.SetActive(false);
    }

    public void ClearDrivingUsage()
    {
        HideAllUI();
    }

    public bool SetDrivingUsage(InputDevice device, InputFeatureUsage usage)
    {
        m_HideOnStart = false;

        HideAllUI();

        if (usage.type == typeof(bool))
            BoolFeatureUI.SetDrivingUsage(device, usage.As<bool>());
        else if (usage.type == typeof(float))
            FloatFeatureUI.SetDrivingUsage(device, usage.As<float>());
        else if (usage.type == typeof(uint))
            UintFeatureUI.SetDrivingUsage(device, usage.As<uint>());
        else if (usage.type == typeof(Vector2))
            Vector2FeatureUI.SetDrivingUsage(device, usage.As<Vector2>());
        else if (usage.type == typeof(Vector3))
            Vector3FeatureUI.SetDrivingUsage(device, usage.As<Vector3>());
        else if (usage.type == typeof(Quaternion))
            QuaternionFeatureUI.SetDrivingUsage(device, usage.As<Quaternion>());
        else if (usage.type == typeof(Eyes) && EyeDataUI != null) // Doesn't make sense to show this in the InputArray scene
            EyeDataUI.gameObject.SetActive(true);
        else {
            Debug.LogError("ArbiterFeatureUsageDrivesUI could not SetDrivingUsage() for device " + device.name + " and " + usage.type.ToString() + " type usage " + usage.name);
            return false;
        }

        return true;
    }
}
