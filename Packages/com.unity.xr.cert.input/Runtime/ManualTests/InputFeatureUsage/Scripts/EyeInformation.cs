using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class EyeInformation : MonoBehaviour
{
    public Text FixationPoint;
    public Text EyeOpenAmountLeft;
    public Text EyeOpenAmountRight;
    public Text EyePositionLeft;
    public Text EyePositionRight;
    public Text EyeRotationLeft;
    public Text EyeRotationRight;


    InputDevice m_Device;
    InputFeatureUsage m_Usage;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetDeviceAndUsage(InputDevice device, InputFeatureUsage usage)
    {
        m_Device = device;
        m_Usage = usage;
    }
    
    // Update is called once per frame
    void Update()
    {
        Eyes EyeData = new Eyes();
        float LeftEyeOpenAmount = -1;
        float RightEyeOpenAmount = -1;
        Vector3 FixationPointValue;
        Vector3 PositionLeft;
        Vector3 PositionRight;
        Quaternion RotationLeft;
        Quaternion RotationRight;

        if (m_Device.isValid)
        {
            if (m_Device.TryGetFeatureValue(m_Usage.As<Eyes>(), out EyeData))
            {
                EyeData.TryGetLeftEyeOpenAmount(out LeftEyeOpenAmount);
                EyeOpenAmountLeft.text = LeftEyeOpenAmount.ToString();
                EyeData.TryGetRightEyeOpenAmount(out RightEyeOpenAmount);
                EyeOpenAmountRight.text = RightEyeOpenAmount.ToString();

                EyeData.TryGetFixationPoint(out FixationPointValue);
                FixationPoint.text = FixationPointValue.ToString();

                EyeData.TryGetLeftEyePosition(out PositionLeft);
                EyePositionLeft.text = PositionLeft.ToString();
                EyeData.TryGetRightEyePosition(out PositionRight);
                EyePositionRight.text = PositionRight.ToString();

                EyeData.TryGetLeftEyeRotation(out RotationLeft);
                EyeRotationLeft.text = RotationLeft.ToString();
                EyeData.TryGetRightEyeRotation(out RotationRight);
                EyeRotationRight.text = RotationRight.ToString();
            }
        }
    }
}
