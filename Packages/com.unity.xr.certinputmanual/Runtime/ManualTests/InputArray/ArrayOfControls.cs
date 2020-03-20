using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class ArrayOfControls : MonoBehaviour
{
    public GameObject controlUIPrefab;
    public Text nameText;

    private RectTransform m_ParentRect;

    private Vector2 m_OriginalRectTransform;
    private Vector3 m_NextPosition;
    private float m_RowSeparation;

    private List<string> m_CommonUsages;
    private List<GameObject> m_Elements;

    void Start()
    {
        BuildCommonUsagesList();

        m_ParentRect = GetComponent<RectTransform>();
        m_OriginalRectTransform = m_ParentRect.sizeDelta;
        m_RowSeparation = 1.25f * controlUIPrefab.GetComponent<RectTransform>().rect.height;
        ResetElementInsertPosition();
        
        m_Elements = new List<GameObject>();

        Clear();
    }

    void BuildCommonUsagesList()
    {
        m_CommonUsages = new List<string>();

        System.Reflection.MemberInfo[] Members = typeof(CommonUsages).GetMembers();
        Array.Sort(Members, new MemberInfoAlphabetizer());

        for (int i = 0; i < Members.Length; i++)
        {
            if (Members[i].MemberType == System.Reflection.MemberTypes.Field)
            {
                string memberName = Members[i].Name;
                string name = "";

                if (memberName.Length == 1)
                    name = (char.ToUpper(memberName[0])).ToString();
                else
                    name = (char.ToUpper(memberName[0]) + memberName.Substring(1));

                m_CommonUsages.Add(name);
            }
        }
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

        Debug.Log($"Does {usage.name} exist in common usages?");
        if (!m_CommonUsages.Contains(usage.name))
        {
            Text[] textComponents = NewControl.GetComponentsInChildren<Text>();

            for (int i = 0; i < textComponents.Length; i++)
                textComponents[i].color = Color.red;
        }
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
            AddElement(device, usages[i]);
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
