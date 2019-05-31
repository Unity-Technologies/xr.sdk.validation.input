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
        [Timeout(3600000)] // An hour
        [Description("This test verifies haptic response with a physical controller.")]
        public IEnumerator InputHaptics()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputHaptics");
        }

        [UnityTest]
        [Timeout(3600000)] // An hour
        [Description("This test verifies Recenter functionality.")]
        public IEnumerator InputRecenter()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputRecenter");
        }

        [UnityTest]
        [Timeout(3600000)] // An hour
        [Description("This test verifies Feature/Usage controls specifics.")]
        public IEnumerator InputFeatureUsageControls()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputFeatureUsageControls");
        }

        [UnityTest]
        [Timeout(3600000)] // An hour
        [Description("This test verifies Feature/Usage tracking specifics.")]
        public IEnumerator InputFeatureUsageTracking()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("InputFeatureUsageTracking");
        }
    }
}
