using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ControlItemUIManager : MonoBehaviour
{
    public Image statusImage;
    public Text featureName;
    public Text usageName;

    public Sprite StatusUntested;
    public Sprite StatusInProgress;
    public Sprite StatusPassed;
    public Sprite StatusFail;

    public void OnEnable()
    {
        SetStatusUntested();
    }

    public void SetFeatureName(string name)
    {
        featureName.text = name;
    }

    public void SetUsageName(string name)
    {
        usageName.text = name;
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
