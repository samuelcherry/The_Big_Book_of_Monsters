using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Upgrades upgrades;
    public Prestige prestige;
    public SaveManager saveManager;
    public EnemyStats enemyStats;
    public GameObject startScreen; // Reference to the start screen UI
    public GameObject roleSelect;
    public GameObject mainGame;
    public MenuManager menuManager;
    public Inventory playerInventory;

    public int roleCost = 5;

    void Start()
    {
        // Display the start screen at the beginning
        startScreen.SetActive(true);
        roleSelect.SetActive(false);
        mainGame.SetActive(false);
        playerInventory.LoadInventory();


        if (enemyStats.adventures[enemyStats.tempAdventureNumber] != enemyStats.currentAdventure)
        {
            enemyStats.currentAdventure = enemyStats.adventures[enemyStats.tempAdventureNumber];
            enemyStats.ResetEnemies();
            enemyStats.Stage = 1;
            enemyStats.progressBarTimer.SetStageAnimation();
        }


        // Set the game to pause
        Time.timeScale = 0;
    }

    public void StartButton()
    {
        startScreen.SetActive(false);
        mainGame.SetActive(false);
        saveManager.Load();
        Debug.Log("START MANAGER LOAD");
        prestige.UpdatePrestigeText();
        upgrades.roles[0].roleUnlocked = 1;


        if (playerStats.roleChoosen == 0)
        {
            roleSelect.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            mainGame.SetActive(true);
            Time.timeScale = 1;
        }
        menuManager.MenuMove(0);
        playerInventory.LoadInventory();
    }

    public void StartGame(int index)
    {
        if (upgrades.roles[index].roleUnlocked == 1)
        {
            // Hide the start screen
            roleSelect.SetActive(false);
            mainGame.SetActive(true);
            //SELECTS ROLE
            playerStats.role = index;
            playerStats.roleChoosen = 1;

            // Begin the game by setting timeScale to 1
            Time.timeScale = 1;
            saveManager.Save();
        }//Unlocking new role
        else if (upgrades.roles[index].roleUnlocked == 0 && prestige.prestigeMulti > roleCost + 1)
        {
            var currentEnemy = enemyStats.currentAdventure.enemies[enemyStats.Stage - 1];
            prestige.prestigeMulti -= roleCost;
            currentEnemy.goldRwd = currentEnemy.baseGoldRwd * prestige.prestigeMulti;
            currentEnemy.xpRwd = currentEnemy.baseXpRwd * prestige.prestigeMulti;
            upgrades.roles[index].roleUnlocked = 1;
            saveManager.Save();
        }
        menuManager.MenuMove(0);
    }
}
