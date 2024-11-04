using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithToggleManager : MonoBehaviour
{

    [System.Serializable]
    public struct ToggleButton
    {
        public Button autoBuyer;
        public GameObject autoBuyerButton;
        public int toggleValue;
    }

    public ToggleButton[] toggleButtons = new ToggleButton[3];
    public SlotUpgrades slotUpgrades;
    public EnemyStats enemyStats;
    public Prestige prestige;
    public int AutoBuyerAmt = 1;
    public int AutoBuyerMax = 3;
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
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            CheckGold(i);
        }
        UpdateAutoBuyerText();
        CheckToggleVisibilty();
    }

    public void CheckGold(int index)
    {
        if (toggleButtons[index].toggleValue == 1)
        {
            if (enemyStats.GoldAmt >= slotUpgrades.slotStructs[index].slotAmtArr[slotUpgrades.slotStructs[index].slotLvl])
            {
                slotUpgrades.SlotUpgrade(index);
            }
            else { }
        }
    }

    //Changes the autobuyer count and toggles visibility

    public void ShowButtons(int index)
    {
        if (toggleButtons[index].toggleValue == 0 && AutoBuyerAmt > 0)
        {
            AutoBuyerAmt -= 1;
            toggleButtons[index].toggleValue = 1;
        }
        else if (toggleButtons[index].toggleValue == 1)
        {
            AutoBuyerAmt += 1;
            toggleButtons[index].toggleValue = 0;
        }
    }


    public void CheckToggleVisibilty()
    {
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            if (toggleButtons[i].toggleValue == 0)
            {
                toggleButtons[i].autoBuyerButton.SetActive(false);
            }
            else if (toggleButtons[i].toggleValue == 1)
            {
                toggleButtons[i].autoBuyerButton.SetActive(true);
            }
        }
    }

    public void UpdateAutoBuyerText()
    {
        if (AutoBuyerAmtText != null)
        {
            AutoBuyerAmtText.text = AutoBuyerLvl + "/" + AutoBuyerMax;
        }
        if (AutoBuyerCostText != null && AutoBuyerLvl < AutoBuyerMax)
        {
            AutoBuyerCostText.text = "Cost: " + AutoBuyerCost[AutoBuyerLvl] + " Prestige points";
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
            }
        }
        else
        {

        }
    }
}
