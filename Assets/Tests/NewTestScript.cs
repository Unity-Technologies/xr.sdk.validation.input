using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.SceneManagement;

namespace Tests
{
    public class NewTestScript
    {
        [UnityTest]
        //[Timeout(1000)] // timeout in milliseconds
        [Description("This test should load the scene, print a debug statement, and succeed")]
        public IEnumerator TestScene()
        {
            string SceneName = "TestScene";

            SceneManager.LoadScene(SceneName);
            yield return null;

            TestFacilitator facilitator = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<TestFacilitator>();
            yield return facilitator.RunAllTests();
            Debug.Log(facilitator.GetTestStatus());
        }

        [UnityTest]
        //[Timeout(1000)] // timeout in milliseconds
        [Description("This test should load the scene, print a debug statement, and succeed")]
        public IEnumerator TestScene2()
        {
            string SceneName = "TestScene";

            SceneManager.LoadScene(SceneName);
            yield return null;

            TestFacilitator facilitator = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<TestFacilitator>();
            yield return facilitator.RunAllTests();
            Debug.Log(facilitator.GetTestStatus());
        }
    }
}
