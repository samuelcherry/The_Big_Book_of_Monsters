using JetBrains.Annotations;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public Prestige prestige;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
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
        PlayerPrefs.SetInt("AtkMetalCount", playerStats.atkMetalCount);
        PlayerPrefs.SetInt("DefMetalCount", playerStats.defMetalCount);
        PlayerPrefs.SetInt("HpMetalCount", playerStats.hpMetalCount);

        PlayerPrefs.SetInt("AutoBuyerLvl", blacksmithToggleManager.AutoBuyerLvl);
        PlayerPrefs.SetInt("AutoBuyerAmt", blacksmithToggleManager.AutoBuyerAmt);

        AlchemySave();

        PlayerPrefs.Save();

    }

    public void Load()
    {
        enemyStats.GoldAmt = PlayerPrefs.GetFloat("GoldAmt");
        playerStats.level = PlayerPrefs.GetInt("Level");

        slotUpgrades.slotStructs[0].slotLvl = PlayerPrefs.GetInt("SlotOneLvl");
        slotUpgrades.slotStructs[1].slotLvl = PlayerPrefs.GetInt("SlotTwoLvl");
        slotUpgrades.slotStructs[2].slotLvl = PlayerPrefs.GetInt("SlotThreeLvl");

        playerStats.currentXp = PlayerPrefs.GetFloat("Xp");
        prestige.baseXP = PlayerPrefs.GetFloat("BaseXp");

        prestige.prestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");
        playerStats.atkMetalCount = PlayerPrefs.GetInt("AtkMetalCount");
        playerStats.defMetalCount = PlayerPrefs.GetInt("DefMetalCount");
        playerStats.hpMetalCount = PlayerPrefs.GetInt("HpMetalCount");

        blacksmithToggleManager.AutoBuyerLvl = PlayerPrefs.GetInt("AutoBuyerLvl");
        blacksmithToggleManager.AutoBuyerAmt = PlayerPrefs.GetInt("AutoBuyerAmt");

        AlchemyLoad();

        prestige.UpdatePostPrestigeText();

    }

    public void HardReset()
    {
        prestige.SoftRest();
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

        for (int i = 0; i < upgrades.upgrades.Length; i++)
        {
            upgrades.upgrades[i].metalCount = 0;
            upgrades.upgrades[i].metalSlider.value = upgrades.upgrades[i].metalCount / upgrades.upgrades[i].metalMax;
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
