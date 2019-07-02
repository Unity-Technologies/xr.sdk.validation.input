using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using System;

public class BuildSetupAndCleanup : IPrebuildSetup, IPostBuildCleanup
{
    const string SavedSceneListPrefName    = "SavedSceneList";

    public void Setup()
    {
        // Save original scene list
        string SceneListAccumulator = "";
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            SceneListAccumulator += EditorBuildSettings.scenes[i].path + (EditorBuildSettings.scenes[i].enabled ? "1" : "0") + "\n";

        PlayerPrefs.SetString(SavedSceneListPrefName, SceneListAccumulator);
        PlayerPrefs.Save();

        // Set up test scene list for the test run
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[] {
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputArray.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputExperience.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputExperienceRecenter.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputFeatureUsageControls.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputFeatureUsageTracking.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/InputHaptics.unity", true),
            new EditorBuildSettingsScene("Packages/com.unity.xr.cert.input/Runtime/ManualTests/NameManfSerial.unity", true),
            };
    }

    public void Cleanup()
    {
        // Restore original scene list
        List<string> RestoredScenes = new List<string>();
        string SavedScenesString = PlayerPrefs.GetString(SavedSceneListPrefName);

        if (SavedScenesString == "")
        {
            EditorBuildSettings.scenes = new EditorBuildSettingsScene[0];
            return;
        }
        
        string[] SavedScenesRawString = SavedScenesString.Split('\n');
        List<EditorBuildSettingsScene> EditorScenes = new List<EditorBuildSettingsScene>();
        EditorBuildSettingsScene Temp;

        // Length - 1 because String.Split() will give you an empty string as its last element
        for (int i = 0; i < SavedScenesRawString.Length - 1; i++)
        {
            Temp = new EditorBuildSettingsScene();
            Temp.path = SavedScenesRawString[i].Substring(0, SavedScenesRawString[i].Length - 1);
            Temp.enabled = ((SavedScenesRawString[i].Substring(SavedScenesRawString[i].Length - 1) == "1") ? true : false);
            EditorScenes.Add(Temp);
        }

        EditorBuildSettingsScene[] EditorScenesArray  = EditorScenes.ToArray();
        EditorBuildSettings.scenes = EditorScenesArray;
    }
}
