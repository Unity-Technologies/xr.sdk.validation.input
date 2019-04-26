using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        UpdateOrientation();
    }

    public void UpdateOrientation()
    {
        Vector3 LookRotation = (transform.position - Camera.main.transform.position).normalized;

        if (LookRotation != Vector3.zero)
            transform.forward = LookRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOrientation();
    }
}
