using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;
using UnityEngine.XR;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ListCommonUsages : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.Reflection.MemberInfo[] Members = typeof(CommonUsages).GetMembers();
        Array.Sort(Members, new MemberInfoAlphabetizer());
        string accumulator = "";

        for (int i = 0; i < Members.Length; i++)
        {
            if (Members[i].MemberType == System.Reflection.MemberTypes.Field)
            {
                accumulator += Members[i].Name;
                if (i != (Members.Length - 1))
                    accumulator += ", ";
            }
        }

        GetComponent<Text>().text = accumulator;
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
