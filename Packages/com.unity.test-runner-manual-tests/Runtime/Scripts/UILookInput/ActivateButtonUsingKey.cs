using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


namespace Unity.TestRunnerManualTests
{
    [RequireComponent(typeof(Button))]
    public class ActivateButtonUsingKey : MonoBehaviour
    {
        public KeyCode keycode;
        private Button m_Button;

        // Start is called before the first frame update
        void Start()
        {
            m_Button = GetComponent<Button>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(keycode))
                m_Button.onClick?.Invoke();
        }
    }
}
