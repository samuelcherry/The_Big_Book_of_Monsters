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
    }

    public SlotStruct[] slotStructs = new SlotStruct[3];

    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public SaveManager saveManager;

    void Start()
    {
        //upgrade level
        UpdateSlotText();
        for (int i = 0; i < slotStructs.Length; i++)
        {
            slotStructs[i].slotLvl = 0;
        }
        slotStructs[0].slotAmtArr = new int[] { 0, 10, 10, 10, 20, 20, 20, 30, 30, 30, 30, 30, 40, 40, 40, 50, 50, 50, 50, 50 };
        slotStructs[1].slotAmtArr = new int[] { 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5, 5, 5 };
        slotStructs[2].slotAmtArr = new int[] { 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5, 5, 5 };

        slotStructs[0].slotCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000 };
        slotStructs[1].slotCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000 };
        slotStructs[2].slotCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000 };


        saveManager.Load();

    }

    void Update()
    {
        UpdateSlotText();
    }

    public void SlotUpgrade(int index)
    {
        if (slotStructs[index].slotLvl < slotStructs[index].slotAmtArr.Length)
        {
            if (enemyStats.GoldAmt >= slotStructs[index].slotCostArr[slotStructs[index].slotLvl])
            {
                enemyStats.GoldAmt -= slotStructs[index].slotCostArr[slotStructs[index].slotLvl];
                slotStructs[index].slotLvl += 1;
                playerStats.maxHp += slotStructs[index].slotAmtArr[slotStructs[index].slotLvl];
                UpdateSlotText();
                playerStats.UpdateStatText();
                saveManager.Save();
            }
        }
    }

    public void UpdateSlotText()
    {
        for (int i = 0; i < slotStructs.Length; i++)
        {
            if (slotStructs[i].slotLvl < slotStructs[i].slotAmtArr.Length - 1)
            {
                if (slotStructs[i].slotCostText != null)
                {
                    slotStructs[i].slotCostText.text = "Cost: " + slotStructs[i].slotCostArr[slotStructs[i].slotLvl].ToString();
                }
            }
            else if (slotStructs[i].slotLvl >= slotStructs[i].slotAmtArr.Length - 1)
            {
                slotStructs[i].slotCostText.text = "Cost: MAX";
            }
        }
    }
}



