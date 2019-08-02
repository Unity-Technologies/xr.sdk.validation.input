using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class ControlTestAngularAcceleration : ControlTest
{
    GameObject m_Visualizer = null;
    Bearings m_Bearings = null;
    GraphFromVector3FeatureUsage m_Graph = null;

    public ControlTestAngularAcceleration(InputDevice device, InputFeatureUsage usage) : base(device, usage)
    {
        Description = "A visualizer has been added to the device under test.  Hold the device still and rotate it to match the oscillation of the X, Y, and Z cubes.  Angular Acceleration magnitude for the axis of rotation under test should be around 20-30, while the other two should remain <10." +
            "\n\nHold the device still. Angular acceleration should be approximately zero in all dimensions. <10 is sufficient.";

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
        m_Visualizer = Object.Instantiate(TestManager.TrackedDeviceVisualizerPointerPrefab);
        m_Visualizer.transform.SetParent(TestManager.XRRig.transform);
        TrackedDevice Device = m_Visualizer.GetComponent<TrackedDevice>();
        Device.device = DeviceUnderTest;
        //Device.rotationUsage = FeatureUsageUnderTest.As<Quaternion>();

        m_Visualizer.transform.localPosition = Vector3.forward;
        
        Quaternion checkIfRotationIsPresent;
        if (DeviceUnderTest.TryGetFeatureValue(CommonUsages.deviceRotation, out checkIfRotationIsPresent))
            Device.rotationUsage = CommonUsages.deviceRotation;
        else if (DeviceUnderTest.TryGetFeatureValue(CommonUsages.colorCameraRotation, out checkIfRotationIsPresent))
            Device.rotationUsage = CommonUsages.colorCameraRotation;
        Vector3 checkIfPositionIsPresent;
        if (DeviceUnderTest.TryGetFeatureValue(CommonUsages.devicePosition, out checkIfPositionIsPresent))
            Device.positionUsage = CommonUsages.devicePosition;
        else if (DeviceUnderTest.TryGetFeatureValue(CommonUsages.colorCameraPosition, out checkIfPositionIsPresent))
            Device.positionUsage = CommonUsages.colorCameraPosition;

        // Set up bearings
        m_Bearings = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().bearings;
        if (m_Bearings != null)
            m_Bearings.EnableAngularAcceleration(DeviceUnderTest, FeatureUsageUnderTest.As<Vector3>());

        m_Graph = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<DeviceTestManager>().graphVector3;
        if (m_Graph != null)
        {
            m_Graph.gameObject.SetActive(true);
            m_Graph.SetActive(DeviceUnderTest, FeatureUsageUnderTest.As<Vector3>());
        }
    }

    public override void Teardown()
    {
        if (m_Bearings != null)
            m_Bearings.DisableAngularAcceleration();

        if (m_Graph != null) {
            m_Graph.SetInactive();
            m_Graph.gameObject.SetActive(false);
        }
        Object.Destroy(m_Visualizer);
    }
}
