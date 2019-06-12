using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ListDevices : MonoBehaviour
{
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        ListTheDevices();
    }

    void OnEnable()
    {
        text = GetComponent<Text>();

        InputDevices.deviceConnected += HandleDeviceChange;
        InputDevices.deviceDisconnected += HandleDeviceChange;
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= HandleDeviceChange;
        InputDevices.deviceDisconnected -= HandleDeviceChange;
    }

    void HandleDeviceChange(InputDevice device)
    {
        ListTheDevices();
    }

    void ListTheDevices()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevices(devices);

#if UNITY_2019_3_OR_NEWER
        text.text = "";
#else
        text.text = "Manufacturer and Serial Number are only accessibly in 2019.3+. You only need to check the names in this Unity version.";
#endif

        for (int i = 0; i < devices.Count; i++)
        {
            if (i != 0)
                text.text += "\n";

#if UNITY_2019_3_OR_NEWER
            text.text += "[" + (i + 1) + "] Name: " + devices[i].name + ", Manufacturer: " + devices[i].manufacturer + ", Serial Number: " + devices[i].serialNumber;
#else
            text.text += "[" + (i + 1) + "] Name: " + devices[i].name + ", ";
#endif
        }
    }
}
