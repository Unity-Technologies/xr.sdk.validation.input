using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.SceneManagement;

namespace Tests
{
    public class TemplateManual
    {
        [UnityTest]
        [Description("This test should load the scene, print a debug statement, and succeed")]
        public IEnumerator TestScene()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("TemplateScene");
        }

        [UnityTest]
        [Description("This test should load the scene, print a debug statement, and succeed")]
        public IEnumerator TestScene2()
        {
            yield return TestFunctions.RunFacilitatorSceneTest("TemplateScene");
        }
    }
}
