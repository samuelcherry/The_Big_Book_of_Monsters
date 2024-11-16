using UnityEngine;

public class ConfirmManager : MonoBehaviour
{
    public SaveManager saveManager;
    public EnemyStats enemyStats;

    [System.Serializable]
    public struct ConfirmMenu
    {
        public GameObject Menu;
        public bool isVisible;
    }


    public ConfirmMenu[] confirmMenus = new ConfirmMenu[4];


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
        }
        else
        {
            confirmMenus[index].Menu.SetActive(false); // Hide the menu
            confirmMenus[index].isVisible = false;
        }
    }

}
