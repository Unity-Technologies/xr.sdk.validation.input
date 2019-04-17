using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.XR;

namespace Tests
{
    public class InputAutomatic
    {
        bool ContainsFeatureWithName(List<InputFeatureUsage> features, string name)
        {
            for (int i = 0; i < features.Count; i++)
            {
                if (features[i].name == name)
                    return true;
            }

            return false;
        }

        [Test]
        [Description("This test verifies the Related Usage Definitions Section of the Input Rules document.")]
        public void RelatedUsageDefinitions()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                List<InputFeatureUsage> Features = new List<InputFeatureUsage>();
                Devices[i].TryGetFeatureUsages(Features);
                if (Features.Count == 0)
                    break;

                // If either PrimaryTouch or SecondaryButton exists
                // then PrimaryButton must exist
                if (ContainsFeatureWithName(Features, "PrimaryTouch") 
                    || ContainsFeatureWithName(Features, "SecondaryButton")
                    )
                    Assert.IsTrue(ContainsFeatureWithName(Features, "PrimaryButton"));

                // If SecondaryTouch exists
                // then PrimaryTouch must exist
                if (ContainsFeatureWithName(Features, "SecondaryTouch"))
                    Assert.IsTrue(ContainsFeatureWithName(Features, "PrimaryTouch"));

                // If SecondaryTouch exists
                // then SecondaryButton must exist
                if (ContainsFeatureWithName(Features, "SecondaryTouch"))
                    Assert.IsTrue(ContainsFeatureWithName(Features, "SecondaryButton"));

                // If Primary2DAxisTouch, Primary2DAxisClick, or Secondary2DAxis exist
                // then Primary2DAxis must exist
                if (ContainsFeatureWithName(Features, "Primary2DAxisTouch") 
                    || ContainsFeatureWithName(Features, "Primary2DAxisClick")
                    || ContainsFeatureWithName(Features, "Secondary2DAxis")
                    )
                    Assert.IsTrue(ContainsFeatureWithName(Features, "Primary2DAxis"));

                // If either Trigger or TriggerButton exist then both must exist.
                Assert.IsTrue(!(ContainsFeatureWithName(Features, "Trigger") ^ ContainsFeatureWithName(Features, "TriggerButton")));

                // If either Grip or GripButton exist then both must exist.
                Assert.IsTrue(!(ContainsFeatureWithName(Features, "Grip") ^ ContainsFeatureWithName(Features, "GripButton")));

            }
        }

        [Test]
        [Description("This test verifies that a HMD/Generic device has the correct tracking usages.")]
        public void TrackingUsagesRoleGeneric()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].role == InputDeviceRole.Generic)
                {
                    List<InputFeatureUsage> Features = new List<InputFeatureUsage>();
                    Devices[i].TryGetFeatureUsages(Features);

                    Assert.IsTrue(ContainsFeatureWithName(Features, "DeviceRotation"));
                    Assert.IsTrue(ContainsFeatureWithName(Features, "LeftEyeRotation"));
                    Assert.IsTrue(ContainsFeatureWithName(Features, "RightEyeRotation"));
                    Assert.IsTrue(ContainsFeatureWithName(Features, "CenterEyeRotation"));
                }
            }
        }

        [Test]
        [Description("This test verifies that a TrackingReference device has the correct tracking usages.")]
        public void TrackingUsagesRoleTrackingReference()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].role == InputDeviceRole.TrackingReference)
                {
                    List<InputFeatureUsage> Features = new List<InputFeatureUsage>();
                    Devices[i].TryGetFeatureUsages(Features);

                    Assert.IsTrue(ContainsFeatureWithName(Features, "DevicePosition"));
                    Assert.IsTrue(ContainsFeatureWithName(Features, "DeviceRotation"));
                }
            }
        }

        [Test]
        [Description("This test verifies that a HardwareTracker device has the correct tracking usages.")]
        public void TrackingUsagesRoleHardwareTracker()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                if (Devices[i].role == InputDeviceRole.HardwareTracker)
                {
                    List<InputFeatureUsage> Features = new List<InputFeatureUsage>();
                    Devices[i].TryGetFeatureUsages(Features);

                    Assert.IsTrue(ContainsFeatureWithName(Features, "DevicePosition"));
                    Assert.IsTrue(ContainsFeatureWithName(Features, "DeviceRotation"));
                }
            }
        }

        [Test]
        [Description("This test verifies that a tracked device contatins the minimum set of features.")]
        public void TrackinUsagesDeviceDefinition()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                List<InputFeatureUsage> Features = new List<InputFeatureUsage>();
                Devices[i].TryGetFeatureUsages(Features);

                if (ContainsFeatureWithName(Features, "IsTracked")
                    || ContainsFeatureWithName(Features, "TrackingState")
                    || ContainsFeatureWithName(Features, "DevicePosition")
                    || ContainsFeatureWithName(Features, "DeviceRotation")
                    || ContainsFeatureWithName(Features, "DeviceVelocity")
                    || ContainsFeatureWithName(Features, "DeviceAngularVelocity")
                    || ContainsFeatureWithName(Features, "DeviceAcceleration")
                    || ContainsFeatureWithName(Features, "DeviceAngularAcceleration")
                    )
                    Assert.IsTrue(ContainsFeatureWithName(Features, "IsTracked")
                    && ContainsFeatureWithName(Features, "TrackingState")
                    && (ContainsFeatureWithName(Features, "DevicePosition")
                    || ContainsFeatureWithName(Features, "DeviceRotation")
                    || ContainsFeatureWithName(Features, "DeviceVelocity")
                    || ContainsFeatureWithName(Features, "DeviceAngularVelocity")
                    || ContainsFeatureWithName(Features, "DeviceAcceleration")
                    || ContainsFeatureWithName(Features, "DeviceAngularAcceleration")
                    )
                    );
            }
        }

        [Test]
        [Description("This test verifies that a tracked device contatins the minimum set of features.")]
        public void HapticCapabilitiesSanityCheck()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                HapticCapabilities hapticCapabilities;

                if (!Devices[i].TryGetHapticCapabilities(out hapticCapabilities))
                    break;

                if (hapticCapabilities.supportsBuffer) {
                    Assert.IsTrue(hapticCapabilities.bufferFrequencyHz > 0);
                    Assert.IsTrue(hapticCapabilities.bufferOptimalSize > 0);
                    Assert.IsTrue(hapticCapabilities.bufferOptimalSize <= hapticCapabilities.bufferMaxSize);
                    Assert.IsTrue(hapticCapabilities.bufferOptimalSize <= 4096); // kUnityXRMaxHapticBufferSize
                }
                else
                {
                    Assert.Equals(0, hapticCapabilities.bufferFrequencyHz);
                    Assert.Equals(0, hapticCapabilities.bufferOptimalSize);
                    Assert.Equals(0, hapticCapabilities.bufferMaxSize);
                }
            }
        }

        [Test]
        [Description("This test verifies that a tracked device contatins the minimum set of features.")]
        public void UsagesNoRepeats()
        {
            List<InputDevice> Devices = new List<InputDevice>();
            InputDevices.GetDevices(Devices);

            Assert.AreNotEqual(0, Devices.Count, "No devices found");

            for (int i = 0; i < Devices.Count; i++)
            {
                List<InputFeatureUsage> Features = new List<InputFeatureUsage>();
                Devices[i].TryGetFeatureUsages(Features);

                for (int j = i + 1; j < Devices.Count; j++)
                {
                    Assert.AreNotEqual(Features[i].name, Features[j].name);
                }
            }
        }
    }
}
