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
    public Upgrades.Roles.Upgrade upgrade;
    public ProgressBarTimer progressBarTimer;
    public float baseXP;
    public float prestigeMulti;
    public TMP_Text currentMultiText, futureMultiText, roleSelectPrestigePoints;

    void Awake()
    {
        if (PlayerPrefs.GetFloat("PrestigeMulti") >= 1)
        {
            prestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");
        }
        else
        {
            prestigeMulti = 1;
        }
    }

    void Update()
    {
        UpdatePrestigeText();
    }
    public void AddBaseXp()
    {
        baseXP += enemyStats.currentAdventure.enemies[enemyStats.Stage - 1].xpRwd / prestigeMulti;
    }

    public void PrestigeHero()
    {
        prestigeMulti += baseXP / 10000;
        SoftRest();
    }

    public void SoftRest()
    {

        //XP RESET
        playerStats.level = 1;
        playerStats.currentXp = 0;
        playerStats.xpBar.value = playerStats.currentXp / playerStats.maxXP;
        baseXP = 0;


        //SLOT UPGRADES RESET
        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.slotStructs[i].slotLvl = 0;

            playerStats.maxHp = playerStats.hpMaxArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[0].slotAmtArr[slotUpgrades.slotStructs[0].slotLvl]);
            playerStats.atk = playerStats.atkArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl]);
            playerStats.def = playerStats.defArray[playerStats.level - 1] + Convert.ToInt32(slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl]);
        }

        //ENEMY STATS RESET

        enemyStats.Stage = 1;
        enemyStats.GoldAmt = 0;
        var currentEnemy = enemyStats.currentAdventure.enemies[enemyStats.Stage - 1];

        currentEnemy.enemyCurrentHp = currentEnemy.enemyMaxHp;
        enemyStats.enemyHpBar.value = currentEnemy.enemyCurrentHp / currentEnemy.enemyMaxHp;

        currentEnemy.xpRwd = currentEnemy.baseXpRwd * enemyStats.prestige.prestigeMulti;
        currentEnemy.goldRwd = currentEnemy.baseGoldRwd * enemyStats.prestige.prestigeMulti;

        progressBarTimer.enemyAtkTime = currentEnemy.enemySpeed;

        enemyStats.GoldAmt = 0;

        //PLAYER STATS RESET

        playerStats.currentHp = playerStats.maxHp;
        playerStats.atk *= playerStats.atkMetalCount * upgrades.atkPassiveMulti + 1;
        playerStats.def *= playerStats.defMetalCount * upgrades.defPassiveMulti + 1;
        playerStats.maxHp *= playerStats.hpMetalCount * upgrades.hpPassiveMulti + 1;
        progressBarTimer.playerAtkTime = playerStats.speedArray[playerStats.level - 1];

        //UPGRADES RESET

        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                upgrades.roles[r].upgrades[i].unlocked = false;
                upgrades.roles[r].upgrades[i].purchased = false;
                upgrades.roles[r].upgrades[i].blocked = false;
            }
        }

        //TEXT RESET

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
        if (roleSelectPrestigePoints != null)
        {
            roleSelectPrestigePoints.text = "Prestige Points: " + playerStats.FormatStatValue(prestigeMulti).ToString();
        }


    }
    public void UpdatePostPrestigeText()
    {
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            if (upgrades.roles[r].passiveAtkBonusText != null)
            {
                upgrades.roles[r].passiveAtkBonusText.text = "Passive ATK Bonus: \n" + playerStats.atkMetalCount * upgrades.atkPassiveMulti;
            }
            if (upgrades.roles[r].passiveDefBonusText != null)
            {
                upgrades.roles[r].passiveDefBonusText.text = "Passive DEF Bonus: \n" + playerStats.defMetalCount * upgrades.defPassiveMulti;
            }
            if (upgrades.roles[r].passiveHpBonusText != null)
            {
                upgrades.roles[r].passiveHpBonusText.text = "Passive HP Bonus: \n" + playerStats.hpMetalCount * upgrades.hpPassiveMulti;
            }
        }
    }



}