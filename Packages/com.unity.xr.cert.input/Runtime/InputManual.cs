using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.SceneManagement;
using Unity.TestRunnerManualTests;

[PrebuildSetup(typeof(BuildSetupAndCleanup))]
[PostBuildCleanup(typeof(BuildSetupAndCleanup))]
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
    [Description("This test verifies Feature/Usage controls specifics.")]
    public IEnumerator InputFeatureUsageControls()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputFeatureUsageControls");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies Feature/Usage tracking specifics.")]
    public IEnumerator InputFeatureUsageTracking()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputFeatureUsageTracking");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies Device names, manufacturer, and serial number information.")]
    public IEnumerator NameManfSerial()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("NameManfSerial");
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
    [Description("This test verifies TrackingOriginType and Boundary functions.")]
    public IEnumerator InputExperience()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputExperience");
    }

    [UnityTest]
    [Timeout(3600000)] // An hour
    [Description("This test verifies Recentering functionality.")]
    public IEnumerator InputExperienceRecenter()
    {
        yield return TestHelperFunctions.RunFacilitatorSceneTest("InputExperienceRecenter");
    }
}
