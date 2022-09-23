using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTimerManager : MonoBehaviour
{
    // TODO make this nicer with enums
    private const float TIMEROFFSETX = -25;
    private const float TIMEROFFSETY = -50;

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

    public void RedTimerEnable()
    {
        RedTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(RedTimer)) {
            RedTimer.GetComponent<Animator>().SetTrigger("KeyTimerTrigger");
            OnTimerList.Remove(RedTimer);
        }
        OnTimerList.Insert(0, RedTimer);
        UpdateKeyList();
    }

    public void RedTimerDisable()
    {
        OnTimerList.Remove(RedTimer);
        RedTimer.SetActive(false);
        UpdateKeyList();
    }

    public void GreenTimerEnable()
    {
        GreenTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(GreenTimer)) {
            GreenTimer.GetComponent<Animator>().SetTrigger("KeyTimerTrigger");
            OnTimerList.Remove(GreenTimer);
        }
        OnTimerList.Insert(0, GreenTimer);
        UpdateKeyList();
    }

    public void GreenTimerDisable()
    {
        OnTimerList.Remove(GreenTimer);
        GreenTimer.SetActive(false);
        UpdateKeyList();
    }

    public void BlueTimerEnable()
    {
        BlueTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(BlueTimer)) {
            BlueTimer.GetComponent<Animator>().SetTrigger("KeyTimerTrigger");
            OnTimerList.Remove(BlueTimer);
        }
        OnTimerList.Insert(0, BlueTimer);
        UpdateKeyList();
    }

    public void BlueTimerDisable()
    {
        OnTimerList.Remove(BlueTimer);
        BlueTimer.SetActive(false);
        UpdateKeyList();
    }

    public void YellowTimerEnable()
    {
        YellowTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(YellowTimer)) {
            YellowTimer.GetComponent<Animator>().SetTrigger("KeyTimerTrigger");
            OnTimerList.Remove(YellowTimer);
        }
        OnTimerList.Insert(0, YellowTimer);
        UpdateKeyList();
    }

    public void YellowTimerDisable()
    {
        OnTimerList.Remove(YellowTimer);
        YellowTimer.SetActive(false);
        UpdateKeyList();
    }

    public void CyanTimerEnable()
    {
        CyanTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(CyanTimer)) {
            CyanTimer.GetComponent<Animator>().SetTrigger("KeyTimerTrigger");
            OnTimerList.Remove(CyanTimer);
        }
        OnTimerList.Insert(0, CyanTimer);
        UpdateKeyList();
    }

    public void CyanTimerDisable()
    {
        OnTimerList.Remove(CyanTimer);
        CyanTimer.SetActive(false);
        UpdateKeyList();
    }

    public void MagentaTimerEnable()
    {
        MagentaTimer.SetActive(true);

        // if active, play timer, else, add to list of on timers
        if (OnTimerList.Contains(MagentaTimer))
        {
            MagentaTimer.GetComponent<Animator>().SetTrigger("KeyTimerTrigger");
            OnTimerList.Remove(MagentaTimer);
        }
        OnTimerList.Insert(0, MagentaTimer);
        UpdateKeyList();
    }

    public void MagentaTimerDisable()
    {
        OnTimerList.Remove(MagentaTimer);
        MagentaTimer.SetActive(false);
        UpdateKeyList();
    }

    private void UpdateKeyList()
    {
        float currOffset = TIMEROFFSETY;
        RectTransform currPos;
        foreach (GameObject timer in OnTimerList)
        {
            currPos = timer.GetComponent<RectTransform>();
            currPos.anchoredPosition = new Vector2(TIMEROFFSETX, currOffset);
            currOffset += TIMEROFFSETY;
        }
    }

}
