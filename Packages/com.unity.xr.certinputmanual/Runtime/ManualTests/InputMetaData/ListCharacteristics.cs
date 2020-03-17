using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ListCharacteristics : MonoBehaviour
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

        text.text = "";

        for (int i = 0; i < devices.Count; i++)
        {
            if (i != 0)
                text.text += "\n";

            text.text += "[" + (i + 1) + "] Name: " + devices[i].name + ", Characteristics: " + devices[i].characteristics;
        }
    }
}
