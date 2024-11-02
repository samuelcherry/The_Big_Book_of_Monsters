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


    public int slotOneLvl;
    public int slotTwoLvl;
    public int slotThreeLvl;


    public float[] slotOneCostArr;
    public float[] slotTwoCostArr;
    public float[] slotThreeCostArr;

    public int[] slotOneAmtArr;
    public int[] slotTwoAmtArr;
    public int[] slotThreeAmtArr;

    void Start()
    {


        //upgrade level
        UpdateSlotText();
        slotOneLvl = 0;
        slotTwoLvl = 0;
        slotThreeLvl = 0;
        //Increase per upgrade
        slotOneAmtArr = new int[] { 0,10,10,10,20,20,20,30,30,30,30,30,40,40,40,50,50,50,50,50};
        slotTwoAmtArr = new int[] { 0,1,1,1,2,2,2,3,3,3,3,3,4,4,4,5,5,5,5,5};
        slotThreeAmtArr = new int[] { 0,1,1,1,2,2,2,3,3,3,3,3,4,4,4,5,5,5,5,5};
        //Cost per upgrade
        slotOneCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000};
        slotTwoCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000};
        slotThreeCostArr = new float[] { 5, 10, 15, 20, 30, 40, 50, 100, 150, 200, 250, 500, 1000, 1250, 1500, 2000, 3000, 5000, 10000, 20000};

        
        saveManager.Load();

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
        }else if (slotOneLvl >= slotOneAmtArr.Length -1)
        {
            slotOneCostText.text = "Cost: MAX";
        }
        if (slotTwoLvl < slotTwoAmtArr.Length - 1)
        {
            if (slotTwoCostText != null)
            {
                slotTwoCostText.text = "Cost: " + slotTwoCostArr[slotTwoLvl].ToString();
            }
        
        }else if (slotTwoLvl >= slotTwoAmtArr.Length -1)
        {
            slotTwoCostText.text = "Cost: MAX";
        }
        if (slotThreeLvl < slotThreeAmtArr.Length - 1)
        {
            if (slotThreeCostText != null)
            {
                slotThreeCostText.text = "Cost: " + slotThreeCostArr[slotThreeLvl].ToString();
            }
        }else if (slotThreeLvl >= slotThreeAmtArr.Length -1)
        {
            slotThreeCostText.text = "Cost: MAX";
        }
    }
}



