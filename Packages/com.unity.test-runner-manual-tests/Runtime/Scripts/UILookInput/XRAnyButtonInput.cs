using UnityEngine;
using UnityEngine.EventSystems;


namespace Unity.TestRunnerManualTests
{
    public class XRAnyButtonInput : BaseInput
    {
        [SerializeField, Tooltip("The keyboard key treated as the mouse button.")]
        KeyCode m_MouseKeyCode = KeyCode.Space;


        /// <summary>
        /// You can use this to treat any button press as a mouse click.
        /// Be wary of using this with capacitive touch!
        /// </summary>
        public bool clickMouseOnAnyButton = false;

        public override bool mousePresent
        {
            get { return true; }
        }

        public override Vector2 mouseScrollDelta
        {
            get { return Vector2.zero; }
        }

        public override Vector2 mousePosition
        {
            get { return new Vector2(UnityEngine.XR.XRSettings.eyeTextureWidth / 2f, UnityEngine.XR.XRSettings.eyeTextureHeight / 2f); }
        }

        public override bool GetMouseButton(int button)
        {
            if (button != 0)
                return false;

            // Doesn't work on 18.3.0a9 - fix if necessary
            //if (Application.isMobilePlatform)
            //{

    //#if UNITY_HAS_GOOGLEVR && !UNITY_IOS
    //            if (GvrController.State == GvrConnectionState.Connected)
    //            {
    //                return GvrController.IsTouching;
    //            }
    //            else
    //            {
    //                return Input.GetMouseButton(0);
    //            }
    //#else
    //            return Input.GetMouseButton(0);
    //#endif
    //        }
            return
                Input.GetKey(m_MouseKeyCode) ||
                (clickMouseOnAnyButton &&
                (Input.GetKey(KeyCode.JoystickButton0) ||
                Input.GetKey(KeyCode.JoystickButton1) ||
                Input.GetKey(KeyCode.JoystickButton2) ||
                Input.GetKey(KeyCode.JoystickButton3) ||
                Input.GetKey(KeyCode.JoystickButton4) ||
                Input.GetKey(KeyCode.JoystickButton5) ||
                Input.GetKey(KeyCode.JoystickButton6) ||
                Input.GetKey(KeyCode.JoystickButton7) ||
                Input.GetKey(KeyCode.JoystickButton8) ||
                Input.GetKey(KeyCode.JoystickButton9) ||
                Input.GetKey(KeyCode.JoystickButton10) ||
                Input.GetKey(KeyCode.JoystickButton11) ||
                Input.GetKey(KeyCode.JoystickButton12) ||
                Input.GetKey(KeyCode.JoystickButton13) ||
                Input.GetKey(KeyCode.JoystickButton14) ||
                Input.GetKey(KeyCode.JoystickButton15) ||
                Input.GetKey(KeyCode.JoystickButton16) ||
                Input.GetKey(KeyCode.JoystickButton17) ||
                Input.GetKey(KeyCode.JoystickButton18) ||
                Input.GetKey(KeyCode.JoystickButton19)))
            ;
        }

        public override bool GetMouseButtonDown(int button)
        {
            if (button != 0)
                return false;

            // Doesn't work on 18.3.0a9 - fix if necessary
    //        if (Application.isMobilePlatform)
    //        {
    //#if UNITY_HAS_GOOGLEVR && !UNITY_IOS
    //            if (GvrController.State == GvrConnectionState.Connected)
    //            {
    //                return GvrController.TouchDown;
    //            }
    //            else
    //            {
    //                return Input.GetMouseButtonDown(0);
    //            }
    //#else
    //            return Input.GetMouseButtonDown(0);
    //#endif
    //        }
            return
                Input.GetKeyDown(m_MouseKeyCode) ||
                (clickMouseOnAnyButton &&
                (Input.GetKeyDown(KeyCode.JoystickButton0) ||
                Input.GetKeyDown(KeyCode.JoystickButton1) ||
                Input.GetKeyDown(KeyCode.JoystickButton2) ||
                Input.GetKeyDown(KeyCode.JoystickButton3) ||
                Input.GetKeyDown(KeyCode.JoystickButton4) ||
                Input.GetKeyDown(KeyCode.JoystickButton5) ||
                Input.GetKeyDown(KeyCode.JoystickButton6) ||
                Input.GetKeyDown(KeyCode.JoystickButton7) ||
                Input.GetKeyDown(KeyCode.JoystickButton8) ||
                Input.GetKeyDown(KeyCode.JoystickButton9) ||
                Input.GetKeyDown(KeyCode.JoystickButton10) ||
                Input.GetKeyDown(KeyCode.JoystickButton11) ||
                Input.GetKeyDown(KeyCode.JoystickButton12) ||
                Input.GetKeyDown(KeyCode.JoystickButton13) ||
                Input.GetKeyDown(KeyCode.JoystickButton14) ||
                Input.GetKeyDown(KeyCode.JoystickButton15) ||
                Input.GetKeyDown(KeyCode.JoystickButton16) ||
                Input.GetKeyDown(KeyCode.JoystickButton17) ||
                Input.GetKeyDown(KeyCode.JoystickButton18) ||
                Input.GetKeyDown(KeyCode.JoystickButton19)))
            ;
        }

        public override bool GetMouseButtonUp(int button)
        {
            if (button != 0)
                return false;

            // Doesn't work on 18.3.0a9 - fix if necessary
    //        if (Application.isMobilePlatform)
    //        {
    //#if UNITY_HAS_GOOGLEVR && !UNITY_IOS
    //            if (GvrController.State == GvrConnectionState.Connected)
    //            {
    //                return GvrController.TouchUp;
    //            }
    //            else
    //            {
    //                return Input.GetMouseButtonUp(0);
    //            }
    //#else
    //            return Input.GetMouseButtonUp(0);
    //#endif
    //        }
            return
                Input.GetKeyUp(m_MouseKeyCode) ||
                (clickMouseOnAnyButton &&
                (Input.GetKeyUp(KeyCode.JoystickButton0) ||
                Input.GetKeyUp(KeyCode.JoystickButton1) ||
                Input.GetKeyUp(KeyCode.JoystickButton2) ||
                Input.GetKeyUp(KeyCode.JoystickButton3) ||
                Input.GetKeyUp(KeyCode.JoystickButton4) ||
                Input.GetKeyUp(KeyCode.JoystickButton5) ||
                Input.GetKeyUp(KeyCode.JoystickButton6) ||
                Input.GetKeyUp(KeyCode.JoystickButton7) ||
                Input.GetKeyUp(KeyCode.JoystickButton8) ||
                Input.GetKeyUp(KeyCode.JoystickButton9) ||
                Input.GetKeyUp(KeyCode.JoystickButton10) ||
                Input.GetKeyUp(KeyCode.JoystickButton11) ||
                Input.GetKeyUp(KeyCode.JoystickButton12) ||
                Input.GetKeyUp(KeyCode.JoystickButton13) ||
                Input.GetKeyUp(KeyCode.JoystickButton14) ||
                Input.GetKeyUp(KeyCode.JoystickButton15) ||
                Input.GetKeyUp(KeyCode.JoystickButton16) ||
                Input.GetKeyUp(KeyCode.JoystickButton17) ||
                Input.GetKeyUp(KeyCode.JoystickButton18) ||
                Input.GetKeyUp(KeyCode.JoystickButton19)))
            ;
        }

        public override bool GetButtonDown(string buttonName)
        {
            return false;
        }

        public override bool touchSupported
        {
            get { return false; }
        }
    }
}
