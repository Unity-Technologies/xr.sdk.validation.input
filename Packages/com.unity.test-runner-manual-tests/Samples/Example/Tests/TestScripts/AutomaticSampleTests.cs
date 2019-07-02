using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using UnityEngine.SceneManagement;

// This file shows run-of-the-mill automated Test Runner tests for comparison to the manual tests.

public class TemplateAutomatic
{
    [UnityTest]
    [Description("This test should load the scene, print a debug statement, and succeed")]
    public IEnumerator AlwaysPass()
    {
        yield return null;
    }

    [UnityTest]
    [Description("This test should load the scene, print a debug statement, and succeed")]
    public IEnumerator AlwaysFail()
    {
        yield return null;
        Assert.IsTrue(false);
    }
}
