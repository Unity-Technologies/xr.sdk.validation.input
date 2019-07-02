using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class InputFeatureUsageContainer
{
    public InputFeatureUsage FeatureUsage;
    public ControlItemUIManager UIManager;
    public bool HaveAllTestsPassed;

    public InputFeatureUsageContainer(InputFeatureUsage feature)
    {
        FeatureUsage = feature;
        HaveAllTestsPassed = false;
    }
}
