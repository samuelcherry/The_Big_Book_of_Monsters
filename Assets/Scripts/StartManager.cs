using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startScreen; // Reference to the start screen UI
    private bool gameStarted = false;

    void Start()
    {
        // Display the start screen at the beginning
        startScreen.SetActive(true);
        
        // Set the game to pause
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        // Hide the start screen
        startScreen.SetActive(false);
        
        // Begin the game by setting timeScale to 1
        Time.timeScale = 1;
        
        gameStarted = true;
    }

    void Update()
    {
        // Optional: Check for pressing Enter or other key to start game
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }
}
