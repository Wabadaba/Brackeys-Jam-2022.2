using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinManager : MonoBehaviour
{
    // how many players are inside the win zone
    private int colCount;
    private void Start()
    {
        colCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colCount += 1;
            if(colCount >= 2)
            {
                FindObjectOfType<MovementController>().CompleteLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colCount -= 1;
        }
    }


}
