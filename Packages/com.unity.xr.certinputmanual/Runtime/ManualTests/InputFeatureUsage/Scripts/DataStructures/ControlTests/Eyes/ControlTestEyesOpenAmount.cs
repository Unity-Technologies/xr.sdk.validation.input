using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestEyesOpenAmount : ControlTest
{
    public ControlTestEyesOpenAmount(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Look at the eye information panel. Manually verify that the left and right eye open amounts react correctly as you open and close each eye separately.";
        CertReqID = "1.2.3.x";

        Checks = new Check[2];
        Checks[0] = new CheckEyesRange_0_to_1(DeviceUnderTest, FeatureUsageUnderTest, this);
        Checks[1] = new CheckRequireManualPass(DeviceUnderTest, FeatureUsageUnderTest, this);

        for (int i = 0; i < Checks.Length; i++)
        {
            Checks[i].ForcedFailure += HandleForcedFail;
        }
    }

    public override void Setup()
    {
        DeviceTestManager TestManager = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>();

        // Show coordinates
        TestManager.EyeHelper.GetComponent<EyeInformation>().SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest);
        TestManager.EyeHelper.SetActive(true);
    }

    public override void Teardown()
    {
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().EyeHelper.SetActive(false);
    }
}
