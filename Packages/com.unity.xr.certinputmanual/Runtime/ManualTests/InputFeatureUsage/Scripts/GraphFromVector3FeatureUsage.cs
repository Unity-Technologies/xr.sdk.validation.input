using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class GraphFromVector3FeatureUsage : MonoBehaviour
{
    public Transform particleSystemX;
    public Transform particleSystemY;
    public Transform particleSystemZ;

    private InputDevice m_Device;
    private InputFeatureUsage<Vector3> m_Usage;
    private bool m_Active;

    public void SetActive(InputDevice NewDevice, InputFeatureUsage<Vector3> NewUsage)
    {
        m_Active = true;
        m_Device = NewDevice;
        m_Usage = NewUsage;
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
            
            particleSystemX.position = new Vector3(0, Value.x, 0);
            particleSystemY.position = new Vector3(0, Value.y, 0);
            particleSystemZ.position = new Vector3(0, Value.z, 0);
        }
    }
}
