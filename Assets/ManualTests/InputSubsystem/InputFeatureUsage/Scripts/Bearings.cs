using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearings : MonoBehaviour
{
    public GameObject Direction;
    public ParticleSystem VelocityParticlesXYZ;
    public GameObject Coordinates;

    public bool EnableDirection { set { Direction.SetActive(value); } }
    public bool EnableXYZVelocityParticles { set { VelocityParticlesXYZ.gameObject.SetActive(value); } }
    public bool EnableCoordinates { set { Coordinates.SetActive(value); } }

    public void HideAll()
    {
        EnableDirection = false;
        EnableXYZVelocityParticles = false;
        EnableCoordinates = false;
    }

    void Start()
    {
        HideAll();
        EnableDirection = true;
    }
}
