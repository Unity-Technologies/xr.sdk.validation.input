using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class HapticDeviceUnderTest : MonoBehaviour
{
    public InputDevice device { get;  private set; }

    void SetNewHapticDevice(InputDevice newDevice)
    {
        device = newDevice;
    }
}
