using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

public class TrackingStateBrokenOut : MonoBehaviour
{
    public InputDevice Device;

    public Text PositionDataText;
    public Text VelocityDataText;
    public Text AccelerationDataText;
    public Text RotationDataText;
    public Text AngularVelocityDataText;
    public Text AngularAccelerationDataText;

    private InputFeatureUsage<uint> TrackingStateUsage;

    void Start() { TrackingStateUsage = new InputFeatureUsage<uint>("TrackingState"); }
    
    void Update()
    {
        uint State;

        if (!Device.isValid || !Device.TryGetFeatureValue(TrackingStateUsage, out State))
            return;

        PositionDataText.text = ((State & 0x1) != 0) ? "true" : "false";
        RotationDataText.text = ((State & 0x2) != 0) ? "true" : "false";

        VelocityDataText.text = ((State & 0x4) != 0) ? "true" : "false";
        AngularVelocityDataText.text = ((State & 0x8) != 0) ? "true" : "false";

        AccelerationDataText.text = ((State & 0x10) != 0) ? "true" : "false";
        AngularAccelerationDataText.text = ((State & 0x20) != 0) ? "true" : "false";
    }
}
