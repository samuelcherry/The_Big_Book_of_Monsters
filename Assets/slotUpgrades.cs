using UnityEngine;
using TMPro;


public class SlotUpgrades : MonoBehaviour
{

    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public SaveManager saveManager;

    public TMP_Text slotOneCostText;
    public TMP_Text slotTwoCostText;
    public TMP_Text slotThreeCostText;
    public TMP_Text slotFourCostText;

    public int slotOneLvl = 0;
    public int slotTwoLvl = 0;
    public int slotThreeLvl = 0;
    public int slotFourLvl = 0;


    public float[] slotOneCostArr = new float[] { 100, 1000, 10000, 100000 };
    public float[] slotTwoCostArr = new float[] { 100, 1000, 10000, 100000 };
    public float[] slotThreeCostArr = new float[] { 100, 1000, 10000, 100000 };
    public float[] slotFourCostArr = new float[] { 100, 1000, 10000, 100000 };
    public int[] slotOneAmtArr = new int[] { 0, 10, 30, 50, 100 };
    public int[] slotTwoAmtArr = new int[] { 0, 10, 30, 50, 100 };
    public int[] slotThreeAmtArr = new int[] { 0, 10, 30, 50, 100 };
    public int[] slotFourAmtArr = new int[] { 0, 10, 30, 50, 100 };

    void Start()
    {
        //saveManager.Load();
        UpdateSlotText();
        slotOneLvl = 0;
        slotTwoLvl = 0;
        slotTwoLvl = 0;
        slotTwoLvl = 0;
    }

    void Update()
    {
        UpdateSlotText();
    }

    public void SlotOneUpgrade()
    {
        if (slotOneLvl < slotOneAmtArr.Length + 1)
        {
            if (enemyStats.GoldAmt >= slotOneCostArr[slotOneLvl])
            {
                enemyStats.GoldAmt -= slotOneCostArr[slotOneLvl];
                slotOneLvl += 1;
                playerStats.maxHp += slotOneAmtArr[slotOneLvl];
                UpdateSlotText();
                playerStats.UpdateStatText();
                saveManager.Save();
            }
            else
            {
            }
        }
    }
    public void SlotTwoUpgrade()
    {
        if (slotTwoLvl < slotTwoAmtArr.Length + 1)
        {
            if (enemyStats.GoldAmt >= slotTwoCostArr[slotTwoLvl])
            {
                enemyStats.GoldAmt -= slotTwoCostArr[slotTwoLvl];
                slotTwoLvl += 1;
                playerStats.atk += slotTwoAmtArr[slotTwoLvl];
                UpdateSlotText();
                playerStats.UpdateStatText();
                saveManager.Save();
            }
            else
            {
            }
        }
    }
    public void SlotThreeUpgrade()
    {
        if (slotThreeLvl < slotThreeAmtArr.Length + 1)
        {
            if (enemyStats.GoldAmt >= slotThreeCostArr[slotThreeLvl])
            {
                enemyStats.GoldAmt -= slotThreeCostArr[slotThreeLvl];
                slotThreeLvl += 1;
                playerStats.def += slotThreeAmtArr[slotThreeLvl];
                UpdateSlotText();
                playerStats.UpdateStatText();
                saveManager.Save();
            }
            else
            {
            }
        }
    }
    public void SlotFourUpgrade()
    {
        if (slotFourLvl < slotFourAmtArr.Length + 1)
        {
            if (enemyStats.GoldAmt >= slotFourCostArr[slotFourLvl])
            {
                enemyStats.GoldAmt -= slotFourCostArr[slotFourLvl];
                slotFourLvl += 1;
                playerStats.atk += slotFourAmtArr[slotFourLvl];
                UpdateSlotText();
                playerStats.UpdateStatText();
                saveManager.Save();
            }
            else
            {
            }
        }
    }
    public void UpdateSlotText()
    {
        if (slotOneLvl < slotOneAmtArr.Length + 1)
        {
            if (slotOneCostText != null)
            {
                slotOneCostText.text = "Cost: " + slotOneCostArr[slotOneLvl].ToString();
            }
        }
        if (slotTwoLvl < slotTwoAmtArr.Length + 1)
        {
            if (slotTwoCostText != null)
            {
                slotTwoCostText.text = "Cost: " + slotTwoCostArr[slotTwoLvl].ToString();
            }
        }
        if (slotThreeLvl < slotThreeAmtArr.Length + 1)
        {
            if (slotThreeCostText != null)
            {
                slotThreeCostText.text = "Cost: " + slotThreeCostArr[slotThreeLvl].ToString();
            }
        }
        if (slotFourLvl < slotFourAmtArr.Length + 1)
        {
            if (slotFourCostText != null)
            {
                slotFourCostText.text = "Cost: " + slotFourCostArr[slotFourLvl].ToString();
            }
            if (slotOneLvl < slotOneAmtArr.Length + 1)
            {
            }
        }
    }

}



