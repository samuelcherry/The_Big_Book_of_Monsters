using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Upgrades upgrades;
    public GameObject startScreen; // Reference to the start screen UI
    public GameObject roleSelect;
    public GameObject mainGame;

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
        roleSelect.SetActive(true);
    }

    public void StartGame(int index)
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
}
