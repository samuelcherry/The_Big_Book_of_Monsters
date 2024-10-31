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


    public int slotOneLvl = 0;
    public int slotTwoLvl = 0;
    public int slotThreeLvl = 0;


    public float[] slotOneCostArr = new float[] { 100, 1000, 10000, 100000 };
    public float[] slotTwoCostArr = new float[] { 100, 1000, 10000, 100000 };
    public float[] slotThreeCostArr = new float[] { 100, 1000, 10000, 100000 };


    [HideInInspector]
    public int[] slotOneAmtArr;
    [HideInInspector]
    public int[] slotTwoAmtArr;
    [HideInInspector]
    public int[] slotThreeAmtArr;

    void Start()
    {
        //saveManager.Load();
        UpdateSlotText();
        slotOneLvl = 0;
        slotTwoLvl = 0;
        slotThreeLvl = 0;
        slotOneAmtArr = new int[] { 0, 10, 30, 50, 100 };
        slotTwoAmtArr = new int[] { 0, 10, 20, 30, 40 };
        slotThreeAmtArr = new int[] { 0, 2, 5, 8, 10 };


    }

    void Update()
    {
        UpdateSlotText();
    }

    public void SlotOneUpgrade()
    {
        if (slotOneLvl < slotOneAmtArr.Length)
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
        }
    }
    public void SlotTwoUpgrade()
    {
        if (slotTwoLvl < slotTwoAmtArr.Length)
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
        }
    }
    public void SlotThreeUpgrade()
    {
        if (slotThreeLvl < slotThreeAmtArr.Length)
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
        }
    }
    
    public void UpdateSlotText()
    {
        if (slotOneLvl < slotOneAmtArr.Length - 1)
        {
            if (slotOneCostText != null)
            {
                slotOneCostText.text = "Cost: " + slotOneCostArr[slotOneLvl].ToString();
            }
        }
        if (slotTwoLvl < slotTwoAmtArr.Length - 1)
        {
            if (slotTwoCostText != null)
            {
                slotTwoCostText.text = "Cost: " + slotTwoCostArr[slotTwoLvl].ToString();
            }
        }
        if (slotThreeLvl < slotThreeAmtArr.Length - 1)
        {
            if (slotThreeCostText != null)
            {
                slotThreeCostText.text = "Cost: " + slotThreeCostArr[slotThreeLvl].ToString();
            }
        }
    }
}



