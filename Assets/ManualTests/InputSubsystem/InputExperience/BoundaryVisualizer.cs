using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class BoundaryVisualizer : MonoBehaviour
{
    List<XRInputSubsystem> InputInstances;

    void OnEnable()
    {
        InputInstances = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(InputInstances);

        if (InputInstances.Count == 0)
            Debug.LogError("Error: no input systems!");

        for (int i = 0; i < InputInstances.Count; i++)
        {
            InputInstances[i].boundaryChanged += OnBoundaryChange;
            InputInstances[i].trackingOriginUpdated += OnTrackingOriginUpdated;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < InputInstances.Count; i++)
        {
            InputInstances[i].boundaryChanged -= OnBoundaryChange;
            InputInstances[i].trackingOriginUpdated -= OnTrackingOriginUpdated;
        }
    }

    void OnBoundaryChange(XRInputSubsystem subsystem)
    {

    }

    void OnTrackingOriginUpdated(XRInputSubsystem subsystem)
    {

    }
}
