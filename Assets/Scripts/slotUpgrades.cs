using UnityEngine;
using TMPro;


public class SlotUpgrades : MonoBehaviour
{

    [System.Serializable]
    public struct SlotStruct
    {
        public TMP_Text slotCostText;
        public int slotLvl;
        public float[] slotCostArr;
        public int[] slotAmtArr;
        public string StatBoost;
    }

    public SlotStruct[] slotStructs = new SlotStruct[3];
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public SaveManager saveManager;

    void Awake()
    {
        //upgrade level
        for (int i = 0; i < slotStructs.Length; i++)
        {
            slotStructs[i].slotLvl = 0;
        }
        slotStructs[0].slotAmtArr = new int[] { 0, 10, 20, 30, 50, 70, 90, 120, 150, 180, 210, 240, 280, 320, 360, 400, 450, 500, 550, 600, 700 };
        slotStructs[1].slotAmtArr = new int[] { 0, 1, 2, 3, 5, 7, 9, 12, 15, 18, 21, 24, 28, 32, 36, 40, 45, 50, 55, 60, 70 };
        slotStructs[2].slotAmtArr = new int[] { 0, 1, 2, 3, 5, 7, 9, 12, 15, 18, 21, 24, 28, 32, 36, 40, 45, 50, 55, 60, 70 };

        slotStructs[0].slotCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000 };
        slotStructs[1].slotCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000 };
        slotStructs[2].slotCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000 };

        slotStructs[0].StatBoost = "MAXHP";
        slotStructs[1].StatBoost = "ATK";
        slotStructs[2].StatBoost = "DEF";

        for (int i = 0; i < slotStructs.Length; i++)
        {
            UpdateSlotText(i);
        }


    }
    public void SlotUpgrade(int index)
    {
        if (slotStructs[index].slotLvl < slotStructs[index].slotAmtArr.Length - 1)
        {
            if (enemyStats.GoldAmt >= slotStructs[index].slotCostArr[slotStructs[index].slotLvl])
            {
                enemyStats.GoldAmt -= slotStructs[index].slotCostArr[slotStructs[index].slotLvl];
                slotStructs[index].slotLvl += 1;
                UpdateSlotText(index);
                playerStats.UpdateStats();
                playerStats.UpdateGoldText();
                playerStats.UpdateHpText();
                saveManager.Save();
            }
        }
    }

    public void UpdateSlotText(int index)
    {
        var upgradeSlot = slotStructs[index];
        if (upgradeSlot.slotLvl < upgradeSlot.slotAmtArr.Length - 1)
        {
            if (upgradeSlot.slotCostText != null)
            {
                upgradeSlot.slotCostText.text = "Cost: " + upgradeSlot.slotCostArr[upgradeSlot.slotLvl];
            }
        }
        else if (upgradeSlot.slotLvl >= upgradeSlot.slotAmtArr.Length - 1)
        {
            upgradeSlot.slotCostText.text = "Cost: MAX";
        }
    }
}



