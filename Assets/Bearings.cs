using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearings : MonoBehaviour
{
    public GameObject Direction;
    public ParticleSystem VelocityParticlesXYZ;

    public bool EnableDirection { set { Direction.gameObject.SetActive(value); } }
    public bool EnableXYZVelocityParticles { set { VelocityParticlesXYZ.gameObject.SetActive(value); } }

    public void HideAll()
    {
        EnableDirection = false;
        EnableXYZVelocityParticles = false;
    }

    void Start()
    {
        HideAll();
        EnableDirection = true;
    }
}
