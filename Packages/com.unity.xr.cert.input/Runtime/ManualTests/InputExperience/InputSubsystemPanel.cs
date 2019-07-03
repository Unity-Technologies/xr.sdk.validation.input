using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.XR;
using UnityEngine.UI;

using Unity.TestRunnerManualTests;

public class InputSubsystemPanel : MonoBehaviour
{
    public Text currentOriginType;
    public Text availableOriginType;
    public Text displayName;
    public Text status;
    public Text boundaryChanged;
    public Text trackingOriginUpdated;

    private Transform m_ParentForBoundaryPoints;
    private List<GameObject> m_BoundaryPointVisualizers = null;
    private LineRenderer m_BoundaryLineRenderer = null;

    private int m_SystemNumber = -1;

    private XRInputSubsystem m_InputSubsystem = null;
    private XRInputSubsystem InputSubsystem
    {
        get { return m_InputSubsystem; }
        set
        {
            if (m_InputSubsystem != null)
            {
                m_InputSubsystem.boundaryChanged -= OnBoundaryChange;
                m_InputSubsystem.trackingOriginUpdated -= OnTrackingOriginUpdated;
            }

            m_InputSubsystem = value;
            displayName.text = m_SystemNumber + ": " + m_InputSubsystem.SubsystemDescriptor.id;
            currentOriginType.text = m_InputSubsystem.GetTrackingOriginMode().ToString();
            UpdateAvailableOriginType();

            m_InputSubsystem.boundaryChanged += OnBoundaryChange;
            m_InputSubsystem.trackingOriginUpdated += OnTrackingOriginUpdated;

            DrawBoundary();
        }
    }

    public void Setup(Transform parentForBoundaryPoints, int systemNumber, XRInputSubsystem subsystem)
    {
        m_ParentForBoundaryPoints = parentForBoundaryPoints;
        m_SystemNumber = systemNumber;
        InputSubsystem = subsystem;
    }

    void SetStatus(string newText)
    {
        status.text = newText;
        StartCoroutine(UITextColorPulse(status));
    }

    public void TryRecenter()
    {
        if (m_InputSubsystem.TryRecenter())
            SetStatus("Recentered");
        else
            SetStatus("Could not recenter. Recenter can only function in Device TrackingMode, and may not be available on this provider.");
    }
    
    public void UpdateCurrentOriginType(TrackingOriginModeFlags targetTrackingOriginMode)
    {
        if (!m_InputSubsystem.TrySetTrackingOriginMode(targetTrackingOriginMode) && 
            ((targetTrackingOriginMode & m_InputSubsystem.GetSupportedTrackingOriginModes()) != 0)) {
            SetStatus("Error! Tracking mode could not be set!");
            return;
        }

        currentOriginType.text = m_InputSubsystem.GetTrackingOriginMode().ToString();

        if ((m_InputSubsystem.GetTrackingOriginMode() & m_InputSubsystem.GetSupportedTrackingOriginModes()) == 0)
            SetStatus("Error! Tracking mode set to an unsupported mode!");
        else if (targetTrackingOriginMode != m_InputSubsystem.GetTrackingOriginMode())
            SetStatus("Success! Tracking mode not set to an unsupported mode!");
        else
            SetStatus("Success! Tracking mode set to a supported mode!");
    }

    public void UpdateAvailableOriginType()
    {
        string accumulation = "";

        TrackingOriginModeFlags AvailableOrigins = m_InputSubsystem.GetSupportedTrackingOriginModes();

        foreach (TrackingOriginModeFlags type in Enum.GetValues(typeof(TrackingOriginModeFlags)))
        {
            if ((AvailableOrigins & type) != 0)
                accumulation += type.ToString() + ", ";
        }

        availableOriginType.text = accumulation;
    }

    public void TrySetOriginTypeDevice()
    {
        UpdateCurrentOriginType(TrackingOriginModeFlags.Device);
    }

    public void TrySetOriginTypeFloor()
    {
        UpdateCurrentOriginType(TrackingOriginModeFlags.Floor);
    }

    public void TrySetOriginTypeTrackingReference()
    {
        UpdateCurrentOriginType(TrackingOriginModeFlags.TrackingReference);
    }

    public void TrySetOriginTypeUnknown()
    {
        UpdateCurrentOriginType(TrackingOriginModeFlags.Unknown);
    }

    void OnBoundaryChange(XRInputSubsystem subsystem)
    {
        StartCoroutine(UITextColorPulse(boundaryChanged));
        Debug.Log("OnBoundaryChanged");
        DrawBoundary();
    }

    void OnTrackingOriginUpdated(XRInputSubsystem subsystem)
    {
        StartCoroutine(UITextColorPulse(trackingOriginUpdated));
        Debug.Log("OnTrackingOriginUpdated");
        DrawBoundary();
    }

    IEnumerator UITextColorPulse(Text text)
    {
        Debug.Log("Started new UITextColorPulse with text " + text.name);

        float TotalTime = 2f;
        float Timer = 0;

        while (Timer < TotalTime) {
            Timer += Time.deltaTime;
            text.color = (Color.red * ((TotalTime - Timer) / TotalTime))
                + (Color.black * (Timer / TotalTime));
            yield return null;
        }
    }

    void DrawBoundary()
    {
        if (m_BoundaryLineRenderer == null) {
            m_BoundaryLineRenderer = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
            m_BoundaryLineRenderer.startWidth = 0.05f;
            m_BoundaryLineRenderer.endWidth = 0.05f;
        }

        List<Vector3> BoundaryPoints = new List<Vector3>();

        if (!m_InputSubsystem.TryGetBoundaryPoints(BoundaryPoints))
        {
            SetStatus("Error! TryGetBoundaryPoints failed!");
            Debug.Log("Error! TryGetBoundaryPoints failed!");
            return;
        }
        else
        {
            if (m_BoundaryPointVisualizers == null)
                m_BoundaryPointVisualizers = new List<GameObject>();
            else
            {
                for(int i = 0; i < m_BoundaryPointVisualizers.Count; i++)
                {
                    Destroy(m_BoundaryPointVisualizers[i]);
                }
                m_BoundaryPointVisualizers.Clear();
            }

            for (int i = 0; i < BoundaryPoints.Count; i++)
            {
                GameObject NewGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                NewGameObject.name = m_InputSubsystem.ToString() + "BoundaryPoint" + i;
                NewGameObject.transform.SetParent(m_ParentForBoundaryPoints);
                NewGameObject.transform.localPosition = BoundaryPoints[i];
                NewGameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                m_BoundaryPointVisualizers.Add(NewGameObject);

                if (i == 0)
                {
                    NewGameObject.GetComponent<MeshRenderer>().material.color = Color.green;

                    GameObject Signpost = new GameObject();
                    Signpost.transform.SetParent(NewGameObject.transform);
                    Signpost.transform.localPosition = Vector3.zero;

                    Signpost.AddComponent<Billboard>();

                    TextMesh StartText = Signpost.AddComponent<TextMesh>();
                    StartText.text = ("Start of boundary\n for XRInputSubsystem " + m_SystemNumber);
                    StartText.alignment = TextAlignment.Center;
                    StartText.anchor = TextAnchor.MiddleCenter;
                    StartText.fontSize = 120;
                    StartText.characterSize = 0.01f;
                    StartText.color = Color.green;
                }
            }

            m_BoundaryLineRenderer.positionCount = BoundaryPoints.Count;
            m_BoundaryLineRenderer.SetPositions(BoundaryPoints.ToArray());
        }
    }
}
