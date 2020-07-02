using UnityEngine;
using UnityEditor;

public class ManualCertTestsEditor : EditorWindow
{
    internal const string k_WindowTitle = "Manual Certification Configuration";
    float m_DelaySeconds = -1;

    /// <summary>
    /// Create a window if necessary, or focus a currently open window.
    /// The MenuItem attribute inserts this as a function in the editor menu bars.
    /// </summary>
    [MenuItem("Tools/" + k_WindowTitle)]
    public static void ShowWindow()
    {
        GetWindow<ManualCertTestsEditor>(k_WindowTitle);
    }

    /// <summary>
    /// Specify what is drawn inside the editor window
    /// </summary>
    void OnGUI()
    {
        // Init
        if (m_DelaySeconds < 0)
        {
            if (PlayerPrefs.HasKey(ManualCertTestConfig.k_DelaySecondsKey))
                m_DelaySeconds = PlayerPrefs.GetFloat(ManualCertTestConfig.k_DelaySecondsKey);
            else
            {
                m_DelaySeconds = 0;
                PlayerPrefs.SetFloat(ManualCertTestConfig.k_DelaySecondsKey, m_DelaySeconds);
            }
        }

        float NewDelaySeconds = EditorGUILayout.DelayedFloatField(
            new GUIContent("Runtime setup delay", "The time in seconds to wait while the runtime under test completes setup."), 
            m_DelaySeconds);

        if (NewDelaySeconds != m_DelaySeconds)
        {
            m_DelaySeconds = NewDelaySeconds;
            PlayerPrefs.SetFloat(ManualCertTestConfig.k_DelaySecondsKey, m_DelaySeconds);
        }
    }
}
