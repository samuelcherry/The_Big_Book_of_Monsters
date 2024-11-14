using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Upgrades upgrades;
    public Prestige prestige;
    public GameObject startScreen; // Reference to the start screen UI
    public GameObject roleSelect;
    public GameObject mainGame;

    public int roleCost = 5;

    void Start()
    {
        // Display the start screen at the beginning
        startScreen.SetActive(true);
        roleSelect.SetActive(false);
        mainGame.SetActive(false);

        // Set the game to pause
        Time.timeScale = 0;
    }

    public void StartButton()
    {
        startScreen.SetActive(false);
        mainGame.SetActive(false);
        roleSelect.SetActive(true);

        Time.timeScale = 0;
    }

    public void StartGame(int index)
    {
        if (upgrades.roles[index].roleUnlocked == true)
        {
            // Hide the start screen
            roleSelect.SetActive(false);
            mainGame.SetActive(true);
            Debug.Log(playerStats.role);
            playerStats.role = index;
            Debug.Log(playerStats.role);

            // Begin the game by setting timeScale to 1
            Time.timeScale = 1;
        }
        else if (upgrades.roles[index].roleUnlocked == false && prestige.prestigeMulti > roleCost + 1)
        {
            prestige.prestigeMulti -= roleCost;
            upgrades.roles[index].roleUnlocked = true;
        }
    }
}
