using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.TestRunnerManualTests
{
    public abstract class TestFacilitator : MonoBehaviour
    {
        public InstructionCanvas instructionCanvas;
        public OverallTestStatus overallStatus { get; private set; } = OverallTestStatus.NotRun;

        public enum OverallTestStatus
        {
            NotRun,
            Skipped,
            Inconclusive,
            Failed,
            Passed
        }
        
        protected bool m_WaitForContinue;
        protected string m_StatusDetails = "None";

        // Inheritors should not need to extend this function, so it is private.
        // If you need your scene to do something in awake, do it in another script. TestFacilitators should
        // only be used to drive test logic elsewhere in the scene and record results.
        private void Awake()
        {
            TestRunnerIsRunning Indicator = null;
            Indicator = FindObjectOfType<TestRunnerIsRunning>();

            if (Indicator == null)
            {
                Debug.Log("Did not find that TestRunner is driving this test. Starting the test run separately.");
                StartCoroutine(LaunchRunTestWhenReady());
            }
            else
            {
                Debug.Log("Found that TestRunner is driving this test. No further action required.");
            }
        }

        // Some ssystems need time to initialize.
        public virtual IEnumerator LaunchRunTestWhenReady()
        {
            yield return StartCoroutine(RunTest());
        }

        // This is called by TestRunner scripts.
        // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
        // Please refer to the readme in this project's root folder for more information
        protected abstract IEnumerator RunTest();

        protected void RecordStatus(OverallTestStatus OverallStatus, string StatusDetails)
        {
            overallStatus = OverallStatus;
            m_StatusDetails = StatusDetails;
        }

        public string GetTestStatus()
        {
            return "\n----------------------------------" +
                "\nGeneral Status: " + overallStatus +
                "\nStatus Details: " + m_StatusDetails;
        }

        protected IEnumerator WaitForContinue()
        {
            m_WaitForContinue = true;
            while (overallStatus == OverallTestStatus.NotRun && m_WaitForContinue)
                yield return null;
        }
        
        public void Continue()
        {
            m_WaitForContinue = false;
        }

        public void Skip(string StatusDetails)
        {
            RecordStatus(OverallTestStatus.Skipped, StatusDetails);
        }
        
        public void Inconclusive(string StatusDetails)
        {
            RecordStatus(OverallTestStatus.Inconclusive, StatusDetails);
        }

        public void Fail(string StatusDetails)
        {
            RecordStatus(OverallTestStatus.Failed, StatusDetails);
        }
    }
}
