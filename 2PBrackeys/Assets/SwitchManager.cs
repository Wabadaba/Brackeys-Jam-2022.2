using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{

    //dear god i hope no one ever reads the code for this project
    private KeyTimerManager keytimer;

    public bool redOn;
    public bool greenOn;
    public bool blueOn;
    public bool cyanOn;
    public bool yellowOn;
    public bool magentaOn;

    public static SwitchManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        keytimer = FindObjectOfType<KeyTimerManager>();
    }

    // Update is called once per frame
    public void EnableSwitch(string type)
    {
        if (type.Equals("red"))
        {
            if (redOn)
                StopCoroutine("redTick");
            redOn = true;
            keytimer.RedTimerEnable();
            StartCoroutine("redTick");
        }
        else if (type.Equals("green"))
        {
            if (greenOn)
                StopCoroutine("greenTick");
            greenOn = true;
            keytimer.GreenTimerEnable();
            StartCoroutine("greenTick");
        }
        else if (type.Equals("blue"))
        {
            if (blueOn)
                StopCoroutine("blueTick");
            blueOn = true;
            keytimer.BlueTimerEnable();
            StartCoroutine("blueTick");
        }
        else if (type.Equals("cyan"))
        {
            if (cyanOn)
                StopCoroutine("cyanTick");
            cyanOn = true;
            keytimer.CyanTimerEnable();
            StartCoroutine("cyanTick");
        }
        else if (type.Equals("yellow"))
        {
            if (yellowOn)
                StopCoroutine("yellowTick");
            yellowOn = true;
            keytimer.YellowTimerEnable();
            StartCoroutine("yellowTick");
        }
        else if (type.Equals("magenta"))
        {
            if (magentaOn)
                StopCoroutine("magentaTick");
            magentaOn = true;
            keytimer.MagentaTimerEnable();
            StartCoroutine("magentaTick");
        }
    }

    IEnumerator redTick()
    {
        yield return new WaitForSeconds(5f);
        keytimer.RedTimerDisable();
        redOn = false;
    }

    IEnumerator greenTick()
    {
        yield return new WaitForSeconds(5f);
        keytimer.GreenTimerDisable();
        greenOn = false;
    }

    IEnumerator blueTick()
    {
        yield return new WaitForSeconds(5f);
        keytimer.BlueTimerDisable();
        blueOn = false;
    }

    IEnumerator cyanTick()
    {
        yield return new WaitForSeconds(5f);
        keytimer.CyanTimerDisable();
        cyanOn = false;
    }

    IEnumerator yellowTick()
    {
        yield return new WaitForSeconds(5f);
        keytimer.YellowTimerDisable();
        yellowOn = false;
    }

    IEnumerator magentaTick()
    {
        yield return new WaitForSeconds(5f);
        keytimer.MagentaTimerDisable();
        magentaOn = false;
    }
}
