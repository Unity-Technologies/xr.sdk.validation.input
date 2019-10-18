using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Bearings : MonoBehaviour
{
    public GameObject Direction;
    public ParticleSystem VelocityParticlesXYZ;
    public GameObject Coordinates;
    public GameObject AngularVelocityOrbit;
    public GameObject Acceleration;
    public GameObject AngularAccelerationVisuals;

    // When adding a new bearing, make sure to update this!
    public void HideAll()
    {
        EnableDirection = false;
        EnableXYZVelocityParticles = false;
        EnableCoordinates = false;
        EnableAcceleration = false;
        DisableAngularVelocityOrbit();
        DisableAngularAcceleration();
    }

    public bool EnableDirection { set { Direction.SetActive(value); } }
    public bool EnableXYZVelocityParticles { set { VelocityParticlesXYZ.gameObject.SetActive(value); } }
    public bool EnableCoordinates { set { Coordinates.SetActive(value); } }
    public bool EnableAcceleration { set { Acceleration.SetActive(value); } }

    public void EnableAngularVelocityOrbit(InputDevice device, InputFeatureUsage<Vector3> usage)
    {
        AngularVelocityOrbit.SetActive(true);
        AngularVelocityOrbit.GetComponent<AngularVelocityOrbit>().Visualizer.SetDrivers(device, usage);
    }

    public void DisableAngularVelocityOrbit()
    {
        AngularVelocityOrbit.SetActive(false);
    }

    public void EnableAngularAcceleration(InputDevice device, InputFeatureUsage<Vector3> usage)
    {
        AngularAccelerationVisuals.SetActive(true);
        AngularAccelerationVisuals.GetComponent<Vector3DrivesTextMesh>().SetDrivingUsage(device, usage);
    }

    public void DisableAngularAcceleration()
    {
        AngularAccelerationVisuals.SetActive(false);
    }

    void Start()
    {
        HideAll();
        EnableDirection = true;
    }
}
