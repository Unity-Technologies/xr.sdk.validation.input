using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class XRNodeCapabilities : MonoBehaviour
{
    public Text deviceName;
    public Text numChannels;
    public Text supportsImpulse;
    public Text supportsBuffer;
    public Text bufferFreqHz;


    void Update()
    {
        HapticCapabilities caps = new HapticCapabilities();
        InputDevice device = GetComponentInParent<HapticDeviceUnderTest>().device;

        if (device == null
            || !device.TryGetHapticCapabilities(out caps)
            )
        {
            Debug.Log("TryGetHapticCapabilities failed for device " + device.name);
            deviceName.text = device.name;
            numChannels.text = "failed to get haptic capabilities";
            supportsImpulse.text = "failed to get haptic capabilities";
            supportsBuffer.text = "failed to get haptic capabilities";
            bufferFreqHz.text = "failed to get haptic capabilities";
            return;
        }

        deviceName.text = device.name;
        numChannels.text = caps.numChannels.ToString();
        supportsImpulse.text = caps.supportsImpulse.ToString();
        supportsBuffer.text = caps.supportsBuffer.ToString();
        bufferFreqHz.text = caps.bufferFrequencyHz.ToString();
    }
}
