using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class EyePointer : MonoBehaviour
{
    public enum LeftOrRightEye { LeftEye, RightEye }
    
    public Vector3 positionOffset = Vector3.zero;

    InputDevice m_Device;
    InputFeatureUsage m_Usage;
    LeftOrRightEye m_EyeSide = LeftOrRightEye.LeftEye;

    private Transform myTransform;

    public void SetDeviceAndUsage(InputDevice device, InputFeatureUsage usage, LeftOrRightEye eyeSide)
    {
        m_Device = device;
        m_Usage = usage;
        m_EyeSide = eyeSide;
    }

    void Start()
    {
        myTransform = transform; // cache a reference to transform
    }

    // Update is called once per frame
    void Update()
    {
        Eyes currentEyeState = new Eyes();
        Vector3 eyePosition = new Vector3();
        Quaternion eyeRotation = new Quaternion();

        if (m_Device.isValid && m_Device.TryGetFeatureValue(m_Usage.As<Eyes>(), out currentEyeState))
        {
            if (m_EyeSide == LeftOrRightEye.LeftEye) {
                currentEyeState.TryGetLeftEyePosition(out eyePosition);
                currentEyeState.TryGetLeftEyeRotation(out eyeRotation);
            }
            else
            {
                currentEyeState.TryGetRightEyePosition(out eyePosition);
                currentEyeState.TryGetRightEyeRotation(out eyeRotation);
            }
            myTransform.localPosition = eyePosition + positionOffset;
        }
    }
}
