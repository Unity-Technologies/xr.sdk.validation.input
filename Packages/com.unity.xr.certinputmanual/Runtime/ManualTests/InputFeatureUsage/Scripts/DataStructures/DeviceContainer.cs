using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class DeviceContainer
{
    public InputDevice Device;
    public DeviceItemUIManager UIManager;
    public bool HaveAllTestsPassed = false;
    public bool NoTestsToRun = false;

    public DeviceContainer(InputDevice device)
    {
        Device = device;
        HaveAllTestsPassed = false;
    }
}
