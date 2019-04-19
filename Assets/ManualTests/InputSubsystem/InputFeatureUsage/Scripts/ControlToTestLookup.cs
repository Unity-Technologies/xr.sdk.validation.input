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
        // TODO: Add any control-specific tests in another switch statement using usage.name(?)
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
        else
            AddNotSupportedTypeTests(device, usage, tests);

        // *** Add usage tests here ***
        //switch(usage.type)
        //{
        //    case "TrackingState":
        //        AddUintTests(device, usage, tests);
        //        break;
        //    case "Trigger":
        //    case "Grip":
        //    case "IndexTouch":
        //    case "ThumbTouch":
        //    case "IndexFinger":
        //    case "MiddleFinger":
        //    case "RingFinger":
        //    case "PinkyFinger":
        //        AddAxis1DTests(device, usage, tests);
        //        break;
        //    case "Primary2DAxis":
        //    case "Secondary2DAxis":
        //        AddAxis2DTests(device, usage, tests);
        //        break;
        //    case "DevicePosition":
        //    case "DeviceVelocity":
        //    case "DeviceAcceleration":
        //    case "DeviceAngularVelocity":
        //    case "DeviceAngularAcceleration":
        //    case "CenterEyePosition":
        //    case "CenterEyeVelocity":
        //    case "CenterEyeAcceleration":
        //    case "CenterEyeAngularVelocity":
        //    case "CenterEyeAngularAcceleration":
        //    case "LeftEyePosition":
        //    case "LeftEyeVelocity":
        //    case "LeftEyeAcceleration":
        //    case "LeftEyeAngularVelocity":
        //    case "LeftEyeAngularAcceleration":
        //    case "RightEyePosition":
        //    case "RightEyeVelocity":
        //    case "RightEyeAcceleration":
        //    case "RightEyeAngularVelocity":
        //    case "RightEyeAngularAcceleration":
        //        AddAxis3DTests(device, usage, tests);
        //        break;
        //    case "DeviceRotation":
        //    case "CenterEyeRotation":
        //    case "LeftEyeRotation":
        //    case "RightEyeRotation":
        //        AddRotationTests(device, usage, tests);
        //        break;
        //    default:
        //        //AddCatchTest
        //        break;
        //}
        
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

    private static void AddNotSupportedTypeTests(InputDevice device, InputFeatureUsage usage, List<ControlTest> tests)
    {
        tests.Add(new ControlTestUnsupportedType(device, usage));
    }
}
