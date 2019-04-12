using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected string m_Description = "Describe your test here";
    protected string m_StatusDetails = "None";

    // This is called by TestRunner scripts.
    // Simply entering playmode won't start this, and we don't want it to start twice via Start() or Awake()
    // Please refer to the readme in this project's root folder for more information
    public abstract IEnumerator RunTest();

    protected void RecordStatus(OverallTestStatus OverallStatus, string StatusDetails)
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
