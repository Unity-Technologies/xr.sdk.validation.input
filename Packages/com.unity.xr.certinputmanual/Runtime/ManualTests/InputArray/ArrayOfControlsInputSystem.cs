using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class ArrayOfControlsInputSystem : MonoBehaviour
{
    public GameObject controlUIPrefab;
    public Text nameText;

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

    void AddElement(InputDevice device, InputControl control)
    {
        if (Mathf.Abs(m_NextPosition.y) > m_ParentRect.rect.height)
            m_ParentRect.sizeDelta = new Vector2(m_ParentRect.sizeDelta.x, m_ParentRect.sizeDelta.y + m_RowSeparation);
        GameObject NewControl = Instantiate(controlUIPrefab, m_ParentRect);
        m_Elements.Add(NewControl);
        NewControl.transform.localPosition = m_NextPosition;
        NewControl.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        NewControl.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        m_NextPosition = new Vector3(m_NextPosition.x, m_NextPosition.y - m_RowSeparation, 0);

        NewControl.GetComponent<ControlValueForInputSystem>().SetDrivingControl(device, control);
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
        m_NextPosition = new Vector3(0, -0.625f * m_RowSeparation, 0);
    }

    public void ClearArrayOfControls()
    {
        Clear();
    }

    public void FillArrayOfControlsIS(InputDevice device)
    {
        nameText.text = device.name;

        ReadOnlyArray<InputControl> controls = device.children;

        for (int i = 0; i < controls.Count; i++)
        {
            AddElement(device, controls[i]);
        }
    }

    public class MemberInfoAlphabetizer : IComparer<MemberInfo>  
    {
        // Call CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer<MemberInfo>.Compare(MemberInfo x, MemberInfo y)  
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
