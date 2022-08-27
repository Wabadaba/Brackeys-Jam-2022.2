using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : MonoBehaviour
{
    const int DOORINT = 7;
    const int GATEINT = 9;
    SwitchManager switches;

    public Sprite doorSprite;
    public Sprite gateSprite;
    public string blocktype;
    public bool solid;

    bool redOn;
    bool greenOn;
    bool blueOn;
    bool cyanOn;
    bool yellowOn;
    bool magentaOn;

    void Start()
    {
        switches = FindObjectOfType<SwitchManager>();
        if (solid)
        {
            GetComponent<SpriteRenderer>().sprite = doorSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = gateSprite;
        }
    }

    void FixedUpdate()
    {
        UpdateColors();
        if (checkOn())
        {
            // make it the OPPOSITE of what the initial state is
            if (solid)
            {
                GetComponent<SpriteRenderer>().sprite = gateSprite;
                gameObject.layer = GATEINT;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = doorSprite;
                GetComponent<BoxCollider2D>().enabled = true;
                gameObject.layer = DOORINT;
            }
        } else // lol lmao
        {
            if (solid)
            {
                GetComponent<SpriteRenderer>().sprite = doorSprite;
                GetComponent<BoxCollider2D>().enabled = true;
                gameObject.layer = DOORINT;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = gateSprite;
                GetComponent<BoxCollider2D>().enabled = false;
                gameObject.layer = GATEINT;
            }
        }
    }

    void UpdateColors()
    {
        switches = FindObjectOfType<SwitchManager>();
        redOn = switches.redOn;
        greenOn = switches.greenOn;
        blueOn = switches.blueOn;
        cyanOn = switches.cyanOn;
        yellowOn = switches.yellowOn;
        magentaOn = switches.magentaOn;
    }

    bool checkOn()
    {
        if (redOn && blocktype.Equals("red") ||
            greenOn && blocktype.Equals("green") ||
            blueOn && blocktype.Equals("blue") ||
            cyanOn && blocktype.Equals("cyan") ||
            yellowOn && blocktype.Equals("yellow") ||
            magentaOn && blocktype.Equals("magenta"))
        {
            return true;
        }
        return false;
    }
}
