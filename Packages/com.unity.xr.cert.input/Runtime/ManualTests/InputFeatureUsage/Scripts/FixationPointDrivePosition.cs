using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class FixationPointDrivePosition : MonoBehaviour
{
    public InputDevice? device = null;
    public InputFeatureUsage eyeFeatureUsage;
    public bool showPosition = false;
    public TextMesh ReportText;

    private Transform myTransform;

    void Start()
    {
        myTransform = transform; // cache a reference to transform
    }

    // Update is called once per frame
    void Update()
    {
        Eyes eyes;
        Vector3 FixationPosition;
        if (device != null)
        {
            if (((InputDevice)device).TryGetFeatureValue(eyeFeatureUsage.As<Eyes>(), out eyes) && eyes.TryGetFixationPoint(out FixationPosition))
            {
                myTransform.position = FixationPosition;
                if (showPosition)
                    ReportText.text = "FixationPoint\n" + myTransform.localPosition;
            }
        }
    }
}
