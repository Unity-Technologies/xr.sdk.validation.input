using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VR;

public class XRInputCursor : MonoBehaviour
{
    private List<RaycastResult> m_RaycastResults;
    private Camera m_Camera;

    private EventSystem m_EventSystem;
    public SpriteRenderer sprite;

    public void OnButtonClick(string button_name)
    {
        Debug.Log("OnButtonClick!");
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    void Start()
    {
        m_RaycastResults = new List<RaycastResult>();
        m_Camera = Camera.main;
        m_EventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (!m_EventSystem)
            return;

        var pointerData = new PointerEventData(m_EventSystem);
        pointerData.position = new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth / 2f, UnityEngine.XR.XRSettings.eyeTextureHeight / 2f);

        m_RaycastResults.Clear();
        m_EventSystem.RaycastAll(pointerData, m_RaycastResults);

        if (m_RaycastResults.Count == 0)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;

            var closestResult = new RaycastResult {distance = float.MaxValue};
            foreach (var r in m_RaycastResults)
            {
                if (r.distance < closestResult.distance)
                    closestResult = r;
            }

            transform.position = m_Camera.transform.position + m_Camera.transform.forward * (closestResult.distance - m_Camera.nearClipPlane);
        }
    }
}
