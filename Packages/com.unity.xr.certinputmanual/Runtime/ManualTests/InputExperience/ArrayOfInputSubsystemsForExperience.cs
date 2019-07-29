using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class ArrayOfInputSubsystemsForExperience : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform xrRig;

    private RectTransform m_ParentRect;

    private Vector2 m_OriginalRectTransform;
    private Vector3 m_NextPosition;
    private float m_RowSeparation;

    private List<GameObject> m_Elements;

    void Start()
    {
        m_ParentRect = GetComponent<RectTransform>();
        m_OriginalRectTransform = m_ParentRect.sizeDelta;
        m_RowSeparation = 15 + uiPrefab.GetComponent<RectTransform>().rect.height;
        ResetElementInsertPosition();
        
        m_Elements = new List<GameObject>();

        Clear();
    }

    void AddElement(XRInputSubsystem inputSubsystem, int systemNumber)
    {
        if (Mathf.Abs(m_NextPosition.y) > m_ParentRect.rect.height)
            m_ParentRect.sizeDelta = new Vector2(m_ParentRect.sizeDelta.x, m_ParentRect.sizeDelta.y + m_RowSeparation);
        GameObject NewSubsystemUI = Instantiate(uiPrefab, m_ParentRect);
        m_Elements.Add(NewSubsystemUI);
        NewSubsystemUI.transform.localPosition = m_NextPosition;
        NewSubsystemUI.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        NewSubsystemUI.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        m_NextPosition = new Vector3(0, m_NextPosition.y - m_RowSeparation, 0);
        
        NewSubsystemUI.GetComponent<InputSubsystemPanel>().Setup(xrRig, systemNumber, inputSubsystem);
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
        m_NextPosition = new Vector3(0, -(15 + (0.5f * uiPrefab.GetComponent<RectTransform>().rect.height)), 0);
    }

    public void ClearArrayOfSubsystems()
    {
        Clear();
    }

    public void FillArrayOfSubsystems()
    {
        List<XRInputSubsystem> InputInstances = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(InputInstances);

        for (int i = 0; i < InputInstances.Count; i++)
        {
            AddElement(InputInstances[i], i);
        }
    }
}
