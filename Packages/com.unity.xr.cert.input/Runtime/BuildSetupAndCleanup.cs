using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using System;

// This setup script appends scenes to the scene list during setup.
// It then removes the same number of scenes from the scene list during cleanup.

public class BuildSetupAndCleanup : IPrebuildSetup, IPostBuildCleanup
{
    string PlayerPrefKey = "XRPluginInputTestsNumberOfTestScenes";

    public void Setup()
    {
        // Set up test scene list for the test run
        EditorBuildSettingsScene[] TestScenes = new EditorBuildSettingsScene[] {
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputArray.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputExperience.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputExperienceRecenter.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputFeatureUsageControls.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputFeatureUsageTracking.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputHaptics.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/NameManfSerial.unity", true),
            };

        PlayerPrefs.SetInt(PlayerPrefKey, TestScenes.Length);

        EditorBuildSettingsScene[] NewSceneList = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length + TestScenes.Length];

        Array.Copy(EditorBuildSettings.scenes, NewSceneList, EditorBuildSettings.scenes.Length);
        Array.Copy(TestScenes, 0, NewSceneList, EditorBuildSettings.scenes.Length, TestScenes.Length);

        EditorBuildSettings.scenes = NewSceneList;
    }

    public void Cleanup()
    {
        EditorBuildSettingsScene[] NewSceneList = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length - PlayerPrefs.GetInt(PlayerPrefKey)];
        Array.Copy(EditorBuildSettings.scenes, NewSceneList, NewSceneList.Length);
        EditorBuildSettings.scenes = NewSceneList;
    }
}
