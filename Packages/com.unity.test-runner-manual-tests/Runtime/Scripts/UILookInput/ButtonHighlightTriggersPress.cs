using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Unity.TestRunnerManualTests
{
    [RequireComponent(typeof(Button))]
    public class ButtonHighlightTriggersPress : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button m_Button;

        private bool m_Highlighted;
        private float m_Timer;
        private float m_TimeToPress = 1.0f;
        private Color m_OriginalHighlightColor;

        // Start is called before the first frame update
        void Start()
        {
            m_Button = GetComponent<Button>();

            ColorBlock OriginalColorBlock = m_Button.colors;
            m_OriginalHighlightColor = OriginalColorBlock.highlightedColor;
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
                    EndHighlight(); // Prevent accidental double pressing
                }
                else // add time after check in case frame rate is garbage for some reason
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
            StartHighlight();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EndHighlight();
        }

        void StartHighlight()
        {
            m_Highlighted = true;
            m_Timer = 0f;
        }

        void EndHighlight()
        {
            m_Highlighted = false;
            m_Timer = 0f;

            // Reset colors
            ColorBlock highlightBlock = m_Button.colors;
            highlightBlock.highlightedColor = Color.white;
            m_Button.colors = highlightBlock;
        }
    }
}
