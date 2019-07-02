using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Unity.TestRunnerManualTests;

[PrebuildSetup(typeof(BuildSetupAndCleanup))]
[PostBuildCleanup(typeof(BuildSetupAndCleanup))]
public class TemplateManual
{
    [UnityTest]
    [Description("This test should load the scene, print a debug statement, and succeed")]
    public IEnumerator TestScene()
    {
        // The scene used here is set up in BuildSetupAndCleanup.cs
        yield return TestHelperFunctions.RunFacilitatorSceneTest("SampleTestScene");
    }

    [UnityTest]
    [Description("This test should load the scene, print a debug statement, and succeed")]
    public IEnumerator TestScene2()
    {
        // The scene used here is set up in BuildSetupAndCleanup.cs
        yield return TestHelperFunctions.RunFacilitatorSceneTest("SampleTestScene");
    }
}
