
using System;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public AlchemyTimers alchemyTimers;
    public Bestiary bestiary;
    public BuffManager buffManager;
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public Prestige prestige;
    public ProgressBarTimer progressBarTimer;
    public SaveManager saveManager;
    public StartScreenManager startScreenManager;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
    public BlacksmithToggleManager blacksmithToggleManager;
    public Inventory playerInventory;
    public ListToText listToText;

    public void SoftReset()
    {

        //XP RESET
        playerStats.level = 1;
        playerStats.currentXp = 0;
        playerStats.xpBar.value = playerStats.currentXp / playerStats.maxXP;
        prestige.baseXP = 0;


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
        playerStats.roleChoosen = 0;

        //ALCHEMY RESET

        for (int i = 0; i < alchemyTimers.alchemyProgressBar.Length; i++)
        {
            alchemyTimers.alchemyToggles[i].isOn = false;
            alchemyTimers.alchemyProgressBar[i].previousToggleStates = false;
            alchemyTimers.AlchAutoBuyerAmt = alchemyTimers.AlchAutoBuyerLvl;
        }

        //TEXT RESET

        playerStats.UpdateStatText();
        enemyStats.UpdateEnemyStatsText();
        prestige.UpdatePrestigeText();
        prestige.UpdatePostPrestigeText();

        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.UpdateSlotText(i);
        }


        //BUFF RESET
        buffManager.activeBuffs.Clear();
        buffManager.activeBuffnames.Clear();



        playerInventory.LoadInventory();
        saveManager.Save();

    }


    public void HardReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        startScreenManager.StartButton();

        playerStats.level = 1;
        playerStats.currentXp = 0;
        playerStats.xpBar.value = playerStats.currentXp / playerStats.maxXP;
        prestige.baseXP = 0;
        prestige.prestigeMulti = 1;
        playerStats.roleChoosen = 0;

        //ADVENTURE RESET
        for (int i = 0; i < enemyStats.adventures.Length; i++)
        {
            enemyStats.adventures[i].isCompleted = 0;
            enemyStats.adventures[i].prevCompleted = 0;
        }
        enemyStats.tempAdventureNumber = 0;
        enemyStats.currentAdventure = enemyStats.adventures[enemyStats.tempAdventureNumber];

        playerStats.atkMetalCount = 0;
        playerStats.defMetalCount = 0;
        playerStats.hpMetalCount = 0;



        prestige.UpdatePrestigeText();
        prestige.UpdatePostPrestigeText();
        playerStats.UpdateStatText();
        enemyStats.UpdateEnemyStatsText();

        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.UpdateSlotText(i);
        }

        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                upgrades.roles[r].upgrades[i].metalCount = 0;
            }
        }

        for (int r = 1; r < upgrades.roles.Length; r++)
        {
            upgrades.roles[r].roleUnlocked = 0;
        }

        blacksmithToggleManager.AutoBuyerLvl = 0;
        blacksmithToggleManager.AutoBuyerAmt = 0;


        //ALCHEMY RESET
        alchemyTimers.AlchAutoBuyerAmt = 0;
        alchemyTimers.AlchAutoBuyerLvl = 0;
        for (int i = 0; i < alchemyTimers.alchemyProgressBar.Length; i++)
        {
            alchemyTimers.alchemyProgressBar[i].alchLvl = 1;
            alchemyTimers.alchemyProgressBar[i].alchXP = 0;
            alchemyTimers.alchemyProgressBar[i].rwd = 0;
            alchemyTimers.alchemyProgressBar[i].alchLvlBar.value = 0;
            alchemyTimers.alchemyProgressBar[i].progressBar.value = 0;
            alchemyTimers.alchemyProgressBar[i].totalTime = alchemyTimers.alchemyProgressBar[i].baseTime;
            alchemyTimers.alchemyProgressBar[i].timeLeft = alchemyTimers.alchemyProgressBar[i].totalTime;
            alchemyTimers.alchemyProgressBar[i].limit = 10;
            alchemyTimers.alchemyProgressBar[i].alchMaxXp = 20;

        }

        //BOOK RESET
        for (int i = 0; i < bestiary.entry.Length; i++)
        {
            bestiary.entry[i].IsDefeated = 0;
        }

        //INVENTORY RESET
        playerInventory.ClearInventory();
        listToText.PopulateText(playerInventory.sampleList);

        SoftReset();
    }
}