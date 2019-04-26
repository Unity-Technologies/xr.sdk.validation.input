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

    // When adding a new bearing, make sure to update this!
    public void HideAll()
    {
        EnableDirection = false;
        EnableXYZVelocityParticles = false;
        EnableCoordinates = false;
        DisableAngularVelocityOrbit();
    }

    public bool EnableDirection { set { Direction.SetActive(value); } }
    public bool EnableXYZVelocityParticles { set { VelocityParticlesXYZ.gameObject.SetActive(value); } }
    public bool EnableCoordinates { set { Coordinates.SetActive(value); } }

    public void EnableAngularVelocityOrbit(InputDevice device, InputFeatureUsage<Vector3> usage)
    {
        AngularVelocityOrbit.SetActive(true);
        AngularVelocityOrbit.GetComponent<AngularVelocityOrbit>().Visualizer.SetDrivers(device, usage);
    }

    public void DisableAngularVelocityOrbit()
    {
        AngularVelocityOrbit.SetActive(false);
    }

    void Start()
    {
        HideAll();
        EnableDirection = true;
    }
}
