using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestRotation : ControlTest
{
    GameObject m_Visualizer = null;
    QuaternionDrivesTextMesh m_TextOnFace;

    public ControlTestRotation(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "A visualizer has been added to the device under test.  Rotate the device and verify that the device's virtual rotation matches the device's actual rotation.  If the device under test is a headset, turning your head to view the +X, -X, +Y, -Y, +Z, -Z blocks and seeing the screen update 1:1 with your head movement is sufficient.";

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

        if ((DeviceUnderTest.characteristics & InputDeviceCharacteristics.HeadMounted) == 0)
        {
            // Set up visualizer
            m_Visualizer = Object.Instantiate(TestManager.TrackedDeviceVisualizerRotationPrefab);
            m_Visualizer.transform.SetParent(TestManager.XRRig.transform);
            TrackedDevice Device = m_Visualizer.GetComponent<TrackedDevice>();
            Device.device = DeviceUnderTest;
            Device.rotationUsage = FeatureUsageUnderTest.As<Quaternion>();

            m_Visualizer.transform.localPosition = Vector3.forward;
        
            Vector3 trash;
            if (DeviceUnderTest.TryGetFeatureValue(CommonUsages.devicePosition, out trash))
                Device.positionUsage = CommonUsages.devicePosition;
            else if (DeviceUnderTest.TryGetFeatureValue(CommonUsages.colorCameraPosition, out trash))
                Device.positionUsage = CommonUsages.colorCameraPosition;
        }
        else
        {
            m_TextOnFace = TestManager.rotationTextOnFace;
            m_TextOnFace.SetDrivingUsage(DeviceUnderTest, FeatureUsageUnderTest.As<Quaternion>());
        }
    }

    public override void Teardown()
    {
        if ((DeviceUnderTest.characteristics & InputDeviceCharacteristics.HeadMounted) == 0)
            Object.Destroy(m_Visualizer);
        else
            m_TextOnFace.gameObject.SetActive(false);
    }
}
