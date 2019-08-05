using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClipTestButton : MonoBehaviour
{
    private byte[] m_Clip = null;

    // Update is called once per frame
    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PlayClip);
    }

    void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(PlayClip);
    }

    // In implementations, overwrite this to generate clips
    protected virtual bool GenerateClip(ref byte[] clip)
    {
        HapticCapabilities caps = new HapticCapabilities();
        InputDevice device = GetComponentInParent<HapticDeviceUnderTest>().device;

        if (device == null
            || !device.TryGetHapticCapabilities(out caps)
            )
            return false;

        // This base implementation generates a very boring clip of solid intensity
        // over the max clip time.
        int clipTime = (int)(caps.bufferFrequencyHz * 2); // 2 seconds
        clip = new byte[clipTime];
        for (int i = 0; i < clipTime; i++)
        {
            clip[i] = byte.MaxValue;
        }

        return true;
    }

    void PlayClip()
    {
        InputDevice device = GetComponentInParent<HapticDeviceUnderTest>().device;

        if (GenerateClip(ref m_Clip) 
            && device != null
            )
        {
            device.SendHapticBuffer(0, m_Clip);
        }
    }
}
