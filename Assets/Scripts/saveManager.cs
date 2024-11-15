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

    public void Save()
    {
        PlayerPrefs.SetFloat("GoldAmt", enemyStats.GoldAmt);
        PlayerPrefs.SetInt("Level", playerStats.level);

        PlayerPrefs.SetInt("SlotOneLvl", slotUpgrades.slotStructs[0].slotLvl);
        PlayerPrefs.SetInt("SlotTwoLvl", slotUpgrades.slotStructs[1].slotLvl);
        PlayerPrefs.SetInt("SlotThreeLvl", slotUpgrades.slotStructs[2].slotLvl);

        PlayerPrefs.SetFloat("Xp", playerStats.currentXp);
        PlayerPrefs.SetFloat("BaseXp", prestige.baseXP);

        PlayerPrefs.SetFloat("PrestigeMulti", prestige.prestigeMulti);
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                PlayerPrefs.SetFloat($"UpgradeMetalCount_{i}", upgrades.roles[r].upgrades[i].metalCount);
            }
        }

        PlayerPrefs.SetInt("AtkMetalCount", playerStats.atkMetalCount);
        PlayerPrefs.SetInt("DefMetalCount", playerStats.defMetalCount);
        PlayerPrefs.SetInt("HpMetalCount", playerStats.hpMetalCount);

        PlayerPrefs.SetInt("AutoBuyerLvl", blacksmithToggleManager.AutoBuyerLvl);
        PlayerPrefs.SetInt("AutoBuyerAmt", blacksmithToggleManager.AutoBuyerAmt);

        for (int i = 0; i < enemyStats.adventures.Length; i++)
        {
            PlayerPrefs.SetInt($"AdventureIsCompleted_{i}", enemyStats.adventures[i].isCompleted);
        }
        //BOOK SAVES
        for (int i = 0; i < bestiary.entry.Length; i++)
        {
            PlayerPrefs.SetInt($"IsDefeated_{i}", bestiary.entry[i].IsDefeated);
        }


        AlchemySave();

        PlayerPrefs.Save();

    }

    public void Load()
    {

        //ENEMY LOAD

        enemyStats.GoldAmt = PlayerPrefs.GetFloat("GoldAmt");

        for (int i = 0; i < enemyStats.adventures.Length; i++)
        {
            enemyStats.adventures[i].isCompleted = PlayerPrefs.GetInt($"AdventureIsCompleted_{i}");
        }


        //PLAYER LOAD
        if (PlayerPrefs.GetInt("Level") <= 1)
        {
            playerStats.level = 1;
        }
        else
        {
            playerStats.level = PlayerPrefs.GetInt("Level");
        }

        playerStats.currentXp = PlayerPrefs.GetFloat("Xp");
        prestige.baseXP = PlayerPrefs.GetFloat("BaseXp");


        //SLOT UPGRADES LOAD
        slotUpgrades.slotStructs[0].slotLvl = PlayerPrefs.GetInt("SlotOneLvl");
        slotUpgrades.slotStructs[1].slotLvl = PlayerPrefs.GetInt("SlotTwoLvl");
        slotUpgrades.slotStructs[2].slotLvl = PlayerPrefs.GetInt("SlotThreeLvl");

        blacksmithToggleManager.AutoBuyerLvl = PlayerPrefs.GetInt("AutoBuyerLvl");
        blacksmithToggleManager.AutoBuyerAmt = PlayerPrefs.GetInt("AutoBuyerAmt");

        //PRESTIGE LOAD
        if (PlayerPrefs.GetFloat("PrestigeMulti") <= 1)
        {
            prestige.prestigeMulti = 1;
        }
        else
        {
            prestige.prestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");
        }


        //UPGRADES LOAD
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                upgrades.roles[r].upgrades[i].metalCount = PlayerPrefs.GetFloat($"UpgradeMetalCount_{i}");
            }

            playerStats.atkMetalCount = PlayerPrefs.GetInt("AtkMetalCount");
            playerStats.defMetalCount = PlayerPrefs.GetInt("DefMetalCount");
            playerStats.hpMetalCount = PlayerPrefs.GetInt("HpMetalCount");

            //ALCHEMY LOAD

            AlchemyLoad();


            //BOOK LOAD

            for (int i = 0; i < bestiary.entry.Length; i++)
            {
                bestiary.entry[i].IsDefeated = PlayerPrefs.GetInt($"IsDefeated_{i}");
            }

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

            Debug.Log("Load");
        }
    }

    public void HardReset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        prestige.SoftRest();
        playerStats.level = 1;
        prestige.baseXP = 0;
        prestige.prestigeMulti = 1;

        playerStats.atkMetalCount = 0;
        playerStats.defMetalCount = 0;
        playerStats.hpMetalCount = 0;

        prestige.UpdatePrestigeText();
        prestige.UpdatePostPrestigeText();
        playerStats.UpdateStatText();
        enemyStats.UpdateEnemyStatsText();

        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            slotUpgrades.UpdateSlotText(i);
        }

        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                upgrades.roles[r].upgrades[i].metalCount = 0;
            }
            alchemyTimers.AlchAutoBuyerAmt = 0;
            alchemyTimers.AlchAutoBuyerLvl = 0;

            //BOOK RESET
            for (int i = 0; i < bestiary.entry.Length; i++)
            {
                bestiary.entry[i].IsDefeated = 0;
            }
        }
    }

    public void AlchemySave()
    {
        PlayerPrefs.SetInt("AlchAutoBuyerLvl", alchemyTimers.AlchAutoBuyerLvl);
        PlayerPrefs.SetInt("AlchAutoBuyerAmt", alchemyTimers.AlchAutoBuyerAmt);
    }

    public void AlchemyLoad()
    {
        alchemyTimers.AlchAutoBuyerLvl = PlayerPrefs.GetInt("AlchAutoBuyerLvl");
        alchemyTimers.AlchAutoBuyerAmt = PlayerPrefs.GetInt("AlchAutoBuyerAmt");
        alchemyTimers.AlchAutoBuyerAmt = alchemyTimers.AlchAutoBuyerLvl;
    }
}
