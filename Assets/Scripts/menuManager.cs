using UnityEngine;


public class MenuManager : MonoBehaviour
{

    [System.Serializable]
    public struct Menu
    {
        public GameObject MenuGroup;
        public bool MenuOpen;
    }

    public Menu[] menu = new Menu[7];


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
    }
}