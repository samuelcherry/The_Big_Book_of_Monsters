using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu UI
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false); // Hide the pause menu at the start
    }

    void Update()
    {
        // Check for the pause button input (e.g., Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Stop the game time
        pauseMenu.SetActive(true); // Show the pause menu
        // You can also pause other systems like audio or animations here
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game time
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void QuitGame()
    {
        // Implement your quitting logic here
        // e.g., Application.Quit(); for standalone builds
        // or SceneManager.LoadScene("MainMenu") for loading a main menu scene
    }
}
