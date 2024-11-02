using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithToggleManager : MonoBehaviour
{
    public SlotUpgrades slotUpgrades;
    public EnemyStats enemyStats;
    public Prestige prestige;
    public Button AutoBuyerOne;
    public Button AutoBuyerTwo;
    public Button AutoBuyerThree;
    public GameObject AutoBuyerOneButton;
    public GameObject AutoBuyerTwoButton;
    public GameObject AutoBuyerThreeButton;

    public int AutoBuyerAmt = 1;
    public int AutoBuyerMax = 3;
    public int ToggleOneValue = 0;
    public int ToggleTwoValue = 0;
    public int ToggleThreeValue = 0;

    public int AutoBuyerLvl;
    public int[] AutoBuyerCost;

    public TMP_Text AutoBuyerAmtText;
    public TMP_Text AutoBuyerCostText;

    void Start()
    {
        AutoBuyerLvl = 0;
        AutoBuyerCost = new int[] { 10, 15, 25 };
    }

    void Update()
    {
        CheckGold();
        UpdateAutoBuyerText();
        CheckToggleVisibilty();
    }

    public void CheckGold()
    {
        if (ToggleOneValue == 1)
        {
            if (enemyStats.GoldAmt >= slotUpgrades.slotOneAmtArr[slotUpgrades.slotOneLvl])
            {
                slotUpgrades.SlotOneUpgrade();
            }
            else { }
        }
        if (ToggleTwoValue == 1)
        {
            if (enemyStats.GoldAmt >= slotUpgrades.slotTwoAmtArr[slotUpgrades.slotTwoLvl])
            {
                slotUpgrades.SlotTwoUpgrade();
            }
            else { }
        }
        if (ToggleThreeValue == 1)
        {
            if (enemyStats.GoldAmt >= slotUpgrades.slotThreeAmtArr[slotUpgrades.slotThreeLvl])
            {
                slotUpgrades.SlotThreeUpgrade();
            }
            else { }
        }
    }

    //Changes the autobuyer count and toggles visibility

    public void ToggleOne()
    {
        if (ToggleOneValue == 0 && AutoBuyerAmt > 0)
        {
            AutoBuyerAmt -= 1;
            ToggleOneValue = 1;
        }
        else if (ToggleOneValue == 1)
        {
            AutoBuyerAmt += 1;
            ToggleOneValue = 0;
        }
    }

    public void ToggleTwo()
    {
        if (ToggleTwoValue == 0 && AutoBuyerAmt > 0)
        {
            AutoBuyerAmt -= 1;
            ToggleTwoValue = 1;
        }
        else if (ToggleTwoValue == 1)
        {
            AutoBuyerAmt += 1;
            ToggleTwoValue = 0;
        }
    }
    public void ToggleThree()
    {
        if (ToggleThreeValue == 0 && AutoBuyerAmt > 0)
        {
            AutoBuyerAmt -= 1;
            ToggleThreeValue = 1;
        }
        else if (ToggleThreeValue == 1)
        {
            AutoBuyerAmt += 1;
            ToggleThreeValue = 0;
        }
    }

    public void CheckToggleVisibilty()
    {
        if (ToggleOneValue == 0)
        {
            AutoBuyerOneButton.SetActive(false);
        }
        else if (ToggleOneValue == 1)
        {
            AutoBuyerOneButton.SetActive(true);
        }
        //
        if (ToggleTwoValue == 0)
        {
            AutoBuyerTwoButton.SetActive(false);
        }
        else if (ToggleTwoValue == 1)
        {
            AutoBuyerTwoButton.SetActive(true);
        }
        //
        if (ToggleThreeValue == 0)
        {
            AutoBuyerThreeButton.SetActive(false);
        }
        else if (ToggleThreeValue == 1)
        {
            AutoBuyerThreeButton.SetActive(true);
        }
    }

    public void UpdateAutoBuyerText()
    {
        if (AutoBuyerAmtText != null)
        {
            AutoBuyerAmtText.text = AutoBuyerAmt + "/" + AutoBuyerMax;
        }
        if (AutoBuyerCostText != null && AutoBuyerLvl < AutoBuyerMax)
        {
            AutoBuyerCostText.text = "Cost: " + AutoBuyerCost[AutoBuyerLvl];
        }
    }

    public void PurchaseAutoBuyer()
    {
        if (AutoBuyerLvl < AutoBuyerMax)
        {
            if (prestige.prestigeMulti - 1 >= AutoBuyerCost[AutoBuyerLvl])
            {
                prestige.prestigeMulti -= AutoBuyerCost[AutoBuyerLvl];
                AutoBuyerLvl += 1;
                AutoBuyerAmt += 1;
            }
        }
        else
        {

        }
    }
}
