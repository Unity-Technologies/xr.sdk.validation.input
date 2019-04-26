using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class AngularVelocityVisualizer : MonoBehaviour
{
    InputDevice m_Device;
    InputFeatureUsage<Vector3> m_AngularVelocityUsage;

    Transform m_Transform;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = transform; // cache since we change this every frame
    }

    public void SetDrivers(InputDevice device, InputFeatureUsage<Vector3> angularVelocityUsage)
    {
        m_Device = device;
        m_AngularVelocityUsage = angularVelocityUsage;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 AngularVelocity;
        if (m_Device.TryGetFeatureValue(m_AngularVelocityUsage, out AngularVelocity))
            m_Transform.Rotate(AngularVelocity, Space.World);
    }
}
