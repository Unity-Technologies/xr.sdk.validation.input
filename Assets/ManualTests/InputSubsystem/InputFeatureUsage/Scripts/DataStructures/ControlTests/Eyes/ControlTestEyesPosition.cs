using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestEyesPosition : ControlTest
{
    public ControlTestEyesPosition(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Verify that the left eye is a bit farther in the -x direction than the right eye.  Then move around the space and verify that the left and right eye positions update correctly relative to this movement.";
        CertReqID = "1.2.3.x";

        Checks = new Check[1];
        Checks[0] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }

    public override void Setup()
    {
        DeviceTestManager TestManager = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>();

        // Show eye data and coordinates
        TestManager.EyeHelper.GetComponent<EyeInformation>().SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest);
        TestManager.EyeHelper.SetActive(true);
        TestManager.bearings.EnableCoordinates = true;
    }

    public override void Teardown()
    {
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().bearings.EnableCoordinates = false;
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().EyeHelper.SetActive(false);
    }
}
