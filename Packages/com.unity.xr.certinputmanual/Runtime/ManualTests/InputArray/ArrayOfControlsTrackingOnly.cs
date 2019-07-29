using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class ArrayOfControlsTrackingOnly : MonoBehaviour
{
    public GameObject controlUIPrefab;
    public Text nameText;
    public ArbiterFeatureUsageDrivesUI IsTrackedDisplay;
    public TrackingStateBrokenOut TrackingStateDisplay;

    private RectTransform m_ParentRect;

    private Vector2 m_OriginalRectTransform;
    private Vector3 m_NextPosition;
    private float m_RowSeparation;

    private List<GameObject> m_Elements;

    void Start()
    {
        m_ParentRect = GetComponent<RectTransform>();
        m_OriginalRectTransform = m_ParentRect.sizeDelta;
        m_RowSeparation = 1.25f * controlUIPrefab.GetComponent<RectTransform>().rect.height;
        ResetElementInsertPosition();
        
        m_Elements = new List<GameObject>();

        Clear();
    }

    void AddElement(InputDevice device, InputFeatureUsage usage)
    {
        if (Mathf.Abs(m_NextPosition.y) > m_ParentRect.rect.height)
            m_ParentRect.sizeDelta = new Vector2(m_ParentRect.sizeDelta.x, m_ParentRect.sizeDelta.y + m_RowSeparation);
        GameObject NewControl = Instantiate(controlUIPrefab, m_ParentRect);
        m_Elements.Add(NewControl);
        NewControl.transform.localPosition = m_NextPosition;
        NewControl.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        NewControl.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        m_NextPosition = new Vector3(-m_NextPosition.x, m_NextPosition.x > 0 ? (m_NextPosition.y - m_RowSeparation): m_NextPosition.y, 0);

        NewControl.GetComponent<ControlValue>().SetDrivingUsage(device, usage);
    }

    void Clear()
    {
        for (int i = 0; i < m_Elements.Count; i++)
            Destroy(m_Elements[i]);

        m_Elements.Clear();

        m_ParentRect.sizeDelta = m_OriginalRectTransform;
        ResetElementInsertPosition();
    }

    void ResetElementInsertPosition()
    {
        m_NextPosition = new Vector3(-m_ParentRect.rect.width/4, -0.625f * m_RowSeparation, 0);
    }

    public void ClearArrayOfControls()
    {
        Clear();
    }

    public void FillArrayOfControls(InputDevice device)
    {
        nameText.text = device.name;

        List<InputFeatureUsage> usages = new List<InputFeatureUsage>();
        device.TryGetFeatureUsages(usages);

        for (int i = 0; i < usages.Count; i++)
        {
            if (UsageNameIndicatesTrackingFeature(usages[i]))
                AddElement(device, usages[i]);

            if (usages[i].name == "TrackingState")
            {
                IsTrackedDisplay.SetDrivingUsage(device, usages[i]);
                TrackingStateDisplay.Device = device;
            }
        }
    }

    bool UsageNameIndicatesTrackingFeature(InputFeatureUsage usage)
    {
        string lowerCaseUsageName = usage.name.ToLower();

        if (lowerCaseUsageName.Contains("trackingstate") || 
            lowerCaseUsageName.Contains("istracked") || 
            lowerCaseUsageName.Contains("position") || 
            lowerCaseUsageName.Contains("rotation") || 
            lowerCaseUsageName.Contains("velocity") || 
            lowerCaseUsageName.Contains("angularvelocity") || 
            lowerCaseUsageName.Contains("acceleration") || 
            lowerCaseUsageName.Contains("angularacceleration")
            )
        {
            return true;
        }

        return false;
    }
}
