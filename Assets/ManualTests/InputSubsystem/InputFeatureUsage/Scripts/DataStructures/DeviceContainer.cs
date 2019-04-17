using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class DeviceContainer
{
    public InputDevice Device;
    public DeviceItemUIManager UIManager;

    public DeviceContainer(InputDevice device)
    {
        Device = device;
    }
}
