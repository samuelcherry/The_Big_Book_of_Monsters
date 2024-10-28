using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public GameObject MenuOne;
    public GameObject MenuTwo;
    public GameObject MenuThree;
    public GameObject MenuFour;
    public bool MenuOneOpen = true;
    public bool MenuTwoOpen = false;
    public bool MenuThreeOpen = false;
    public bool MenuFourOpen = false;
    void Start()
    {
        MenuOne.transform.position = new Vector3(-35, 4, 90);
    }

    public void MenuOneMove()

    {
        if (MenuOneOpen == false)
        {
            MenuOne.transform.position = new Vector3(-35, 4, 90);
            MenuTwo.transform.position = new Vector3(-74, 4, 90);
            MenuThree.transform.position = new Vector3(-74, 4, 90);
            MenuFour.transform.position = new Vector3(-74, 4, 90);
            MenuOneOpen = true;
            MenuTwoOpen = false;
            MenuThreeOpen = false;
            MenuFourOpen = false;
        }

    }
    public void MenuTwoMove()
    {
        if (MenuTwoOpen == false)
        {
            MenuOne.transform.position = new Vector3(-74, 4, 90);
            MenuTwo.transform.position = new Vector3(-35, 4, 90);
            MenuThree.transform.position = new Vector3(-74, 4, 90);
            MenuFour.transform.position = new Vector3(-74, 4, 90);
            MenuOneOpen = false;
            MenuTwoOpen = true;
            MenuThreeOpen = false;
            MenuFourOpen = false;
        }
    }
    public void MenuThreeMove()
    {
        if (MenuThreeOpen == false)
        {
            Debug.Log(MenuThree.transform.position);
            MenuOne.transform.position = new Vector3(-74, 4, 90);
            MenuTwo.transform.position = new Vector3(-74, 4, 90);
            MenuThree.transform.position = new Vector3(-35, 4, 90);
            MenuFour.transform.position = new Vector3(-74, 4, 90);
            MenuOneOpen = false;
            MenuTwoOpen = false;
            MenuThreeOpen = true;
            MenuFourOpen = false;
        }
    }
    public void MenuFourMove()
    {
        if (MenuFourOpen == false)
        {
            Debug.Log(MenuThree.transform.position);
            MenuOne.transform.position = new Vector3(-74, 4, 90);
            MenuTwo.transform.position = new Vector3(-74, 4, 90);
            MenuThree.transform.position = new Vector3(-74, 4, 90);
            MenuFour.transform.position = new Vector3(-35, 4, 90);
            MenuOneOpen = false;
            MenuTwoOpen = false;
            MenuThreeOpen = false;
            MenuFourOpen = true;
        }
    }
}