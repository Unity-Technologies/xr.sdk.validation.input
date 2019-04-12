using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.SceneManagement;

public class TestFacilitator : MonoBehaviour
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

    private bool m_WaitForContinue;
    string m_Description = "Describe your test here";
    string m_StatusDetails = "None";
    
    public IEnumerator RunTest()
    {
        instructionCanvas.Instructions.text = "Activate one of the control buttons below by pressing the corresponding keyboard key or looking directly at it.";
        m_WaitForContinue = true;
        
        while (overallStatus == OverallTestStatus.NotRun && m_WaitForContinue)
            yield return null;

        if (overallStatus == OverallTestStatus.NotRun)
            RecordStatus(OverallTestStatus.Passed, "Continue was successfully triggered");
    }

    private void RecordStatus(OverallTestStatus OverallStatus, string StatusDetails)
    {
        overallStatus = OverallStatus;
        m_StatusDetails = StatusDetails;
    }

    public string GetTestStatus()
    {
        return "General Status: " + overallStatus +
            "\nTest Description: " + m_Description +
            "\nStatus Details: " + m_StatusDetails;
    }
    
    public void Continue()
    {
        m_WaitForContinue = false;
    }

    public void Skip(string StatusDetails)
    {
        RecordStatus(OverallTestStatus.Skipped, StatusDetails);
        this.StopAllCoroutines();
    }
    
    public void Inconclusive(string StatusDetails)
    {
        RecordStatus(OverallTestStatus.Inconclusive, StatusDetails);
        this.StopAllCoroutines();
    }

    public void Fail(string StatusDetails)
    {
        RecordStatus(OverallTestStatus.Failed, StatusDetails);
        this.StopAllCoroutines();
    }
}
