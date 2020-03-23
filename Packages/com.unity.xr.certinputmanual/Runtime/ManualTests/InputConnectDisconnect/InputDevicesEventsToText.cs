using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

public class InputDevicesEventsToText : MonoBehaviour
{
    public Text eventsText;
    public int m_QueueMaximumSize = 20;
    [Tooltip("After the queue is full, no longer accept new values.")]
    public bool fillQueueOnlyOnce = false;

    private Queue<string> m_Events;
    private int m_EventNumber = 0;

    private void Awake()
    {
        // Queue setup
        m_Events = new Queue<string>();
    }

    private void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        InputDevices.deviceConfigChanged += OnDeviceConfigChanged;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        InputDevices.deviceConfigChanged += OnDeviceConfigChanged;
    }

    void OnDeviceConnected(InputDevice device)
    {
        AddEventToQueue("DeviceConnected: " + device.name);
    }

    void OnDeviceDisconnected(InputDevice device)
    {
        AddEventToQueue("DeviceDisconnected: " + device.name);
    }

    void OnDeviceConfigChanged(InputDevice device)
    {
        AddEventToQueue("DeviceConfigChanged: " + device.name);
    }

    void AddEventToQueue(string EventDescriptor)
    {
        if (fillQueueOnlyOnce && (m_EventNumber >= m_QueueMaximumSize))
        {
            return;
        }

        string displayTextAccumulator = "";

        EventDescriptor = "<" + m_EventNumber + "> " + EventDescriptor;
        m_EventNumber++;
        m_Events.Enqueue(EventDescriptor);

        while (m_Events.Count > m_QueueMaximumSize)
        {
            m_Events.Dequeue();
        }

        // Print events
        foreach (string eventString in m_Events)
        {
            displayTextAccumulator = (eventString + "\n") + displayTextAccumulator;
        }
        eventsText.text = displayTextAccumulator;
    }
}
