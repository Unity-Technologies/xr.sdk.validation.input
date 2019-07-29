using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(TextMesh))]
public class TextFromWorldPosition : MonoBehaviour
{
    private TextMesh m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMesh>();
        m_Text.text = GameObject.FindGameObjectWithTag("Player") == null ? 
            transform.position.ToString() : 
            (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).ToString();
    }
}
