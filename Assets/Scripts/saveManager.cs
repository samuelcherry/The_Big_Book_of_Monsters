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

        float Xp = playerStats.currentXp;
        float BaseXp = prestige.baseXP;

        float PrestigeMulti = prestige.prestigeMulti;
        int AtkMetalCount = playerStats.atkMetalCount;
        int DefMetalCount = playerStats.defMetalCount;
        int HpMetalCount = playerStats.hpMetalCount;

        PlayerPrefs.SetFloat("GoldAmt", GoldAmt);
        PlayerPrefs.SetInt("Level", Level);

        PlayerPrefs.SetInt("SlotOneLvl", SlotOneLvl);
        PlayerPrefs.SetInt("SlotTwoLvl", SlotTwoLvl);
        PlayerPrefs.SetInt("SlotThreeLvl", SlotThreeLvl);

        PlayerPrefs.SetFloat("Xp", Xp);
        PlayerPrefs.SetFloat("BaseXp", BaseXp);
        PlayerPrefs.SetFloat("PrestigeMulti", PrestigeMulti);

        PlayerPrefs.SetInt("AtkMetalCount", AtkMetalCount);
        PlayerPrefs.SetInt("DefMetalCount", DefMetalCount);
        PlayerPrefs.SetInt("HpMetalCount", HpMetalCount);
        


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

        float Xp = PlayerPrefs.GetFloat("Xp");
        float BaseXp = PlayerPrefs.GetFloat("BaseXp");

        float PrestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");
        int AtkMetalCount = PlayerPrefs.GetInt("AtkMetalCount");
        int DefMetalCount = PlayerPrefs.GetInt("DefMetalCount");
        int HpMetalCount = PlayerPrefs.GetInt("HpMetalCount");

        enemyStats.GoldAmt = GoldAmt;
        
        playerStats.level = Level;

        slotUpgrades.slotOneLvl = SlotOneLvl;
        slotUpgrades.slotTwoLvl = SlotTwoLvl;
        slotUpgrades.slotThreeLvl = SlotThreeLvl;

        playerStats.currentXp = Xp;
        prestige.baseXP = BaseXp;

        prestige.prestigeMulti = PrestigeMulti;
        playerStats.atkMetalCount = AtkMetalCount;
        playerStats.defMetalCount = DefMetalCount;
        playerStats.hpMetalCount = HpMetalCount;


    }
}