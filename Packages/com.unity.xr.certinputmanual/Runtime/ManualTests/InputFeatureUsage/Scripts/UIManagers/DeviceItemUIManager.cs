using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DeviceItemUIManager : MonoBehaviour
{
    public Image statusImage;
    public Text deviceName;
    
    public Sprite StatusUntested;
    public Sprite StatusInProgress;
    public Sprite StatusPassed;
    public Sprite StatusFail;

    public void OnEnable()
    {
        SetStatusUntested();
    }

    public void SetDeviceName(string name)
    {
        deviceName.text = name;
    }

    public void SetStatusUntested()
    {
        statusImage.sprite = StatusUntested;
    }

    public void SetStatusInProgress()
    {
        statusImage.sprite = StatusInProgress;
    }

    public void SetStatusTested(bool Success)
    {
        if (Success)
            statusImage.sprite = StatusPassed;
        else
            statusImage.sprite = StatusFail;
    }
}
