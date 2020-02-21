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
            // Disable Automatic Test Running
            var TestRunnerIndicator = new GameObject();
            TestRunnerIndicator.AddComponent<TestRunnerIsRunning>();
            DontDestroyOnLoad(TestRunnerIndicator);

            SceneManager.LoadScene(SceneName);
            yield return null;

            // Clean up indicator
            Destroy(TestRunnerIndicator);

            TestFacilitator facilitator = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<TestFacilitator>();
            yield return facilitator.LaunchRunTestWhenReady();
            Debug.Log(facilitator.GetTestStatus());
            Assert.IsTrue(facilitator.overallStatus == TestFacilitator.OverallTestStatus.Passed, "Overall Status Passed?");
        }
    }

    // This is just used as an indicator for TestHelperFunctions.RunFacilitatorSceneTest().  Don't use it anywhere else!
    public class TestRunnerIsRunning : MonoBehaviour { }
}
