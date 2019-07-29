using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatePosition : MonoBehaviour
{
    public Vector3 relativeOscillationExtreame = Vector3.one;
    public float periodModifier = 1f;

    Transform m_Transform;
    Vector3 m_InitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = transform; // cache transform reference
        m_InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_Transform.position = m_InitialPosition + (Mathf.Cos(Time.time * (2 * Mathf.PI) * periodModifier) * relativeOscillationExtreame);
    }
}
