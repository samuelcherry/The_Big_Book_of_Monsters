using UnityEngine;
using System.IO;
using System.Collections.Generic;



public class SaveManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public SlotUpgrades slotUpgrades;
    public Prestige prestige;
    public AlchemyTimers alchemy;
    public SlotUpgrades skillPointSystem;
    public Upgrades upgrades;
    public Bestiary bestiary;
    public BlacksmithToggleManager blacksmithToggleManager;
    private string saveFilePath;
    // Static instance of the SaveManager

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveGame(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData, true); // true for pretty print
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Game saved to {saveFilePath}");
    }

    public SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded successfully.");
            return saveData;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return new SaveData(); // Return default values
        }
    }

    public void Save()
    {
        SaveData saveData = new SaveData
        {
            playerData = new PlayerData
            {
                level = playerStats.level,
                currentXp = playerStats.currentXp,
                atkMetalCount = playerStats.atkMetalCount,
                defMetalCount = playerStats.defMetalCount,
                hpMetalCount = playerStats.hpMetalCount,
                role = playerStats.role,
                roleChoosen = playerStats.roleChoosen
            },
            enemyData = new EnemyData
            {
                goldAmt = enemyStats.GoldAmt,
                adventures = new List<AdventureData>()
            },
            slotUpgradeData = new SlotUpgradeData
            {
                autoBuyerLevel = blacksmithToggleManager.AutoBuyerLvl
            },
            prestigeData = new PrestigeData
            {
                baseXP = prestige.baseXP,
                prestigeMulti = prestige.prestigeMulti
            },
            skillPointData = new SkillPointData
            {
                roles = new List<RoleData>()
            },
            bestiaryData = new BestiaryData(),
            alchemyData = new AlchemyData
            {
                alchAutoBuyerLevel = (int)alchemy.AlchAutoBuyerLvl,
                alchemyBars = new List<AlchemyBarData>(),
                potionAmounts = new List<int>()
            },
            adventureData = new AdventureData
            {
                tempAdventureNumber = enemyStats.tempAdventureNumber
            }
        };

        foreach (var entry in bestiary.entry)
        {
            saveData.bestiaryData.defeatedEntries.Add(entry.IsDefeated);
        }

        foreach (var slot in slotUpgrades.slotStructs)
        {
            saveData.slotUpgradeData.slotLevels.Add(slot.slotLvl);
        }


        // Add adventures
        foreach (var adventure in enemyStats.adventures)
        {
            saveData.enemyData.adventures.Add(new AdventureData
            {
                isCompleted = adventure.isCompleted,
                prevCompleted = adventure.prevCompleted
            });
        }

        // Add roles and upgrades
        foreach (var role in upgrades.roles)
        {
            RoleData roleData = new RoleData
            {
                roleUnlocked = role.roleUnlocked,
                upgrades = new List<UpgradeData>()
            };

            foreach (var upgrade in role.upgrades)
            {
                roleData.upgrades.Add(new UpgradeData
                {
                    metalCount = upgrade.metalCount,
                    unlocked = upgrade.unlocked,
                    purchased = upgrade.purchased,
                    blocked = upgrade.blocked
                });
            }

            saveData.skillPointData.roles.Add(roleData);
        }

        // Add alchemy bars
        foreach (var bar in alchemy.alchemyProgressBar)
        {
            saveData.alchemyData.alchemyBars.Add(new AlchemyBarData
            {
                alchLvl = bar.alchLvl,
                alchXP = bar.alchXP,
                alchMaxXp = bar.alchMaxXp,
                rwd = bar.rwd,
                limit = bar.limit,
                totalTime = bar.totalTime,
                baseTime = bar.baseTime
            });
        }

        SaveGame(saveData);
    }


    public void Load()
    {
        SaveData saveData = LoadGame();

        if (saveData == null)
        {
            Debug.LogWarning("No save data found! Starting a new game.");
            return;
        }

        // Load Player Data
        playerStats.level = saveData.playerData.level;
        playerStats.currentXp = saveData.playerData.currentXp;
        playerStats.atkMetalCount = saveData.playerData.atkMetalCount;
        playerStats.defMetalCount = saveData.playerData.defMetalCount;
        playerStats.hpMetalCount = saveData.playerData.hpMetalCount;
        playerStats.role = saveData.playerData.role;
        playerStats.roleChoosen = saveData.playerData.roleChoosen;

        // Load Enemy Data
        enemyStats.GoldAmt = saveData.enemyData.goldAmt;
        for (int i = 0; i < saveData.enemyData.adventures.Count; i++)
        {
            enemyStats.adventures[i].isCompleted = saveData.enemyData.adventures[i].isCompleted;
            enemyStats.adventures[i].prevCompleted = saveData.enemyData.adventures[i].prevCompleted;
        }

        // Load Slot Upgrades
        // Load Slot Levels from saved data
        if (saveData.slotUpgradeData.slotLevels.Count > 0)
        {
            for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
            {
                if (i < saveData.slotUpgradeData.slotLevels.Count)
                {
                    slotUpgrades.slotStructs[i].slotLvl = saveData.slotUpgradeData.slotLevels[i];
                    slotUpgrades.UpdateSlotText(i);
                }
            }
            blacksmithToggleManager.AutoBuyerLvl = saveData.slotUpgradeData.autoBuyerLevel;
        }
        // Load Prestige Data
        prestige.baseXP = saveData.prestigeData.baseXP;
        prestige.prestigeMulti = saveData.prestigeData.prestigeMulti;

        // Load Skill Points
        for (int i = 0; i < saveData.skillPointData.roles.Count; i++)
        {
            upgrades.roles[i].roleUnlocked = saveData.skillPointData.roles[i].roleUnlocked;

            for (int j = 0; j < saveData.skillPointData.roles[i].upgrades.Count; j++)
            {
                upgrades.roles[i].upgrades[j].metalCount = saveData.skillPointData.roles[i].upgrades[j].metalCount;
                upgrades.roles[i].upgrades[j].unlocked = saveData.skillPointData.roles[i].upgrades[j].unlocked;
                upgrades.roles[i].upgrades[j].purchased = saveData.skillPointData.roles[i].upgrades[j].purchased;
                upgrades.roles[i].upgrades[j].blocked = saveData.skillPointData.roles[i].upgrades[j].blocked;
            }
        }

        // Load Bestiary
        if (saveData.bestiaryData.defeatedEntries.Count > 0)
        {
            for (int i = 0; i < bestiary.entry.Length; i++)
            {
                if (i < saveData.bestiaryData.defeatedEntries.Count)
                {
                    bestiary.entry[i].IsDefeated = saveData.bestiaryData.defeatedEntries[i];
                }
            }
        }


        // Load Alchemy System
        alchemy.AlchAutoBuyerLvl = saveData.alchemyData.alchAutoBuyerLevel;
        alchemy.AlchAutoBuyerAmt = alchemy.AlchAutoBuyerLvl;
        for (int i = 0; i < saveData.alchemyData.potionAmounts.Count; i++)
        {
            alchemy.potion[i].PotionAmt = saveData.alchemyData.potionAmounts[i];
        }


        for (int i = 0; i < saveData.alchemyData.alchemyBars.Count; i++)
        {
            alchemy.alchemyProgressBar[i].alchLvl = saveData.alchemyData.alchemyBars[i].alchLvl;
            alchemy.alchemyProgressBar[i].alchXP = saveData.alchemyData.alchemyBars[i].alchXP;
            alchemy.alchemyProgressBar[i].alchMaxXp = saveData.alchemyData.alchemyBars[i].alchMaxXp;
            alchemy.alchemyProgressBar[i].rwd = saveData.alchemyData.alchemyBars[i].rwd;
            alchemy.alchemyProgressBar[i].limit = saveData.alchemyData.alchemyBars[i].limit;
            alchemy.alchemyProgressBar[i].totalTime = saveData.alchemyData.alchemyBars[i].totalTime;
            alchemy.alchemyProgressBar[i].baseTime = saveData.alchemyData.alchemyBars[i].baseTime;

            alchemy.alchemyProgressBar[i].timeLeft = alchemy.alchemyProgressBar[i].totalTime;
            alchemy.UpdateTimerText(i);
        }

        //Load Adventure Data
        enemyStats.tempAdventureNumber = saveData.adventureData.tempAdventureNumber;


        playerStats.UpdateStatText();
        alchemy.UpdateAlchText();
        prestige.UpdatePrestigeText();
        prestige.UpdatePostPrestigeText();
        Debug.Log("Game loaded successfully.");
    }
}
