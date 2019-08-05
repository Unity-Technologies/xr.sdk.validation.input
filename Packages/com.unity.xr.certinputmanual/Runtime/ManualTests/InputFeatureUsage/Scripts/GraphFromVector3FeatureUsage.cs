using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class GraphFromVector3FeatureUsage : MonoBehaviour
{
    public Transform particleSystemX;
    public Transform particleSystemY;
    public Transform particleSystemZ;

    public Text GraphMaxAmplitude;
    public Text GraphMinAmplitude;

    private InputDevice m_Device;
    private InputFeatureUsage<Vector3> m_Usage;
    private bool m_Active;
    private float m_GraphMaxAmplitude;

    public void SetActive(InputDevice NewDevice, InputFeatureUsage<Vector3> NewUsage, float NewGraphMaxAmplitude)
    {
        m_Active = true;
        m_Device = NewDevice;
        m_Usage = NewUsage;
        m_GraphMaxAmplitude = NewGraphMaxAmplitude;

        GraphMaxAmplitude.text = "+" + Mathf.Abs(NewGraphMaxAmplitude);
        GraphMinAmplitude.text = "-" + Mathf.Abs(NewGraphMaxAmplitude);
    }

    public void SetInactive()
    {
        m_Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Active)
        {
            Vector3 Value;
            m_Device.TryGetFeatureValue(m_Usage, out Value);
            
            particleSystemX.position = new Vector3(particleSystemX.position.x, Value.x / m_GraphMaxAmplitude, particleSystemX.position.z);
            particleSystemY.position = new Vector3(particleSystemY.position.x, Value.y / m_GraphMaxAmplitude, particleSystemY.position.z);
            particleSystemZ.position = new Vector3(particleSystemZ.position.x, Value.z / m_GraphMaxAmplitude, particleSystemZ.position.z);
        }
    }
}
