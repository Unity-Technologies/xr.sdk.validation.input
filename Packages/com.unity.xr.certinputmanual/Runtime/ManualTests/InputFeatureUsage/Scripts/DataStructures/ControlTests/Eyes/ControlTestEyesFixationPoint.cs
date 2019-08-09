using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestEyesFixationPoint : ControlTest
{
    GameObject m_Visualizer = null;

    public ControlTestEyesFixationPoint(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "A visualizer has been created to show the eye fixation point reported by the device.  Look around and verify that this fixation point is correct.";
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

        // Set up visualizer
        m_Visualizer = Object.Instantiate(TestManager.FixationPointVisualizer);
        FixationPointDrivePosition Device = m_Visualizer.GetComponent<FixationPointDrivePosition>();
        Device.device = DeviceUnderTest;
        Device.eyeFeatureUsage = FeatureUsageUnderTest;

        // Show coordinates
        TestManager.EyeHelper.GetComponent<EyeInformation>().SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest);
        TestManager.EyeHelper.SetActive(true);
    }

    public override void Teardown()
    {
        Object.Destroy(m_Visualizer);
        
        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().EyeHelper.SetActive(false);
    }
}
