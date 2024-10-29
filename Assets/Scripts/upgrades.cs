using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public Prestige prestige;

    public Sprite icon_1_0;
    public Sprite icon_1_1;
    public Sprite icon_1_2;

    private bool upg1_unlocked = false;
    private bool upg1_purchased = false;
    public Image button1;

    void Update()
    {
        UpgradeLocks();
    }


    //MOVE SPRITE FUNCTIONALITY TO SEPERATE SCRIPT
    public void UpgradeLocks()
    {
        if (prestige.prestigeMulti > 2)
        {
            upg1_unlocked = true;
            Debug.Log(upg1_unlocked);
        }
        else
        {
        }

        if (upg1_unlocked == false && upg1_purchased == false)
        {
            button1.sprite = icon_1_0;
        }
        else if (upg1_unlocked == true && upg1_purchased == false)
        {
            button1.sprite = icon_1_1;
        }
        else if (upg1_unlocked == true && upg1_purchased == true)
        {
            button1.sprite = icon_1_2;
        }
    }

    public bool upgTwo = false;
    public bool upgThree = false;
    public bool upgFour = false;

    public void UpgOne()
    {
        if (prestige.prestigeMulti > 2)
        {
            prestige.prestigeMulti -= 1;
            upg1_purchased = true;

        }
        else
        { }
    }
    public void UpgTwo()
    {
        if (upg1_purchased == true)
        {
            Debug.Log("unlocked");

        }
        else
        {

        }
    }
}