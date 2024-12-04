using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerStats playerStats;
    public BuffManager buffManager;
    public ListToText listToText;
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
        public string itemText;
        public Sprite icon;

        public ItemDefinition(int id, string name, int quantity, string itemText, Sprite icon)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.itemText = itemText;
            this.icon = icon ?? Resources.Load<Sprite>("DefaultIcon"); // Assign a default icon if null
        }

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
    private Transform inventoryGrid;
    private Transform inventoryItemPrefab;
    public Sprite attackBuffIcon, defBuffIcon, spdBuffIcon;


    [Serializable]
    private class InventoryListWrapper
    {
        public List<InventoryItem> inventoryItems;
        public InventoryListWrapper(List<InventoryItem> items)
        {
            inventoryItems = items;
        }

    }

    void SaveToPrefs<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    void SaveInventory()
    {
        SaveToPrefs("Inventory", new InventoryListWrapper(sampleList));
        Debug.Log("Inventory Saved!");
        listToText.PopulateText(sampleList);
    }

    public void LoadInventory()
    {
        // ClearInventory();

        if (PlayerPrefs.HasKey("Inventory"))
        {
            string json = PlayerPrefs.GetString("Inventory");

            InventoryListWrapper wrapper = JsonUtility.FromJson<InventoryListWrapper>(json);
            sampleList = wrapper.inventoryItems;

            Debug.Log("Inventory loaded!");
        }
        else
        {
            Debug.LogWarning("No inventory data found.");
        }

        listToText.PopulateText(sampleList);

    }

    void Start()
    {
        // Initialize the inventory list
        sampleList = new List<InventoryItem>();
        // Initialize the master item list with predefined items
        masterItemList = new List<ItemDefinition>
        {
        new(0, "Snake Flower", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy2/PNG/Transperent/Icon14")),
        new(1, "Fire Fruit", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon19")),
        new(2, "Water Weed", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon24")),
        new(3, "Spark Flowers", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy2/PNG/Transperent/Icon3")),
        new(4, "Ember Buds", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy2/PNG/Transperent/Icon4")),
        new(5, "Frost Berries", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon22")),
        new(6, "Volt Apples", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon48")),
        new(7, "Animum Powder", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Alchemy1/PNG/Transperent/Icon35")),
        // POTIONS
        new(8, "Hp Potion 1", 0, "HP +50", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon3")),
        new(9, "Atk Potion 1", 0, "ATK +50%", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon1")),
        new(10, "Def Potion 1", 0, "DEF +50%", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon5")),
        new(11, "Spd Potion 1", 0, "SPD +100%", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon2")),
        new(12, "Hp Potion 2", 0, "HP +150", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon7")),
        new(13, "Atk Potion 2", 0, "ATK +100%", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon4")),
        new(14, "Def Potion 2", 0, "DEF +100%", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon8")),
        new(15, "Spd Potion 2", 0, "SPD +150%", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions/PNG/Transperent/Icon16")),
        new(16, "Generic Potion", 0, "Used in Alchemy", Resources.Load<Sprite>("RPG Icons Pixel Art/Potions 2/PNG/Transperent/Icon46")),
        // FOOD
        new(17, "Bread", 0, "HP +10", Resources.Load<Sprite>("RPG Icons Pixel Art/Food_icons/PNG/Transperent/Icon9")),
        new(18, "Drumstick", 0, "HP +20", Resources.Load<Sprite>("RPG Icons Pixel Art/Food_icons/PNG/Transperent/Icon3")),
        new(19, "Full Plate", 0, "HP +30", Resources.Load<Sprite>("RPG Icons Pixel Art/Food_icons/PNG/Transperent/Icon31")),
        new(20, "Bowl of Soup", 0, "HP +40", Resources.Load<Sprite>("RPG Icons Pixel Art/Food_icons/PNG/Transperent/Icon43")),
    };

        enemyDropTables = new Dictionary<int, List<DropRateItem>>
        {
            {
                0, new List<DropRateItem> // Enemy ID 0
                {
                    new() { item = masterItemList[17], dropRate = 30f },
                    new() { item = masterItemList[18], dropRate = 10f },

                }
            },
            {
                1, new List<DropRateItem> // Enemy ID 1
                {
                    new() { item = masterItemList[17], dropRate = 35f },
                    new() { item = masterItemList[18], dropRate = 15f },
                }
            },
                        {
                2, new List<DropRateItem> // Enemy ID 2
                {
                    new() { item = masterItemList[17], dropRate = 40f },
                    new() { item = masterItemList[18], dropRate = 20f },
                    new() { item = masterItemList[19], dropRate = 10f },
                    new() { item = masterItemList[1], dropRate = 15f },

                }
            },
            {
                3, new List<DropRateItem> // Enemy ID 3
                {
                    new() { item = masterItemList[18], dropRate = 40f },
                    new() { item = masterItemList[19], dropRate = 20f },
                    new() { item = masterItemList[20], dropRate = 10f },
                    new() { item = masterItemList[1], dropRate = 10f },
                    new() { item = masterItemList[0], dropRate = 10f },
                }
            },
                        {
                4, new List<DropRateItem> // Enemy ID 4
                {
                    new() { item = masterItemList[17], dropRate = 45f },
                    new() { item = masterItemList[18], dropRate = 40f },
                    new() { item = masterItemList[19], dropRate = 25f },
                    new() { item = masterItemList[20], dropRate = 20f },
                    new() { item = masterItemList[0], dropRate = 15f },
                    new() { item = masterItemList[1], dropRate = 10f },
                    new() { item = masterItemList[2], dropRate = 10f },
                }
            },
            {
                5, new List<DropRateItem> // Enemy ID 5
                {
                    new() { item = masterItemList[0], dropRate = 40f },
                }
            },
                        {
                6, new List<DropRateItem> // Enemy ID 6
                {
                    new() { item = masterItemList[1], dropRate = 40f },
                }
            },
            {
                7, new List<DropRateItem> // Enemy ID 7
                {
                    new() { item = masterItemList[2], dropRate = 40f },
                }
            },
                        {
                8, new List<DropRateItem> // Enemy ID 8
                {
                    new() { item = masterItemList[3], dropRate = 40f }
                }
            },
            {
                9, new List<DropRateItem> // Enemy ID 9
                {
                    new() { item = masterItemList[0], dropRate = 50f },
                    new() { item = masterItemList[1], dropRate = 50f },
                    new() { item = masterItemList[2], dropRate = 50f },
                    new() { item = masterItemList[3], dropRate = 50f },
                    new() { item = masterItemList[8], dropRate = 20f },
                }
            },
                        {
                10, new List<DropRateItem> // Enemy ID 10
                {
                    new() { item = masterItemList[4], dropRate = 50f },
                    new() { item = masterItemList[8], dropRate = 25f },
                }
            },
            {
                11, new List<DropRateItem> // Enemy ID 11
                {
                    new() { item = masterItemList[5], dropRate = 50f },
                    new() { item = masterItemList[9], dropRate = 25f },
                }
            },
                        {
                12, new List<DropRateItem> // Enemy ID 12
                {
                    new() { item = masterItemList[6], dropRate = 50f },
                    new() { item = masterItemList[10], dropRate = 25f },
                }
            },
            {
                13, new List<DropRateItem> // Enemy ID 13
                {
                    new() { item = masterItemList[7], dropRate = 50f },
                    new() { item = masterItemList[11], dropRate = 25f },
                }
            },
                        {
                14, new List<DropRateItem> // Enemy ID 14
                {
                    new() { item = masterItemList[4], dropRate = 60f },
                    new() { item = masterItemList[5], dropRate = 60f },
                    new() { item = masterItemList[6], dropRate = 60f },
                    new() { item = masterItemList[7], dropRate = 60f },
                }
            },
            {
                15, new List<DropRateItem> // Enemy ID 15
                {
                    new() { item = masterItemList[4], dropRate = 70f },
                }
            },
                                    {
                16, new List<DropRateItem> // Enemy ID 16
                {
                    new() { item = masterItemList[5], dropRate = 70f },
                }
            },
            {
                17, new List<DropRateItem> // Enemy ID 17
                {
                    new() { item = masterItemList[6], dropRate = 70f },
                }
            },
                        {
                18, new List<DropRateItem> // Enemy ID 18
                {
                    new() { item = masterItemList[7], dropRate = 70f },
                }
            },
            {
                19, new List<DropRateItem> // Enemy ID 19
                {
                    new() { item = masterItemList[4], dropRate = 70f },
                    new() { item = masterItemList[5], dropRate = 70f },
                    new() { item = masterItemList[6], dropRate = 70f },
                    new() { item = masterItemList[7], dropRate = 70f },
                    new() { item = masterItemList[8], dropRate = 40f },
                    new() { item = masterItemList[9], dropRate = 40f },
                    new() { item = masterItemList[10], dropRate = 40f },
                    new() { item = masterItemList[11], dropRate = 40f }
                }
            },

        };
    }

    public bool HasItem(string itemName, int reqAmount)
    {
        InventoryItem existingItem = sampleList.Find(item => item.name == itemName);

        if (existingItem == null || existingItem.quantity < reqAmount)
        {
            Debug.LogWarning($"Not enough {itemName} in inventory. Required: {reqAmount}, Available: {existingItem?.quantity ?? 0}");
            return false;
        }

        if (existingItem.quantity <= 0)
        {
            Debug.Log("REMOVE ITEM");
        }
        return true;
    }

    public void ClearInventory()
    {
        sampleList.Clear();
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
        InventoryItem existingItem = sampleList.Find(item => item.name == itemName);
        // Check if the item already exists in the inventory

        if (existingItem != null)
        {
            if (existingItem.quantity < 99)
            {
                existingItem.quantity += 1;
            }
            else
            {
                existingItem.quantity = 99;
            }
        }
        else
        {
            InventoryItem newItem = new(itemName, itemQuantity, itemIcon)
            {
                name = itemName,
                quantity = itemQuantity,
                itemIcon = itemIcon
            };
            sampleList.Add(newItem);
        }

        // Optional: Save inventory to persistent storage
        SaveInventory();
    }

    public List<InventoryItem> GetItemList()
    {
        return sampleList;
    }

    public void OnItemClicked(InventoryItem item)
    {
        // Check if the buff already exists before proceeding
        if (!buffManager.activeBuffnames.Contains(item.name))
        {
            switch (item.name)
            {
                case "Bread":
                    HpPotion(10);
                    item.quantity--;
                    break;

                case "Drumstick":
                    HpPotion(20);
                    item.quantity--;
                    break;

                case "Full Plate":
                    HpPotion(30);
                    item.quantity--;
                    break;

                case "Bowl of Soup":
                    HpPotion(40);
                    item.quantity--;
                    break;

                case "Hp Potion 1":
                    HpPotion(50);
                    item.quantity--;
                    break;

                case "Atk Potion 1":
                    AtkPotion(item.name, 0.5f);
                    item.quantity--;
                    break;

                case "Def Potion 1":
                    DefPotion(item.name, 0.5f);
                    item.quantity--;
                    break;

                case "Spd Potion 1":
                    SpdPotion(item.name, 2f);
                    item.quantity--;
                    break;

                case "Hp Potion 2":
                    HpPotion(150);
                    item.quantity--;
                    break;

                case "Atk Potion 2":
                    AtkPotion(item.name, 1f);
                    item.quantity--;
                    break;

                case "Def Potion 2":
                    DefPotion(item.name, 1f);
                    item.quantity--;
                    break;

                case "Spd Potion 2":
                    SpdPotion(item.name, 3f);
                    item.quantity--;
                    break;
            }

            // Reduce the quantity of the item


        }
        else
        {
            Debug.Log("Buff is already active, cannot apply again.");
        }

        if (item.quantity <= 0)
        {
            sampleList.Remove(item);
            Debug.Log($"{item.name} was used up and removed from inventory.");
        }
        SaveInventory();
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

        buffManager.AddBuff("Atk Buff", attackBuffIcon, 60f);

        // Start coroutine to remove the effect after 10 seconds
        StartCoroutine(RemoveAtkPotionEffect(buffName, 60f));
    }

    public void DefPotion(string buffName, float amt)
    {
        // Increase attack value
        buffManager.activeBuffnames.Add(buffName);
        playerStats.defBuff += playerStats.def * amt;
        playerStats.UpdateStats();

        buffManager.AddBuff("Def Buff", defBuffIcon, 60f);

        // Start coroutine to remove the effect after 10 seconds
        StartCoroutine(RemoveDefPotionEffect(buffName, 60f));
    }

    public void SpdPotion(string buffName, float amt)
    {
        // Increase attack value
        buffManager.activeBuffnames.Add(buffName);
        playerStats.spdBuff = amt;
        progressBarTimer.playerAtkTime /= amt;
        playerStats.UpdateStats();

        buffManager.AddBuff("Spd Buff", spdBuffIcon, 60f);

        // Start coroutine to remove the effect after 10 seconds
        StartCoroutine(RemoveSpdPotionEffect(buffName, 60f, amt));
    }

    private IEnumerator RemoveAtkPotionEffect(string buffName, float duration)
    {
        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore the original attack value
        playerStats.atkBuff = 0;
        buffManager.activeBuffnames.Remove(buffName);

        Debug.Log("Atk potion effect has worn off.");
    }

    private IEnumerator RemoveDefPotionEffect(string buffName, float duration)
    {
        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore the original attack value
        playerStats.defBuff = 0;

        Debug.Log("Def potion effect has worn off.");
    }

    private IEnumerator RemoveSpdPotionEffect(string buffName, float duration, float amt)
    {
        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Restore the original attack value
        playerStats.spdBuff = 1;
        progressBarTimer.playerAtkTime *= amt;
        playerStats.UpdateStats();

        Debug.Log("Def potion effect has worn off.");
    }
}
