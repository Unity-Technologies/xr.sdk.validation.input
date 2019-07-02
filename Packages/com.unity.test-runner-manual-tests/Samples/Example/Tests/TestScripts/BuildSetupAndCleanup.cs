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
    int NumberOfScenes = 0;

    public void Setup()
    {
        // Set up test scene list for the test run
        EditorBuildSettingsScene[] TestScenes = new EditorBuildSettingsScene[] {
            new EditorBuildSettingsScene("Packages/com.unity.test-runner-manual-tests/Samples/Example/Tests/Scenes/SampleTestScene.unity", true)
            };

        NumberOfScenes = TestScenes.Length;

        EditorBuildSettingsScene[] NewSceneList = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length + TestScenes.Length];

        Array.Copy(EditorBuildSettings.scenes, NewSceneList, EditorBuildSettings.scenes.Length);
        Array.Copy(TestScenes, 0, NewSceneList, EditorBuildSettings.scenes.Length, TestScenes.Length);

        EditorBuildSettings.scenes = NewSceneList;
    }

    public void Cleanup()
    {
        EditorBuildSettingsScene[] NewSceneList = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length - NumberOfScenes];
        Array.Copy(EditorBuildSettings.scenes, NewSceneList, NewSceneList.Length);
        EditorBuildSettings.scenes = NewSceneList;
    }
}
