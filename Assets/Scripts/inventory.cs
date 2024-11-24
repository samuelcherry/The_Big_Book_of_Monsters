using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public string drop;

    [Serializable]
    public class InventoryItem
    {
        public string name;
        public int quantity;
        public Sprite itemIcon;

        public InventoryItem(string itemName, int itemQuantity, Sprite itemIcon)
        {
            name = itemName;
            quantity = itemQuantity;
            this.itemIcon = itemIcon;
        }
    }

    [Serializable]
    public class ItemDefinition
    {
        public string name;
        public int quantity;
        public Sprite icon;
    }

    public GameObject inventoryGrid;
    public GameObject inventoryItemPrefab;

    public List<InventoryItem> sampleList; // Player's inventory
    public List<ItemDefinition> masterItemList; // Predetermined list of items

    void Start()
    {
        // Initialize the inventory list
        sampleList = new List<InventoryItem>();

        // Initialize the master item list with predefined items
        masterItemList = new List<ItemDefinition>
        {
            new() { name = "Potion_01",quantity=0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon1") },
            new() { name = "Potion_02",quantity=0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon2") },
            new() { name = "Potion_03",quantity=0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon3") }
        };
    }

    public void DropTable()
    {
        if (masterItemList.Count == 0)
        {
            Debug.LogWarning("Master item list is empty!");
            return;
        }

        // Randomly select an item from the master item list
        int randomIndex = UnityEngine.Random.Range(0, masterItemList.Count);
        ItemDefinition randomItem = masterItemList[randomIndex];

        // Add the selected item to the inventory
        AddItem(randomItem.name, 1, randomItem.icon);
    }


    private Dictionary<string, GameObject> itemSlotDictionary = new();
    public void AddItem(string itemName, int itemQuantity, Sprite itemIcon)
    {
        // Check if the item already exists in the player's inventory
        InventoryItem existingItem = sampleList.Find(item => item.name == itemName);
        if (existingItem != null)
        {
            existingItem.quantity += itemQuantity;

            if (itemSlotDictionary.ContainsKey(itemName))
            {
                var quantityText = itemSlotDictionary[itemName].transform.Find("Quantity").GetComponent<TMP_Text>();
                if (quantityText != null)
                {
                    quantityText.text = existingItem.quantity.ToString();
                }
            }
        }
        else
        {
            // Add a new item to the player's inventory
            InventoryItem newItem = new(itemName, itemQuantity, itemIcon);
            sampleList.Add(newItem);

            // Create a new UI slot for the item
            GameObject newSlot = Instantiate(inventoryItemPrefab, inventoryGrid.transform);

            // Update the UI slot with the item's details
            var iconImage = newSlot.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
            var quantityText = newSlot.transform.Find("Quantity").GetComponent<TMP_Text>();

            if (iconImage != null) iconImage.sprite = itemIcon;
            if (quantityText != null) quantityText.text = itemQuantity.ToString();

            itemSlotDictionary.Add(itemName, newSlot);

            // Optionally name the UI slot for easier debugging
            newSlot.name = itemName;
        }

        DebugListContents(sampleList);
    }

    // Method to log the contents of the inventory
    private void DebugListContents(List<InventoryItem> list)
    {
        foreach (var item in list)
        {
            Debug.Log($"Name: {item.name}, Quantity: {item.quantity}");
        }
    }
}
