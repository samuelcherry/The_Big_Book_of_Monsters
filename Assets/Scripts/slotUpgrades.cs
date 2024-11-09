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
        Debug.Log("SlotUpgrades Awake");
        //upgrade level
        for (int i = 0; i < slotStructs.Length; i++)
        {
            slotStructs[i].slotLvl = 0;
        }
        slotStructs[0].slotAmtArr = new int[] { 0, 10, 20, 30, 50, 70, 90, 120, 150, 180, 210, 240, 280, 320, 360, 400, 450, 500, 550, 600 };
        slotStructs[1].slotAmtArr = new int[] { 0, 1, 2, 3, 5, 7, 9, 12, 15, 18, 21, 24, 28, 32, 36, 40, 45, 50, 55, 60 };
        slotStructs[2].slotAmtArr = new int[] { 0, 1, 2, 3, 5, 7, 9, 12, 15, 18, 21, 24, 28, 32, 36, 40, 45, 50, 55, 60 };

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
        if (slotStructs[index].slotLvl < slotStructs[index].slotAmtArr.Length)
        {
            if (enemyStats.GoldAmt >= slotStructs[index].slotCostArr[slotStructs[index].slotLvl])
            {
                enemyStats.GoldAmt -= slotStructs[index].slotCostArr[slotStructs[index].slotLvl];
                slotStructs[index].slotLvl += 1;

                UpdateSlotText(index);
                playerStats.UpdateStats();
                playerStats.UpdateGoldText();
                playerStats.UpdateHpText();
                playerStats.UpdateAtkText();
                playerStats.UpdateDefText();
                saveManager.Save();
            }
        }
    }

    public void UpdateSlotText(int index)
    {
        if (slotStructs[index].slotLvl < slotStructs[index].slotAmtArr.Length - 1)
        {
            if (slotStructs[index].slotCostText != null)
            {
                slotStructs[index].slotCostText.text = "Cost: " + slotStructs[index].slotCostArr[slotStructs[index].slotLvl];
            }
        }
        else if (slotStructs[index].slotLvl >= slotStructs[index].slotAmtArr.Length - 1)
        {
            slotStructs[index].slotCostText.text = "Cost: MAX";
        }
    }
}



