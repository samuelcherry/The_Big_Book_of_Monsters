using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ListToText : MonoBehaviour
{
    public GameObject itemPrefab;
    [SerializeField] public Transform inventoryGrid;
    public Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (itemPrefab == null)
        {
            itemPrefab = Resources.Load<GameObject>("itemPrefab");
            if (itemPrefab == null)
            {
                Debug.LogError("Default prefab not found in Resources folder!");
            }
        }
    }

    public void PopulateText(List<Inventory.InventoryItem> itemList) //PASS SAMPLE LIST AS A PARAMETER SO THAT YOU ONLY CALL THIS FUNTION ONCE SAMPLE LIST IS LOADED
    {
        if (itemPrefab == null)
        {
            itemPrefab = Resources.Load<GameObject>("itemPrefab");
        }

        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }
        Debug.Log(inventoryGrid.childCount);
        List<Inventory.InventoryItem> itemsToDisplay = itemList;

        // Generate a dynamic string from the list
        for (int i = 0; i < itemList.Count; i++)
        {
            int index = i;
            GameObject newItem = Instantiate(itemPrefab, inventoryGrid);
            UnityEngine.UI.Image slotIcon = newItem.GetComponentInChildren<UnityEngine.UI.Image>();
            TMP_Text textElement = newItem.GetComponentInChildren<TMP_Text>();
            UnityEngine.UI.Button button = newItem.GetComponentInChildren<UnityEngine.UI.Button>();
            if (textElement != null)
            {
                textElement.enabled = true;
                textElement.text = $"{itemsToDisplay[i].quantity}";
            }
            if (slotIcon != null)
            {
                Inventory.ItemDefinition itemPath = inventory.masterItemList.Find(item => item.name == itemList[index].name);

                slotIcon.sprite = itemPath.icon;
                slotIcon.enabled = true;

            }
            button?.onClick.AddListener(() => inventory.OnItemClicked(itemList[index]));
        }
    }
}

