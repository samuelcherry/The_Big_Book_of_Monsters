using System;
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
    public ProgressBarTimer progressBarTimer;
    public float baseXP;
    public float prestigeMulti;
    public TMP_Text currentMultiText;
    public TMP_Text futureMultiText;

    void Awake()
    {
        Debug.Log("Prestige Awake");
        if (PlayerPrefs.GetFloat("PrestigeMulti") >= 1)
        {
            Debug.Log("Prestige is not equal to 1");
            prestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");
        }
        else
        {
            prestigeMulti = 1;
        }
        Debug.Log(prestigeMulti);
    }

    void Update()
    {
        UpdatePrestigeText();
    }
    public void AddBaseXp()
    {
        baseXP += enemyStats.enemies[enemyStats.Stage - 1].xpRwd / prestigeMulti;
        Debug.Log("BaseXP: " + baseXP + " XpRwd: " + enemyStats.enemies[enemyStats.Stage - 1].xpRwd);
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
            playerStats.atk = playerStats.atkArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl]);
            playerStats.def = playerStats.defArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl]);
        }

        playerStats.currentHp = playerStats.maxHp;
        enemyStats.GoldAmt = 0;


        playerStats.atk *= playerStats.atkMetalCount * upgrades.atkPassiveMulti + 1;
        playerStats.def *= playerStats.defMetalCount * upgrades.defPassiveMulti + 1;
        playerStats.maxHp *= playerStats.hpMetalCount * upgrades.hpPassiveMulti + 1;
        progressBarTimer.totalTime = playerStats.speedArray[playerStats.level];

        for (int i = 0; i < upgrades.upgrades.Length; i++)
        {
            upgrades.upgrades[i].unlocked = false;
            upgrades.upgrades[i].purchased = false;
            upgrades.upgrades[i].blocked = false;
        }

        var currentEnemy = enemyStats.enemies[enemyStats.Stage - 1];

        currentEnemy.enemyCurrentHp = currentEnemy.enemyMaxHp;
        enemyStats.enemyHpBar.value = currentEnemy.enemyCurrentHp / currentEnemy.enemyMaxHp;

        currentEnemy.xpRwd = currentEnemy.baseXpRwd * enemyStats.prestige.prestigeMulti;
        currentEnemy.goldRwd = currentEnemy.baseGoldRwd * enemyStats.prestige.prestigeMulti;

        progressBarTimer.enemyAtkTime = enemyStats.enemies[enemyStats.Stage - 1].enemySpeed;

        enemyStats.GoldAmt = 0;

        baseXP = 0;
        playerStats.UpdateStatText();
        enemyStats.UpdateEnemyStatsText();
        UpdatePrestigeText();
        UpdatePostPrestigeText();

        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.UpdateSlotText(i);
        }


        saveManager.Save();

    }

    public void UpdatePrestigeText()
    {

        if (currentMultiText != null)
        {
            currentMultiText.text = playerStats.FormatStatValue(prestigeMulti).ToString();
        }
        if (futureMultiText != null)
        {
            futureMultiText.text = playerStats.FormatStatValue(prestigeMulti + baseXP / 10000).ToString();
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