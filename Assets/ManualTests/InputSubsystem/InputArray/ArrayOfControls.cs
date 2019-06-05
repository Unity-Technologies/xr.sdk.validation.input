using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

[RequireComponent (typeof(RectTransform))]
public class ArrayOfControls : MonoBehaviour
{
    public GameObject ControlUIPrefab;

    private RectTransform m_ParentRect;

    private Vector2 m_OriginalRectTransform;
    private Vector3 m_NextPosition;
    private float m_RowSeparation;

    private List<GameObject> m_Elements;

    void Start()
    {
        m_ParentRect = GetComponent<RectTransform>();
        m_OriginalRectTransform = m_ParentRect.sizeDelta;
        m_RowSeparation = 1.25f * ControlUIPrefab.GetComponent<RectTransform>().rect.height;
        ResetElementInsertPosition();
        
        m_Elements = new List<GameObject>();

        Clear();
    }

    public void AddElement(InputDevice device, InputFeatureUsage usage)
    {
        if (Mathf.Abs(m_NextPosition.y) > m_ParentRect.rect.height)
            m_ParentRect.sizeDelta = new Vector2(m_ParentRect.sizeDelta.x, m_ParentRect.sizeDelta.y + m_RowSeparation);
        GameObject NewControl = Instantiate(ControlUIPrefab, m_ParentRect);
        m_Elements.Add(NewControl);
        NewControl.transform.localPosition = m_NextPosition;
        NewControl.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        NewControl.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        m_NextPosition = new Vector3(-m_NextPosition.x, m_NextPosition.x > 0 ? (m_NextPosition.y - m_RowSeparation): m_NextPosition.y, 0);

        NewControl.GetComponent<ControlValue>().SetDrivingUsage(device, usage);
    }

    public void Clear()
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
}
