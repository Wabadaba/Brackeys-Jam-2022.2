using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private AudioManager am;
    public string switchType;

    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(am)
                FindObjectOfType<AudioManager>().Play("click");
            FindObjectOfType<SwitchManager>().EnableSwitch(switchType);
        }
    }
}
