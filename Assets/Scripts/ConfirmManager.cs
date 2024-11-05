using UnityEngine;

public class ConfirmManager : MonoBehaviour
{
    public SaveManager saveManager;

    [System.Serializable]
    public struct ConfirmMenu
    {
        public GameObject Menu;
        public bool isVisible;
    }


    public ConfirmMenu[] confirmMenus = new ConfirmMenu[3];


    void Start()
    {
        for (int i = 0; i < confirmMenus.Length; i++)
        {
            confirmMenus[i].isVisible = false;
        }
    }


    public void ToggleShow(int index)
    {

        if (!confirmMenus[index].isVisible)
        {
            confirmMenus[index].Menu.SetActive(true); // Show the menu
            confirmMenus[index].isVisible = true;
            Debug.Log("TEST 1");
        }
        else
        {
            confirmMenus[index].Menu.SetActive(false); // Hide the menu
            confirmMenus[index].isVisible = false;
            Debug.Log("TEST 2");
        }
    }
}
