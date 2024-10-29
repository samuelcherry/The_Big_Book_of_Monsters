using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class Prestige : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public SlotUpgrades slotUpgrades;
    public float baseXP;
    public float prestigeMulti;
    public TMP_Text currentMultiText;
    public TMP_Text futureMultiText;

    void Start()
    {
        prestigeMulti = 1;
        Debug.Log("From prestige, Multi: " + prestigeMulti);
    }

    void Update()
    {
        UpdatePrestigeText();
    }
    public void AddBaseXp()
    {
        baseXP += enemyStats.baseXpRwd;
        Debug.Log(baseXP);
    }

    public void PrestigeHero()
    {
        Debug.Log("Old multi " + prestigeMulti);
        prestigeMulti += baseXP / 10000;
        Debug.Log("New multi " + prestigeMulti);
        SoftRest();
    }

    public void SoftRest()
    {
        playerStats.level = 1;
        playerStats.currentXp = 0;
        slotUpgrades.slotOneLvl = 0;
        slotUpgrades.slotTwoLvl = 0;
        slotUpgrades.slotThreeLvl = 0;
        slotUpgrades.slotFourLvl = 0;
        playerStats.maxHp = playerStats.hpMaxArray[playerStats.level - 1] + slotUpgrades.slotOneAmtArr[slotUpgrades.slotOneLvl];
        playerStats.atk = playerStats.atkArr[playerStats.level - 1] + slotUpgrades.slotTwoAmtArr[slotUpgrades.slotTwoLvl];
        playerStats.def = playerStats.defArray[playerStats.level - 1] + slotUpgrades.slotThreeAmtArr[slotUpgrades.slotThreeLvl];
        playerStats.currentHp = playerStats.maxHp;
        enemyStats.GoldAmt = 0;

        enemyStats.Stage = 1;
        enemyStats.EnemyName = enemyStats.enemyNameArray[enemyStats.Stage - 1];
        enemyStats.EnemyMaxHp = enemyStats.enemyHpMax[enemyStats.Stage - 1];
        enemyStats.EnemyCurrentHp = enemyStats.EnemyMaxHp;
        enemyStats.EnemyDef = enemyStats.enemyDefArray[enemyStats.Stage - 1];
        enemyStats.baseXpRwd = enemyStats.xpRwdArray[enemyStats.Stage - 1];
        enemyStats.XpRwd = enemyStats.baseXpRwd * enemyStats.prestige.prestigeMulti;
        enemyStats.GoldRwd = enemyStats.goldRwdArray[enemyStats.Stage - 1] * enemyStats.prestige.prestigeMulti;
        enemyStats.EnemyAtk = enemyStats.enemyAtkArray[enemyStats.Stage - 1];
        enemyStats.GoldAmt = 0;
        enemyStats.enemyHpBar.value = enemyStats.EnemyCurrentHp / enemyStats.EnemyMaxHp;



        baseXP = 0;
        playerStats.UpdateStatText();
        enemyStats.UpdateEnemyStatsText();
        UpdatePrestigeText();

        saveManager.Save();

    }

    public void UpdatePrestigeText()
    {

        if (currentMultiText != null)
        {
            currentMultiText.text = prestigeMulti.ToString();
        }
        if (futureMultiText != null)
        {
            futureMultiText.text = (prestigeMulti + baseXP / 1000).ToString();
        }
    }


}