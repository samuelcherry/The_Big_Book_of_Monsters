using System;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class Prestige : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
    public Upgrades.Upgrade upgrade;
    public float baseXP;
    public float prestigeMulti;
    public TMP_Text currentMultiText;
    public TMP_Text futureMultiText;

    void Start()
    {
        prestigeMulti = 1;
    }

    void Update()
    {
        UpdatePrestigeText();
    }
    public void AddBaseXp()
    {
        baseXP += enemyStats.baseXpRwd;
    }

    public void PrestigeHero()
    {
        prestigeMulti += baseXP / 10000;
        SoftRest();
    }

    public void SoftRest()
    {
        playerStats.level = 1;
        playerStats.currentXp = 0;

        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.slotStructs[i].slotLvl = 0;

            playerStats.maxHp = playerStats.hpMaxArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[0].slotAmtArr[slotUpgrades.slotStructs[0].slotLvl]);
            playerStats.atk = playerStats.atkArr[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl]);
            playerStats.def = playerStats.defArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl]);
        }
        playerStats.currentHp = playerStats.maxHp;
        enemyStats.GoldAmt = 0;



        playerStats.atk += playerStats.atkMetalCount * upgrades.atkPassiveMulti;
        playerStats.def += playerStats.defMetalCount * upgrades.defPassiveMulti;
        playerStats.maxHp += playerStats.hpMetalCount * upgrades.hpPassiveMulti;

        for (int i = 0; i < upgrades.upgrades.Length; i++)
        {
            upgrades.upgrades[i].unlocked = false;
            upgrades.upgrades[i].purchased = false;
            upgrades.upgrades[i].blocked = false;
        }

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
        UpdatePostPrestigeText();

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
            futureMultiText.text = (prestigeMulti + baseXP / 10000).ToString();
        }

    }
    public void UpdatePostPrestigeText()
    {
        if (playerStats.passiveAtkBonusText != null)
        {
            playerStats.passiveAtkBonusText.text = "Passive ATK Bonus: \n" + playerStats.atkMetalCount * upgrades.atkPassiveMulti;
        }
        if (playerStats.passiveDefBonusText != null)
        {
            playerStats.passiveDefBonusText.text = "Passive DEF Bonus: \n" + playerStats.defMetalCount * upgrades.defPassiveMulti;
        }
        if (playerStats.passiveHpBonusText != null)
        {
            playerStats.passiveHpBonusText.text = "Passive Hp Bonus: \n" + playerStats.hpMetalCount * upgrades.hpPassiveMulti;
        }
    }



}