using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.SceneManagement;

namespace Tests
{
    public class InputManual
    {
        [UnityTest]
        [Description("This test should load the scene, print a debug statement, and succeed")]
        public IEnumerator InputHaptics()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputHaptics");
        }
    }
}
