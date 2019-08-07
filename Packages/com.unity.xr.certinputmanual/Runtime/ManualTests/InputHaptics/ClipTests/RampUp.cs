using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class RampUp : ClipTestButton
{
    protected override bool GenerateClip(ref byte[] clip)
    {
        HapticCapabilities caps = new HapticCapabilities();
        InputDevice device = GetComponentInParent<HapticDeviceUnderTest>().device;

        if (device == null
            || !device.TryGetHapticCapabilities(out caps)
            )
            return false;

        // Generate actual clip
        int clipTime = (int)(caps.bufferFrequencyHz * 2); // 2 seconds
        clip = new byte[clipTime];
        for (int i = 0; i < clipTime; i++)
        {
            clip[i] = (byte)((i / (float)clipTime) * byte.MaxValue);
        }

        return true;
    }
}
