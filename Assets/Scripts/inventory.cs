using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    public PlayerStats playerStats;
    public BuffManager buffManager;
    public ProgressBarTimer progressBarTimer;
    public AlchemyTimers alchemyTimers;
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
        public int id;
        public string name;
        public int quantity;
        public Sprite icon;
    }

    [Serializable]
    public class DropRateItem
    {
        public ItemDefinition item;
        public float dropRate;
    }

    public List<InventoryItem> sampleList; // Player's inventory
    public List<ItemDefinition> masterItemList; // Predetermined list of items
    private Dictionary<int, List<DropRateItem>> enemyDropTables;
    public GameObject inventoryGrid;
    public GameObject inventoryItemPrefab;

    private Dictionary<string, GameObject> itemSlotDictionary = new();

    public Sprite attackBuffIcon, defBuffIcon, spdBuffIcon;


    void Start()
    {

        // Initialize the inventory list
        sampleList = new List<InventoryItem>();

        // Initialize the master item list with predefined items
        masterItemList = new List<ItemDefinition>
        {
            new() { id=0, name = "Water Weed", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon24") },
            new() { id=1,name = "Fire Fruit", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon19") },
            new() { id=2,name = "Spark Flowers", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy2/PNG/Transperent/Icon3") },
            new() { id=3,name = "Snake Charm Flower", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy2/PNG/Transperent/Icon14") },
            new() { id=4,name = "Ember Buds", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy2/PNG/Transperent/Icon4") },
            new() { id=5,name = "Frost Berries", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon22") },
            new() { id=6,name = "Volt Apples", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon48") },
            new() { id=7,name = "Animum Powder", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon35") },
            //POTIONS
            new() { id=8,name = "Hp Potion 1", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon3") },
            new() { id=9,name = "Atk Potion 1", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon1") },
            new() { id=10,name = "Def Potion 1", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon5") },
            new() { id=11,name = "Spd Potion 1", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon2") },
            new() { id=12,name = "Hp Potion 2", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon7")},
            new() { id=13,name = "Atk Potion 2", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon4") },
            new() { id=14,name = "Def Potion 2", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon8") },
            new() { id=15,name = "Spd Potion 2", quantity = 0, icon = Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon16") },

       };

        enemyDropTables = new Dictionary<int, List<DropRateItem>>
        {
            {
                0, new List<DropRateItem> // Enemy ID 0
                {
                    new() { item = masterItemList[0], dropRate = 10f },
                }
            },
            {
                1, new List<DropRateItem> // Enemy ID 1
                {
                    new() { item = masterItemList[1], dropRate = 10f },
                }
            },
                        {
                2, new List<DropRateItem> // Enemy ID 2
                {
                    new() { item = masterItemList[2], dropRate = 10f },
                }
            },
            {
                3, new List<DropRateItem> // Enemy ID 3
                {
                    new() { item = masterItemList[3], dropRate = 10f },
                }
            },
                        {
                4, new List<DropRateItem> // Enemy ID 4
                {
                    new() { item = masterItemList[0], dropRate = 10f },
                    new() { item = masterItemList[1], dropRate = 10f },
                    new() { item = masterItemList[2], dropRate = 10f },
                    new() { item = masterItemList[3], dropRate = 10f }
                }
            },
            {
                5, new List<DropRateItem> // Enemy ID 5
                {
                    new() { item = masterItemList[0], dropRate = 20f },
                }
            },
                        {
                6, new List<DropRateItem> // Enemy ID 6
                {
                    new() { item = masterItemList[1], dropRate = 20f },
                }
            },
            {
                7, new List<DropRateItem> // Enemy ID 7
                {
                    new() { item = masterItemList[2], dropRate = 20f },
                }
            },
                        {
                8, new List<DropRateItem> // Enemy ID 8
                {
                    new() { item = masterItemList[3], dropRate = 20f }
                }
            },
            {
                9, new List<DropRateItem> // Enemy ID 9
                {
                    new() { item = masterItemList[0], dropRate = 20f },
                    new() { item = masterItemList[1], dropRate = 20f },
                    new() { item = masterItemList[2], dropRate = 20f },
                    new() { item = masterItemList[3], dropRate = 20f },
                    new() { item = masterItemList[8], dropRate = 10f },
                }
            },
                        {
                10, new List<DropRateItem> // Enemy ID 10
                {
                    new() { item = masterItemList[4], dropRate = 5f },
                    new() { item = masterItemList[8], dropRate = 5f },
                }
            },
            {
                11, new List<DropRateItem> // Enemy ID 11
                {
                    new() { item = masterItemList[5], dropRate = 5f },
                    new() { item = masterItemList[9], dropRate = 5f },
                }
            },
                        {
                12, new List<DropRateItem> // Enemy ID 12
                {
                    new() { item = masterItemList[6], dropRate = 5f },
                    new() { item = masterItemList[10], dropRate = 5f },
                }
            },
            {
                13, new List<DropRateItem> // Enemy ID 13
                {
                    new() { item = masterItemList[7], dropRate = 5f },
                    new() { item = masterItemList[11], dropRate = 5f },
                }
            },
                        {
                14, new List<DropRateItem> // Enemy ID 14
                {
                    new() { item = masterItemList[4], dropRate = 10f },
                    new() { item = masterItemList[5], dropRate = 10f },
                    new() { item = masterItemList[6], dropRate = 10f },
                    new() { item = masterItemList[7], dropRate = 10f },
                }
            },
            {
                15, new List<DropRateItem> // Enemy ID 15
                {
                    new() { item = masterItemList[4], dropRate = 20f },
                }
            },
                                    {
                16, new List<DropRateItem> // Enemy ID 16
                {
                    new() { item = masterItemList[5], dropRate = 20f },
                }
            },
            {
                17, new List<DropRateItem> // Enemy ID 17
                {
                    new() { item = masterItemList[6], dropRate = 20f },
                }
            },
                        {
                18, new List<DropRateItem> // Enemy ID 18
                {
                    new() { item = masterItemList[7], dropRate = 20f },
                }
            },
            {
                19, new List<DropRateItem> // Enemy ID 19
                {
                    new() { item = masterItemList[4], dropRate = 20f },
                    new() { item = masterItemList[5], dropRate = 20f },
                    new() { item = masterItemList[6], dropRate = 20f },
                    new() { item = masterItemList[7], dropRate = 20f },
                    new() { item = masterItemList[8], dropRate = 10f },
                    new() { item = masterItemList[9], dropRate = 10f },
                    new() { item = masterItemList[10], dropRate = 10f },
                    new() { item = masterItemList[11], dropRate = 10f }
                }
            },

        };
    }

    public bool HasItem(string itemName, int reqAmount)
    {
        InventoryItem existingItem = sampleList.Find(item => item.name == itemName);
        if (existingItem != null && existingItem.quantity >= reqAmount)
        {
            existingItem.quantity -= reqAmount;

            if (existingItem.quantity <= 0)
            {
                sampleList.Remove(existingItem);
                RemoveItemUI(itemName);
            }
            else
            {
                UpdateIteamQuantityUI(itemName, existingItem.quantity);
            }
            return true;
        }
        return false;
    }

    private void RemoveItemUI(string itemName)
    {
        if (itemSlotDictionary.ContainsKey(itemName))
        {
            GameObject slotToRemove = itemSlotDictionary[itemName];
            itemSlotDictionary.Remove(itemName);
            Destroy(slotToRemove);

            Debug.Log($"Removed UI slot for {itemName}");
        }
    }

    public void UpdateIteamQuantityUI(string itemName, int newQuantity)
    {
        if (itemSlotDictionary.ContainsKey(itemName))
        {
            var quantityText = itemSlotDictionary[itemName].transform.Find("Quantity").GetComponent<TMP_Text>();
            if (quantityText != null)
            {
                quantityText.text = newQuantity.ToString();
            }
        }
    }

    public void DropTable(int enemyId)
    {
        if (!enemyDropTables.ContainsKey(enemyId))
        {
            Debug.LogWarning($"No drop table found for enemy with ID: {enemyId}");
            return;
        }

        List<DropRateItem> dropList = enemyDropTables[enemyId];

        foreach (var dropItem in dropList)
        {
            if (UnityEngine.Random.Range(0f, 100f) < dropItem.dropRate)
            {
                AddItem(dropItem.item.name, 1, dropItem.item.icon);
            }
        }
    }



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
            var button = newSlot.GetComponent<UnityEngine.UI.Button>();


            if (iconImage != null) iconImage.sprite = itemIcon;
            if (quantityText != null) quantityText.text = itemQuantity.ToString();

            itemSlotDictionary.Add(itemName, newSlot);

            if (button != null)
            {
                button.onClick.AddListener(() => OnItemClicked(newItem));
            }

            // Optionally name the UI slot for easier debugging
            newSlot.name = itemName;
        }
    }

    public void OnItemClicked(InventoryItem item)
    {
        Debug.Log($"Clicked on item: {item.name}");

        // Check if the buff already exists before proceeding
        if (!buffManager.activeBuffnames.Contains(item.name))
        {
            switch (item.name)
            {
                case "Hp Potion 1":
                    HpPotion(50);
                    break;

                case "Atk Potion 1":
                    AtkPotion(item.name, 0.5f);
                    break;

                case "Def Potion 1":
                    DefPotion(item.name, 0.5f);
                    break;

                case "Spd Potion 1":
                    SpdPotion(item.name, 2f);
                    break;

                case "Hp Potion 2":
                    HpPotion(150);
                    break;

                case "Atk Potion 2":
                    AtkPotion(item.name, 1f);
                    break;

                case "Def Potion 2":
                    DefPotion(item.name, 1f);
                    break;

                case "Spd Potion 2":
                    SpdPotion(item.name, 3f);
                    break;
            }

            // Reduce the quantity of the item
            item.quantity--;
        }
        else
        {
            Debug.Log("Buff is already active, cannot apply again.");
        }

        if (item.quantity <= 0)
        {
            sampleList.Remove(item);
            RemoveItemUI(item.name);
            Debug.Log($"{item.name} was used up and removed from inventory.");
        }
        else
        {
            // Update the UI to reflect the new quantity
            UpdateIteamQuantityUI(item.name, item.quantity);
        }

    }

    public void HpPotion(int amt)
    {

        playerStats.currentHp += amt;
        if (playerStats.currentHp > playerStats.maxHp)
        {
            playerStats.currentHp = playerStats.maxHp;
        }
        playerStats.UpdateHpText();

    }

    public void AtkPotion(string buffName, float amt)
    {

        // Increase attack value
        buffManager.activeBuffnames.Add(buffName);
        playerStats.atkBuff += playerStats.atk * amt;
        playerStats.UpdateStats();
        playerStats.UpdateAtkText();

        buffManager.AddBuff("Atk Buff", attackBuffIcon, 10f);

        // Start coroutine to remove the effect after 10 seconds
        StartCoroutine(RemoveAtkPotionEffect(buffName, 10f));
    }

    public void DefPotion(string buffName, float amt)
    {
        // Increase attack value
        playerStats.defBuff += playerStats.def * amt;
        playerStats.UpdateStats();
        playerStats.UpdateAtkText();

        buffManager.AddBuff("Def Buff", defBuffIcon, 10f);

        // Start coroutine to remove the effect after 10 seconds
        StartCoroutine(RemoveDefPotionEffect(buffName, 10f));
    }


    public void SpdPotion(string buffName, float amt)
    {
        // Increase attack value
        progressBarTimer.playerAtkTime /= amt;
        playerStats.UpdateStats();
        progressBarTimer.UpdateSpdText();

        buffManager.AddBuff("Spd Buff", spdBuffIcon, 10f);

        // Start coroutine to remove the effect after 10 seconds
        StartCoroutine(RemoveSpdPotionEffect(buffName, 10f, amt));
    }


    private IEnumerator RemoveAtkPotionEffect(string buffName, float duration)
    {
        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore the original attack value
        playerStats.atkBuff = 0;
        buffManager.activeBuffnames.Remove(buffName);
        playerStats.UpdateAtkText();

        Debug.Log("Atk potion effect has worn off.");
    }

    private IEnumerator RemoveDefPotionEffect(string buffName, float duration)
    {
        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore the original attack value
        playerStats.defBuff = 0;
        playerStats.UpdateDefText();

        Debug.Log("Def potion effect has worn off.");
    }

    private IEnumerator RemoveSpdPotionEffect(string buffName, float duration, float amt)
    {
        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore the original attack value
        progressBarTimer.playerAtkTime *= amt;
        progressBarTimer.UpdateSpdText();

        Debug.Log("Def potion effect has worn off.");
    }
}
