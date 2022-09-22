using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTimerManager : MonoBehaviour
{
    // TODO make this nicer with enums

    public GameObject RedTimer;
    public GameObject GreenTimer;
    public GameObject BlueTimer;
    public GameObject YellowTimer;
    public GameObject CyanTimer;
    public GameObject MagentaTimer;

    private List<GameObject> OnTimerList;

    private void Start()
    {
        RedTimer.SetActive(false);
        GreenTimer.SetActive(false);
        BlueTimer.SetActive(false);
        YellowTimer.SetActive(false);
        CyanTimer.SetActive(false);
        MagentaTimer.SetActive(false);

        // initialize the list
        OnTimerList = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            RedTimerEnable();
        }
    }

    public void RedTimerEnable()
    {
        RedTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(RedTimer))
            RedTimer.GetComponent<Animator>().Play("keytimeranim");
        else
            OnTimerList.Insert(0, RedTimer);
    }

    public void RedTimerDisable()
    {
        OnTimerList.Remove(RedTimer);
        RedTimer.SetActive(false);
    }
}
