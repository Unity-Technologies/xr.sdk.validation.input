using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.XR;
using UnityEngine.UI;

public class InputSubsystemPanel : MonoBehaviour
{
    public Text currentOriginType;
    public Text availableOriginType;
    public Text displayName;
    public Text subsystemNumber;

    private int m_SystemNumber = -1;
    public int SystemNumber
    {
        get { return m_SystemNumber; }
        set
        {
            m_SystemNumber = value;
            subsystemNumber.text = value.ToString();
        }
    }

    private XRInputSubsystem m_InputSubsystem;
    public XRInputSubsystem InputSubsystem
    {
        get { return m_InputSubsystem; }
        set
        {
            m_InputSubsystem = value;
            displayName.text = m_InputSubsystem.ToString();
            UpdateCurrentOriginType();
            UpdateAvailableOriginType();
        }
    }

    public void UpdateCurrentOriginType()
    {
        currentOriginType.text = m_InputSubsystem.GetTrackingOriginMode().ToString();
    }

    public void UpdateAvailableOriginType()
    {
        string accumulation = "";

        TrackingOriginModeFlags AvailableOrigins = m_InputSubsystem.GetSupportedTrackingOriginModes();

        Debug.Log(AvailableOrigins);

        foreach (TrackingOriginModeFlags type in Enum.GetValues(typeof(TrackingOriginModeFlags)))
        {
            if ((AvailableOrigins & type) != 0)
                accumulation += type.ToString() + ", ";
        }

        availableOriginType.text = accumulation;
    }

    public void TrySetOriginTypeDevice()
    {
        m_InputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Device);
        UpdateCurrentOriginType();
    }

    public void TrySetOriginTypeFloor()
    {
        m_InputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
        UpdateCurrentOriginType();
    }

    public void TrySetOriginTypeTrackingReference()
    {
        m_InputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.TrackingReference);
        UpdateCurrentOriginType();
    }

    public void TrySetOriginTypeUnknown()
    {
        m_InputSubsystem.TrySetTrackingOriginMode(TrackingOriginModeFlags.Unknown);
        UpdateCurrentOriginType();
    }
}
