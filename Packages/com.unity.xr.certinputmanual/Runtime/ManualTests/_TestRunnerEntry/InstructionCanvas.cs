using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Unity.TestRunnerManualTests;

public class InstructionCanvas : MonoBehaviour
{
    public Text Title;
    public Text Instructions;

    private TestFacilitator Facilitator;

    private void Start()
    {
        Title.text = SceneManager.GetActiveScene().name;
        Facilitator = GameObject.FindGameObjectWithTag("Facilitator").GetComponent<TestFacilitator>();
    }

    public void SetInstructions(string Text)
    {
        Instructions.text = Text;
    }

    public void Skip()
    {
        Facilitator.Skip("InstructionCanvas requesting Skip");
    }

    public void Inconclusive()
    {
        Facilitator.Inconclusive("InstructionCanvas reporting Inconclusive");
    }

    public void Fail()
    {
        Facilitator.Fail("InstructionCanvas reporting Failure");    }

    public void Continue()
    {
        Facilitator.Continue();
    }
}
