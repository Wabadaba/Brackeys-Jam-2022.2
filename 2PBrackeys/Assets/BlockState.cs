using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : MonoBehaviour
{
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

    // Start is called before the first frame update
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

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateColors();
        if (checkOn())
        {
            // make it the OPPOSITE of what the initial state is
            if (solid)
            {
                GetComponent<SpriteRenderer>().sprite = gateSprite;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = doorSprite;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        } else // lol lmao
        {
            if (solid)
            {
                GetComponent<SpriteRenderer>().sprite = doorSprite;
                GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = gateSprite;
                GetComponent<BoxCollider2D>().enabled = false;
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
