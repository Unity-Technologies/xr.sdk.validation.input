using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using NUnit.Framework;

namespace Unity.TestRunnerManualTests
{
    public class TestHelperFunctions : MonoBehaviour
    {
        public static IEnumerator RunFacilitatorSceneTest(string SceneName)
        {
            SceneManager.LoadScene(SceneName);
            yield return null;

            TestFacilitator facilitator = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<TestFacilitator>();
            yield return facilitator.RunTest();
            Debug.Log(facilitator.GetTestStatus());
            Assert.IsTrue(facilitator.overallStatus == TestFacilitator.OverallTestStatus.Passed, "Overall Status Passed?");
        }
    }
}
