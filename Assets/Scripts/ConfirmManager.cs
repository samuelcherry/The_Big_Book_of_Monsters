using UnityEngine;

public class ConfirmManager : MonoBehaviour
{
    public SaveManager saveManager;

    public GameObject confirmMenu; // Reference to the pause menu UI
    private bool isVisible = false;

    void Start()
    {
        confirmMenu.SetActive(false); // Hide the pause menu at the start
    }


    public void ToggleShow()
    {
        isVisible = !isVisible;


        if (isVisible)
        {
            confirmMenu.SetActive(true); // Show the menu
        }
        else
        {
            confirmMenu.SetActive(false); // Show the menu
        }
    }

    public void Confirm()
    {
        saveManager.HardReset();
        ToggleShow();
    }
}
