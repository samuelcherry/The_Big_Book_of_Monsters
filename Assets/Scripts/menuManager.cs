using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public PlayerStats playerStats;

    [System.Serializable]
    public struct Menu
    {
        public GameObject MenuGroup;
        public bool MenuOpen;
    }

    [System.Serializable]
    public struct UpgradeMenu
    {
        public GameObject UpgradeMenus;
        public bool MenuOpen;
    }

    public Menu[] menu = new Menu[7];
    public UpgradeMenu[] upgradeMenus = new UpgradeMenu[4];


    void Start()
    {
        menu[0].MenuGroup.SetActive(true);
    }

    public void MenuMove(int index)
    {
        for (int i = 0; i < menu.Length; i++)
        {
            // Activate the selected menu and deactivate all others
            menu[i].MenuGroup.SetActive(i == index);
        }
        for (int i = 0; i < upgradeMenus.Length; i++)
        {
            upgradeMenus[i].UpgradeMenus.SetActive(false);
        }
    }

    public void UpgradeMenuOpen(int role)
    {
        for (int i = 0; i < upgradeMenus.Length; i++)
        {
            upgradeMenus[i].UpgradeMenus.SetActive(false);
        }

        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].MenuGroup.SetActive(false);
        }

        // Activate the menu based on the role index if it's within bounds
        if (role >= 0 && role < upgradeMenus.Length)
        {
            upgradeMenus[role].UpgradeMenus.SetActive(true);

        }
    }
    public void UpgradeMenuButton()
    {
        UpgradeMenuOpen(playerStats.role);
    }

}