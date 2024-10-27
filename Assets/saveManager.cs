using UnityEngine;



public class SaveManager : MonoBehaviour
{
    public EnemyStats enemyStats;
    public PlayerStats playerStats;
    public int gold;

    public void Save()
    {
        int GoldAmt = enemyStats.GoldAmt;
        int Level = playerStats.level;
        int UpgradeLevel = playerStats.upgradeLvl;
        float Xp = playerStats.currentXp;

        PlayerPrefs.SetInt("GoldAmt", GoldAmt);
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.SetInt("Upgrade Level", UpgradeLevel);
        PlayerPrefs.SetFloat("Xp", Xp);

        PlayerPrefs.Save();
        Debug.Log("Save");
    }

    public void Load()
    {
        int GoldAmt = PlayerPrefs.GetInt("GoldAmt");
        int Level = PlayerPrefs.GetInt("Level");
        int UpgradeLevel = PlayerPrefs.GetInt("Upgrade Level");
        float Xp = PlayerPrefs.GetFloat("Xp");

        enemyStats.GoldAmt = GoldAmt;
        playerStats.level = Level;
        playerStats.upgradeLvl = UpgradeLevel;
        playerStats.currentXp = Xp;


        Debug.Log("Load");
    }
}