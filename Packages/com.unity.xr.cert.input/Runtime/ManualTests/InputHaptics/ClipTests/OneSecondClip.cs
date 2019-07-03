using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class OneSecondClip : ClipTestButton
{
    protected override bool GenerateClip(XRNode node, ref byte[] clip)
    {
        HapticCapabilities caps = new HapticCapabilities();

        InputDevice device = InputDevices.GetDeviceAtXRNode(node);

        if (device == null
            || !device.TryGetHapticCapabilities(out caps)
            )
            return false;

        // Generate actual clip
        int clipTime = (int)(caps.bufferFrequencyHz * 1); // 1 second
        clip = new byte[clipTime];
        for (int i = 0; i < clipTime; i++)
        {
            clip[i] = byte.MaxValue;
        }

        return true;
    }
}
