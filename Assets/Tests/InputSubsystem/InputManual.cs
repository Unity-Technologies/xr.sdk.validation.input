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
        [Description("This test verifies haptic response with a physical controller.")]
        public IEnumerator InputHaptics()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputHaptics");
        }

        [UnityTest]
        [Description("This test verifies Recenter functionality.")]
        public IEnumerator InputRecenter()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputRecenter");
        }

        [UnityTest]
        [Description("This test verifies Feature/Usage specifics.")]
        public IEnumerator InputFeatureUsage()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputFeatureUsage");
        }
    }
}
