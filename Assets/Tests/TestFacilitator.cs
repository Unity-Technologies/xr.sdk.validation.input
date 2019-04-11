using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.SceneManagement;

public class TestFacilitator : MonoBehaviour
{
    public IEnumerator RunAllTests()
    {
        do
        {
            yield return null;
        } while (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.JoystickButton8) && !Input.GetKey(KeyCode.JoystickButton9));

        yield return null;
        Debug.Log("All Tests Finished for scene \"" + SceneManager.GetActiveScene().name + "\"");
    }

    public string GetTestStatus()
    {
        return "\nGeneral Status: Passed " +
            "\nDetailed Status: scene was loaded, you are now getting test status.  The setup works.";
    }
}
