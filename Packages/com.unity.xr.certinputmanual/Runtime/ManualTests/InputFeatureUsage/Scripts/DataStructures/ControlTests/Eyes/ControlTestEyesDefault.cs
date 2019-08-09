using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestEyesDefault : ControlTest
{
    public ControlTestEyesDefault(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "Look at the eye information panel. If possible, disable the eye tracking features of the device or use another method to drive values to their defaults. Manually verify that the values are correctly driven to their default values when valid data cannot be found.  Floats should default to 0.0. Vector3s should default to (0, 0, 0). Quaternions should default to the quaternion identity.";
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
        
        TestManager.EyeHelper.GetComponent<EyeInformation>().SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest);
        TestManager.EyeHelper.SetActive(true);
    }

    public override void Teardown()
    {
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().EyeHelper.SetActive(false);
    }
}
