using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestEyesRotation : ControlTest
{
    GameObject m_VisualizerLeft = null;
    GameObject m_VisualizerRight = null;

    public ControlTestEyesRotation(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "A visualizer has been added to both eyes to show their rotation.  It should look like two bars are extruding from below your eyes.  Look around and verify that these rotation indicators update correctly.";
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
        m_VisualizerLeft = Object.Instantiate(TestManager.TrackedDeviceVisualizerEyePointerPrefab);
        m_VisualizerLeft.transform.SetParent(TestManager.XRRig.transform);
        EyePointer EyePointerLeft = m_VisualizerLeft.GetComponent<EyePointer>();
        EyePointerLeft.SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest, EyePointer.LeftOrRightEye.LeftEye);

        m_VisualizerRight = Object.Instantiate(TestManager.TrackedDeviceVisualizerEyePointerPrefab);
        m_VisualizerRight.transform.SetParent(TestManager.XRRig.transform);
        EyePointer EyePointerRight = m_VisualizerRight.GetComponent<EyePointer>();
        EyePointerRight.SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest, EyePointer.LeftOrRightEye.RightEye);

        // Show eye data and coordinates
        TestManager.EyeHelper.GetComponent<EyeInformation>().SetDeviceAndUsage(DeviceUnderTest, FeatureUsageUnderTest);
        TestManager.EyeHelper.SetActive(true);
    }

    public override void Teardown()
    {
        //Object.Destroy(m_VisualizerLeft);
        //Object.Destroy(m_VisualizerRight);

        GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().EyeHelper.SetActive(false);
    }
}
