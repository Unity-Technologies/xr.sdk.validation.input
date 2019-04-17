using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;

public class RecenterInformation : MonoBehaviour
{
    public Text currentTrackingSpaceType;
    public Text behavioralDescription;

    void Start()
    {
        PopulateText();
    }
    
    public void PopulateText()
    {
        currentTrackingSpaceType.text = XRDevice.GetTrackingSpaceType().ToString();

        switch (XRDevice.GetTrackingSpaceType())
        {
            case TrackingSpaceType.Stationary:
                behavioralDescription.text = "Activating Recenter corrects your position and yaw.\n" +
                    "To Verify:" +
                    "\n- Move away from your starting position if possible." +
                    "\n- Look away from +Z" +
                    "\n- Recenter" +
                    "\n- Verify that your position is reset and your \"forward\" direction aligns with +Z";
                break;
            default:
                behavioralDescription.text = "Recenter should not do anything.  Verify that activating recenter does not change your position or rotation.";
                break;
        }
    }
}
