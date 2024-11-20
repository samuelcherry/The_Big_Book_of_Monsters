using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithToggleManager : MonoBehaviour
{
    public Toggle[] AutobuyerButtons = new Toggle[3];
    public SlotUpgrades slotUpgrades;
    public EnemyStats enemyStats;
    public Prestige prestige;
    public int AutoBuyerAmt = 1;
    public int AutoBuyerMax = 3;
    public int[] AutoBuyerCost;
    public int AutoBuyerLvl;
    public TMP_Text AutoBuyerAmtText;
    public TMP_Text AutoBuyerCostText;
    private bool[] previousToggleStates;

    void Start()
    {
        AutoBuyerAmt = AutoBuyerLvl;
        AutoBuyerCost = new int[] { 10, 15, 25 };

        previousToggleStates = new bool[AutobuyerButtons.Length];
        for (int i = 0; i < AutobuyerButtons.Length; i++)
        {
            previousToggleStates[i] = AutobuyerButtons[i].isOn;
        }
    }

    void Update()
    {
        for (int i = 0; i < AutobuyerButtons.Length; i++)
        {
            CheckGold(i);
        }
        UpdateAutoBuyerText();
    }

    public void CheckGold(int index)
    {
        if (AutobuyerButtons[index].isOn)
        {
            if (enemyStats.GoldAmt >= slotUpgrades.slotStructs[index].slotAmtArr[slotUpgrades.slotStructs[index].slotLvl])
            {
                if (slotUpgrades.slotStructs[index].slotLvl < slotUpgrades.slotStructs[index].slotAmtArr.Length - 1)
                    slotUpgrades.SlotUpgrade(index);
            }
        }
    }

    //Changes the autobuyer count and toggles visibility

    public void ShowButtons(int index)
    {
        if (AutoBuyerAmt > 0 && AutobuyerButtons[index].isOn && !previousToggleStates[index])
        {
            AutoBuyerAmt -= 1;
            AutobuyerButtons[index].isOn = true;
            previousToggleStates[index] = true;
        }
        else if (AutoBuyerAmt <= 0 && AutobuyerButtons[index].isOn && !previousToggleStates[index])
        {
            AutobuyerButtons[index].isOn = false;
            previousToggleStates[index] = false;
        }
        else if (!AutobuyerButtons[index].isOn && previousToggleStates[index])
        {
            AutoBuyerAmt += 1;
            AutobuyerButtons[index].isOn = false;
            previousToggleStates[index] = false;
        }
        else
        {
            AutobuyerButtons[index].isOn = false;
            previousToggleStates[index] = false;
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
            AutoBuyerCostText.text = "Cost: " + AutoBuyerCost[AutoBuyerLvl] + "\nPrestige points";
        }
        else if (AutoBuyerLvl >= AutoBuyerMax)
        {
            AutoBuyerCostText.text = "Cost: MAX";
        }
    }

    public void PurchaseAutoBuyer()
    {
        if (AutoBuyerLvl < AutoBuyerMax)
        {
            if (prestige.prestigeMulti - 1 >= AutoBuyerCost[AutoBuyerLvl])
            {
                prestige.prestigeMulti -= AutoBuyerCost[AutoBuyerLvl];
                enemyStats.ResetPrestige();
                AutoBuyerAmt += 1;
                AutoBuyerLvl += 1;
            }
        }
        else
        {

        }
    }
}
