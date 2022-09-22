using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    public float InstructionTime = 5f;
    void Start()
    {
        StartCoroutine(FinishInstructions());
    }

    IEnumerator FinishInstructions()
    {
        yield return new WaitForSeconds(InstructionTime);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
