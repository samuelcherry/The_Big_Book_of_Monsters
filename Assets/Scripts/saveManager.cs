using UnityEngine;



public class SaveManager : MonoBehaviour
{
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public Prestige prestige;
    public Upgrades upgrades;
    public SlotUpgrades slotUpgrades;
    public int gold;




    public void Save()
    {
        float GoldAmt = enemyStats.GoldAmt;
        int Level = playerStats.level;
        int SlotOneLvl = slotUpgrades.slotOneLvl;
        int SlotTwoLvl = slotUpgrades.slotOneLvl;
        int SlotThreeLvl = slotUpgrades.slotOneLvl;
        int SlotFourLvl = slotUpgrades.slotOneLvl;
        float Xp = playerStats.currentXp;
        float BaseXp = prestige.baseXP;
        float PrestigeMulti = prestige.prestigeMulti;

        PlayerPrefs.SetFloat("GoldAmt", GoldAmt);
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.SetInt("SlotOneLvl", SlotOneLvl);
        PlayerPrefs.SetInt("SlotTwoLvl", SlotTwoLvl);
        PlayerPrefs.SetInt("SlotThreeLvl", SlotThreeLvl);
        PlayerPrefs.SetInt("SlotFourLvl", SlotFourLvl);
        PlayerPrefs.SetFloat("Xp", Xp);
        PlayerPrefs.SetFloat("BaseXp", BaseXp);
        PlayerPrefs.SetFloat("PrestigeMulti", PrestigeMulti);

        for (int i = 0; i < upgrades.upgrades.Length; i++)
        {
            PlayerPrefs.SetInt("UpgradeMetalCount_" + i, upgrades.upgrades[i].metalCount);
        }

        PlayerPrefs.Save();
        Debug.Log("Save");
    }

    public void Load()
    {
        float GoldAmt = PlayerPrefs.GetFloat("GoldAmt");
        int Level = PlayerPrefs.GetInt("Level");
        int SlotOneLvl = PlayerPrefs.GetInt("SlotOneLvl");
        int SlotTwoLvl = PlayerPrefs.GetInt("SlotTwoLvl");
        int SlotThreeLvl = PlayerPrefs.GetInt("SlotThreeLvl");
        int SlotFourLvl = PlayerPrefs.GetInt("SlotFourLvl");
        float Xp = PlayerPrefs.GetFloat("Xp");
        float BaseXp = PlayerPrefs.GetFloat("BaseXp");
        float PrestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");

        enemyStats.GoldAmt = GoldAmt;
        playerStats.level = Level;
        slotUpgrades.slotOneLvl = SlotOneLvl;
        slotUpgrades.slotTwoLvl = SlotTwoLvl;
        slotUpgrades.slotThreeLvl = SlotThreeLvl;
        slotUpgrades.slotFourLvl = SlotFourLvl;
        playerStats.currentXp = Xp;
        prestige.baseXP = BaseXp;
        prestige.prestigeMulti = PrestigeMulti;

        for (int i = 0; i < upgrades.upgrades.Length; i++)
        {
            upgrades.upgrades[i].metalCount = PlayerPrefs.GetInt("UpgradeMetalCount_" + i, 0);
        }

    }
}