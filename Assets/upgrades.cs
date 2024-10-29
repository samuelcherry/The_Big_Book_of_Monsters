using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public Prestige prestige;



    public bool upgOne = false;
    public bool upgTwo = false;
    public bool upgThree = false;
    public bool upgFour = false;

    public void UpgOne()
    {
        if (prestige.prestigeMulti > 2)
        {
            prestige.prestigeMulti -= 1;
            upgOne = true;
        }
        else
        { }
    }
    public void UpgTwo()
    {
        if (upgOne == true)
        {
            Debug.Log("unlocked");

        }
        else
        {

        }
    }
}