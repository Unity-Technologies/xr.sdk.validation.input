using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class TrackedDevice : MonoBehaviour
{
    public InputDevice? device = null;
    public InputFeatureUsage<Vector3>? positionUsage = null;
    public Vector3 positionOffset = Vector3.zero;
    public InputFeatureUsage<Quaternion>? rotationUsage = null;
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
        Vector3 position;
        Quaternion rotation;
        if (device != null)
        {
            if (positionUsage != null) {
                ((InputDevice)device).TryGetFeatureValue((InputFeatureUsage<Vector3>)positionUsage, out position);
                myTransform.localPosition = position + positionOffset;
                if (showPosition)
                    ReportText.text = "DevicePosition\n" + myTransform.localPosition;
            }
            if (rotationUsage != null) {
                ((InputDevice)device).TryGetFeatureValue((InputFeatureUsage<Quaternion>)rotationUsage, out rotation);
                myTransform.rotation = rotation;
            }
        }
    }
}
