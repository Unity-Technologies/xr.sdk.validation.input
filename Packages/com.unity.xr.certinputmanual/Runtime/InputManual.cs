﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.SceneManagement;
using Unity.TestRunnerManualTests;

public class InputManual
{
    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies haptic response with a physical controller.")]
    public IEnumerator InputHaptics()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputHaptics");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies non-tracking Feature/Usage details.")]
    public IEnumerator InputFeatureUsageControls()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputFeatureUsageControls");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies tracking Feature/Usage details.")]
    public IEnumerator InputFeatureUsageTracking()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputFeatureUsageTracking");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies Device names, manufacturer, and serial number information.")]
    public IEnumerator InputMetaData()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputMetaData");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies information that requires viewing all features at once.")]
    public IEnumerator InputArray()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputArray");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies information that requires viewing all features at once.")]
    public IEnumerator InputArrayForInputSystem()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputArrayForInputSystem");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies tracking information that requires viewing all features at once.")]
    public IEnumerator InputArrayForTracking()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputArrayForTracking");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies tracking information that requires viewing all features at once.")]
    public IEnumerator InputConnectDisconnectEvents()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputConnectDisconnectEvents");
    }
        
    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies TrackingOriginType and Boundary functions.")]
    public IEnumerator InputExperience()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputExperience");
    }
}
