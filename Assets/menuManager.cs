using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public GameObject MenuOne;
    public GameObject MenuTwo;
    public bool MenuOneOpen = true;
    public bool MenuTwoOpen = false;

    public void MenuOneMove()

    {
        if (MenuOneOpen == false)
        {
            MenuOne.transform.position = new Vector3(-35, 4, 90);
            MenuTwo.transform.position = new Vector3(-74, 4, 90);
            MenuOneOpen = true;
            MenuTwoOpen = false;
            Debug.Log(MenuOne.transform.position);
        }

    }
    public void MenuTwoMove()
    {
        if (MenuTwoOpen == false)
        {
            MenuOne.transform.position = new Vector3(-74, 4, 90);
            MenuTwo.transform.position = new Vector3(-35, 4, 90);
            MenuOneOpen = false;
            MenuTwoOpen = true;
            Debug.Log(MenuOne.transform.position);
        }
    }
}