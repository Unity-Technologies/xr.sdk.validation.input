using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public abstract class ControlTest
{
    public InputDevice DeviceUnderTest { get; protected set; }
    public InputFeatureUsage FeatureUsageUnderTest { get; protected set; }
    public string Description { get; protected set; }
    public string CertReqID { get; protected set; }
    
    private bool manualPass = false;
    private bool skip = false;

    public TestItemUIManager UIManager;

    protected Check[] Checks;

    public ControlTest(InputDevice device, InputFeatureUsage usage)
    {
        DeviceUnderTest = device;
        FeatureUsageUnderTest = usage;
    }

    public void Skip()
    {
        skip = true;
    }

    public void ManualPass()
    {
        manualPass = true;
    }

    // Run the checks, which should each be a single testable property or characteristic
    // Returns true on complete
    public bool RunChecks()
    {
        bool AllFinished = true;

        for (int i = 0; i < Checks.Length; i++)
        {
            if (Checks[i].Passed || Checks[i].ForcedFail)
                continue;
            else {
                Checks[i].RunCheck();
                AllFinished = false;
            }
        }
        
        return AllFinished;
    }

    public bool AllChecksPassed()
    {
        if (manualPass)
            return true;

        if (skip)
            return false;

        bool AllPassed = true;

        for (int i = 0; i < Checks.Length; i++)
        {
            if (!Checks[i].Passed || Checks[i].ForcedFail)
            {
                AllPassed = false;
                break;
            }
        }

        return AllPassed;
    }

    public void HandleForcedFail(Check sender)
    {
        Debug.Log("Forced Failure on device " + sender.DeviceUnderTest.name 
            + "\n Feature: " + sender.FeatureUsageUnderTest.name
            + "\n Test: " + sender.ParentControlTest);
    }

    public string GetPrintableDescription()
    {
        string retVal = GetType().ToString()
            + "\nCertID #" + CertReqID
            + "\n" + Description
            + "\n";

        for (int i = 0; i < Checks.Length; i++)
        {
            if (Checks[i].Passed)
                retVal += "\n [P] " + Checks[i].SuccessConditionDescription;
            else if (Checks[i].ForcedFail)
                retVal += "\n [F] " + Checks[i].SuccessConditionDescription;
            else
                retVal += "\n [_] " + Checks[i].SuccessConditionDescription;
        }

        return retVal;
    }
}
