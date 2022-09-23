using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoundtrack : MonoBehaviour
{
    AudioManager am;
    void Awake()
    {
        am = FindObjectOfType<AudioManager>();
        if (am)
            am.Play("MainSoundtrack");
    }
}
