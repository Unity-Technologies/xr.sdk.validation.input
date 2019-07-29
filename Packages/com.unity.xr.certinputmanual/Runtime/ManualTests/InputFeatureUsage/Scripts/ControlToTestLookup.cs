using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public static class ControlToTestLookup
{
    public static void LookupControlTests(InputDevice device, InputFeatureUsage usage, out List<ControlTest> tests)
    {
        tests = new List<ControlTest>();

        // Add usage type tests.

        if (usage.type == typeof(bool))
            AddBinaryTests(device, usage, tests);
        else if (usage.type == typeof(uint))
            AddUintTests(device, usage, tests);
        else if (usage.type == typeof(float))
            AddAxis1DTests(device, usage, tests);
        else if (usage.type == typeof(Vector2))
            AddAxis2DTests(device, usage, tests);
        else if (usage.type == typeof(Vector3))
            AddAxis3DTests(device, usage, tests);
        else if (usage.type == typeof(Quaternion))
            AddRotationTests(device, usage, tests);
        else if (usage.type == typeof(Eyes))
            AddEyesTests(device, usage, tests);
        else
            AddNotSupportedTypeTests(device, usage, tests);

        // Add SI unit test

        tests.Add(new ControlTestSIUnits(device, usage));

        // Add usage specific tests

        switch (usage.name)
        {
            case "IsTracked":
                tests.Add(new ControlTestIsTracked(device, usage));
                break;
            case "Trigger":
            case "Grip":
            case "IndexTouch":
            case "ThumbTouch":
            case "IndexFinger":
            case "MiddleFinger":
            case "RingFinger":
            case "PinkyFinger":
            case "BatteryLevel":
                tests.Add(new ControlTest1DRange_0_1(device, usage));
                break;
            case "CombinedTrigger":
                tests.Add(new ControlTest1DRange_neg1_1(device, usage));
                break;
            case "Primary2DAxis":
            case "Secondary2DAxis":
            case "DPad":
                tests.Add(new ControlTest2DRange_neg1_1(device, usage));
                break;
            case "DevicePosition":
            case "ColorCameraPosition":
            case "LeftEyePosition":
            case "RightEyePosition":
            case "CenterEyePosition":
                tests.Add(new ControlTestPosition(device, usage));
                break;
            case "DeviceRotation":
            case "ColorCameraRotation":
            case "LeftEyeRotation":
            case "RightEyeRotation":
            case "CenterEyeRotation":
                tests.Add(new ControlTestRotation(device, usage));
                break;
            case "DeviceVelocity":
            case "ColorCameraVelocity":
            case "LeftEyeVelocity":
            case "RightEyeVelocity":
            case "CenterEyeVelocity":
                tests.Add(new ControlTestVelocity(device, usage));
                break;
            case "DeviceAngularVelocity":
            case "ColorCameraAngularVelocity":
            case "LeftEyeAngularVelocity":
            case "RightEyeAngularVelocity":
            case "CenterEyeAngularVelocity":
                tests.Add(new ControlTestAngularVelocity(device, usage));
                break;
            case "DeviceAcceleration":
            case "ColorCameraAcceleration":
            case "LeftEyeAcceleration":
            case "RightEyeAcceleration":
            case "CenterEyeAcceleration":
                tests.Add(new ControlTestAcceleration(device, usage));
                break;
            case "DeviceAngularAcceleration":
            case "ColorCameraAngularAcceleration":
            case "LeftEyeAngularAcceleration":
            case "RightEyeAngularAcceleration":
            case "CenterEyeAngularAcceleration":
                tests.Add(new ControlTestAngularAcceleration(device, usage));
                break;
            default:
                // Otherwise, this usage doesn't have a specific test to add
                break;
        }

    }

    private static void AddBinaryTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestIsBool(device, usage));
        tests.Add(new ControlTestBinaryDefault(device, usage));
        tests.Add(new ControlTestBinaryRange(device, usage));
    }

    private static void AddAxis1DTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestIsFloat(device, usage));
        tests.Add(new ControlTest1DDefault(device, usage));
    }

    private static void AddUintTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestIsUint(device, usage));
        tests.Add(new ControlTestDiscreteDefault(device, usage));
    }

    private static void AddAxis2DTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestIsVector2(device, usage));
        tests.Add(new ControlTest2DDefault(device, usage));
        tests.Add(new ControlTest2DXY(device, usage));
    }

    private static void AddAxis3DTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestIsVector3(device, usage));
        tests.Add(new ControlTest3DDefault(device, usage));
        tests.Add(new ControlTest3DXYZ(device, usage));
    }

    private static void AddRotationTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestIsQuaternion(device, usage));
        tests.Add(new ControlTestRotationDefault(device, usage));
        tests.Add(new ControlTestRotationNormalizedXYZW(device, usage));
    }

    private static void AddEyesTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestEyesDefault(device, usage));
        tests.Add(new ControlTestEyesOpenAmount(device, usage));
        tests.Add(new ControlTestEyesFixationPoint(device, usage));
        tests.Add(new ControlTestEyesPosition(device, usage));
        tests.Add(new ControlTestEyesRotation(device, usage));
    }

    private static void AddNotSupportedTypeTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestUnsupportedType(device, usage));
    }
}
