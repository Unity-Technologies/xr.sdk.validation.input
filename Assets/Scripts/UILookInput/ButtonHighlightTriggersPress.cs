using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonHighlightTriggersPress : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button m_Button;

    private bool m_Highlighted;
    private float m_Timer;
    private float m_TimeToPress = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        float inverseTimeFraction = 1.0f - (m_Timer / m_TimeToPress);
        if (m_Highlighted)
        {
            if (m_Timer > m_TimeToPress)
            {
                m_Button.onClick?.Invoke();
                m_Timer = 0f;
            }
            else // i like to do it this way in case frame rate is garbage for some reason
            {
                ColorBlock highlightBlock = m_Button.colors;
                highlightBlock.highlightedColor = new Color(inverseTimeFraction, inverseTimeFraction, 0f, 1f);
                m_Button.colors = highlightBlock;
                m_Timer += Time.deltaTime;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Highlighted = true;
        m_Timer = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Highlighted = false;
    }
}
