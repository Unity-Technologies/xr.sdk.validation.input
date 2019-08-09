using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

public class QuaternionDrivesTextMesh : MonoBehaviour
{
    public TextMesh DisplayText;

    private InputDevice Device;
    private InputFeatureUsage<Quaternion> FeatureUsage;
    private string m_FloatFormatString = "+0.00; -0.00; +0.00";
    
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
        
        DisplayText.text = "(" + State.x.ToString(m_FloatFormatString) + ", " + State.y.ToString(m_FloatFormatString) + ", " + State.z.ToString(m_FloatFormatString) + ", " + State.w.ToString(m_FloatFormatString) + ")";
    }
}
