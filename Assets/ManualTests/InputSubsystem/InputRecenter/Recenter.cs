using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class Recenter : MonoBehaviour
{
    public void DoRecenter()
    {
        InputTracking.Recenter();
    }
}
