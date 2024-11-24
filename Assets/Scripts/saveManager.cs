using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public Prestige prestige;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
    public Bestiary bestiary;
    public BlacksmithToggleManager blacksmithToggleManager;
    public AlchemyTimers alchemyTimers;
    public StartScreenManager startScreenManager;
    public Popup popup;
    public ResetManager resetManager;
    //HELPER FUNCTIONS
    private void SaveInt(string key, int value) => PlayerPrefs.SetInt(key, value);
    private int LoadInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(key, defaultValue);
    private void SaveFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);
    private float LoadFloat(string key, float defaultValue = 0f) => PlayerPrefs.GetFloat(key, defaultValue);


    //SAVE FUNCTION

    public void Save()
    {
        PlayerSave();
        EnemySave();
        SlotUpgradeSave();
        PrestigeSave();
        SkillPointSave();
        AdventureSave();
        BookSave();
        AlchemySave();
        popup.FadeText();
        PlayerPrefs.Save();

    }

    public void Load()
    {
        PlayerLoad();
        EnemyLoad();
        SlotUpgradeLoad();
        PrestigeLoad();
        SkillPointLoad();
        AdventureLoad();
        BookLoad();
        AlchemyLoad();

        //TEXT UPDATES
        prestige.UpdatePostPrestigeText();
        playerStats.UpdateStatText();
        enemyStats.UpdateEnemyStatsText();
        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.UpdateSlotText(i);
        }

        //SLIDER UPDATE
        playerStats.xpBar.value = playerStats.currentXp / playerStats.maxXP;

        for (int r = 0; r < upgrades.roles.Length; r++)
        {

            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                var upgrade = upgrades.roles[r].upgrades[i];
                upgrade.skillPointSlider.value = upgrade.metalCount / upgrade.metalMax;
            }
        }
    }
    public void PlayerSave()
    {
        //PLAYER SAVE
        SaveInt("Level", playerStats.level);
        SaveFloat("Xp", playerStats.currentXp);
        SaveInt("AtkMetalCount", playerStats.atkMetalCount);
        SaveInt("DefMetalCount", playerStats.defMetalCount);
        SaveInt("HpMetalCount", playerStats.hpMetalCount);
    }
    public void PlayerLoad()
    {
        //PLAYER LOAD
        if (PlayerPrefs.GetInt("Level") <= 1)
        {
            playerStats.level = 1;
        }
        else
        {
            playerStats.level = PlayerPrefs.GetInt("Level");
        }
        playerStats.currentXp = LoadFloat("Xp", 0);
        playerStats.atkMetalCount = LoadInt("AtkMetalCount", 0);
        playerStats.defMetalCount = LoadInt("DefMetalCount", 0);
        playerStats.hpMetalCount = LoadInt("HpMetalCount", 0);
    }

    public void EnemySave()
    {
        //ENEMY SAVE
        SaveFloat("GoldAmt", enemyStats.GoldAmt);
    }

    public void EnemyLoad()
    {
        //ENEMY LOAD
        enemyStats.GoldAmt = LoadFloat("GoldAmt", 0);
    }

    public void SlotUpgradeSave()
    {
        //SLOT UPGRADES SAVE
        SaveInt("SlotOneLvl", slotUpgrades.slotStructs[0].slotLvl);
        SaveInt("SlotTwoLvl", slotUpgrades.slotStructs[1].slotLvl);
        SaveInt("SlotThreeLvl", slotUpgrades.slotStructs[2].slotLvl);
        SaveInt("AutoBuyerLvl", blacksmithToggleManager.AutoBuyerLvl);

    }

    public void SlotUpgradeLoad()
    {
        //SLOT UPGRADES LOAD
        slotUpgrades.slotStructs[0].slotLvl = LoadInt("SlotOneLvl", 0);
        slotUpgrades.slotStructs[1].slotLvl = LoadInt("SlotTwoLvl", 0);
        slotUpgrades.slotStructs[2].slotLvl = LoadInt("SlotThreeLvl", 0);
        blacksmithToggleManager.AutoBuyerLvl = LoadInt("AutoBuyerLvl", 0);
    }

    public void PrestigeSave()
    {
        //PRESTIGE SAVE
        SaveFloat("BaseXp", prestige.baseXP);
        SaveFloat("PrestigeMulti", prestige.prestigeMulti);
    }
    public void PrestigeLoad()
    {
        LoadFloat("PrestigeMulti", 1);
        prestige.baseXP = LoadFloat("BaseXp", 0);
    }
    public void SkillPointSave()
    {
        //SKILL POINT SAVE
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            SaveInt($"RoleUnlocked{r}", upgrades.roles[r].roleUnlocked);
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                SaveFloat($"UpgradeMetalCount_{r}_{i}", upgrades.roles[r].upgrades[i].metalCount);
                PlayerPrefs.SetInt($"unlocked_{r}_{i}", upgrades.roles[r].upgrades[i].unlocked ? 1 : 0);
                PlayerPrefs.SetInt($"purchased_{r}_{i}", upgrades.roles[r].upgrades[i].purchased ? 1 : 0);
                PlayerPrefs.SetInt($"blocked_{r}_{i}", upgrades.roles[r].upgrades[i].blocked ? 1 : 0);

            }
        }
        SaveInt("Role", playerStats.role);
        SaveInt("RoleChoosen", playerStats.roleChoosen);
    }

    public void SkillPointLoad()
    {
        //UPGRADES LOAD
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            upgrades.roles[r].roleUnlocked = LoadInt($"RoleUnlocked{r}", 0);
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                upgrades.roles[r].upgrades[i].metalCount = LoadFloat($"UpgradeMetalCount_{r}_{i}", 0);
                upgrades.roles[r].upgrades[i].unlocked = PlayerPrefs.GetInt($"unlocked_{r}_{i}", 0) == 1;
                upgrades.roles[r].upgrades[i].purchased = PlayerPrefs.GetInt($"purchased_{r}_{i}", 0) == 1;
                upgrades.roles[r].upgrades[i].blocked = PlayerPrefs.GetInt($"blocked_{r}_{i}", 0) == 1;
            }
        }
        playerStats.role = LoadInt("Role", 0);
        playerStats.roleChoosen = LoadInt("RoleChoosen", 0);
    }

    public void AdventureSave()
    {
        //ADVENTURE SAVE
        for (int i = 0; i < enemyStats.adventures.Length; i++)
        {
            SaveInt($"AdventureIsCompleted_{i}", enemyStats.adventures[i].isCompleted);
            SaveInt($"AdventurePrevCompleted_{i}", enemyStats.adventures[i].prevCompleted);
        }
        SaveInt("AdventureIndex", enemyStats.tempAdventureNumber);

    }

    public void AdventureLoad()
    {
        //ADVENTURE LOAD
        for (int i = 0; i < enemyStats.adventures.Length; i++)
        {
            enemyStats.adventures[i].isCompleted = LoadInt($"AdventureIsCompleted_{i}", 0);
            enemyStats.adventures[i].prevCompleted = LoadInt($"AdventurePrevCompleted_{i}", 0);
        }
        enemyStats.tempAdventureNumber = LoadInt("AdventureIndex", 0);
    }

    public void BookSave()
    {
        //BOOK SAVES
        for (int i = 0; i < bestiary.entry.Length; i++)
        {
            SaveInt($"IsDefeated_{i}", bestiary.entry[i].IsDefeated);
        }
    }

    public void BookLoad()
    {
        //BOOK LOAD
        for (int i = 0; i < bestiary.entry.Length; i++)
        {
            bestiary.entry[i].IsDefeated = LoadInt($"IsDefeated_{i}", 0);
        }
    }

    public void AlchemySave()
    {
        SaveInt("AlchAutoBuyerLvl", alchemyTimers.AlchAutoBuyerLvl);

        for (int i = 0; i < alchemyTimers.alchemyProgressBar.Length; i++)
        {
            var alchBar = alchemyTimers.alchemyProgressBar;
            SaveInt($"alchLvl{i}", alchBar[i].alchLvl);
            SaveFloat($"alchXP{i}", alchBar[i].alchXP);
            SaveInt($"alchRwd{i}", alchBar[i].rwd);
            SaveInt($"alchLimit{i}", alchBar[i].limit);
        }

        for (int i = 0; i < alchemyTimers.potion.Length; i++)
        {
            SaveInt($"potionAmt{i}", alchemyTimers.potion[i].PotionAmt);
        }
        Debug.Log($"Save RWD: " + alchemyTimers.alchemyProgressBar[0].rwd);

    }

    public void AlchemyLoad()
    {

        alchemyTimers.AlchAutoBuyerLvl = LoadInt("AlchAutoBuyerLvl", 0);
        alchemyTimers.AlchAutoBuyerAmt = alchemyTimers.AlchAutoBuyerLvl;
        for (int i = 0; i < alchemyTimers.alchemyProgressBar.Length; i++)
        {
            var alchBar = alchemyTimers.alchemyProgressBar;

            alchBar[i].alchLvl = LoadInt($"alchLvl{i}", 1);
            alchBar[i].alchXP = LoadFloat($"alchXP{i}", 0f);
            alchBar[i].rwd = LoadInt($"alchRwd{i}", 0);
            alchBar[i].limit = LoadInt($"alchLimit{i}", 10);

            alchBar[i].totalTime = alchBar[i].baseTime / alchBar[i].alchLvl;

        }

        for (int i = 0; i < alchemyTimers.potion.Length; i++)
        {
            alchemyTimers.potion[i].PotionAmt = LoadInt($"potionAmt{i}", 0);
        }

        alchemyTimers.UpdateAlchText();
    }
}
