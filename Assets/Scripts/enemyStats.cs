using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public PlayerStats playerStats;
    public SaveManager saveManager;
    public Prestige prestige;
    public TMP_Text EnemyHpText;
    public TMP_Text StageNumber;
    public TMP_Text EnemyNameText;
    public TMP_Text GoldAmtText;
    public Slider enemyHpBar;

    public int Stage;
    public string EnemyName;
    public float EnemyCurrentHp;
    public float EnemyMaxHp;
    public int EnemyAtk;
    public int EnemyDef;

    public float GoldRwd;
    public int baseXpRwd;
    public float XpRwd;
    public float GoldAmt;

    // Declare arrays outside the Start function
    public string[] enemyNameArray;
    public int[] enemyHpMax;
    public int[] enemyDefArray;
    public int[] enemyAtkArray;
    public int[] xpRwdArray;
    public float[] goldRwdArray;

    void Start()
    {
        // Initialize arrays inside the Start function
        enemyNameArray = new string[] { "Imp", "Ogre", "Wight", "Ghost", "Hill Giant", "Drider", "Stone Giant", "Chain Devil", "Treant", "Guardian Naga", "Djinni", "Arch Mage", "Adult White Dragon", "Ice Devil", "Purple Worm", "Iron Golem", "Adult Red Dragon", "Dragon Turtle", "Balor", "Lich" };
        enemyHpMax = new int[] { 30, 100, 150, 200, 500, 600, 700, 750, 800, 950, 1000, 1050, 1150, 1250, 1400, 1600, 1700, 1800, 1900, 200000 };
        enemyDefArray = new int[] { 2, 4, 4, 4, 10, 10, 10, 10, 10, 20, 20, 20, 20, 25, 30, 30, 30, 30, 30, 350 };
        enemyAtkArray = new int[] { 5, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 1250 };
        xpRwdArray = new int[] { 5, 10, 20, 50, 75, 100, 125, 150, 175, 200, 225, 250, 275, 300, 325, 350, 375, 400, 450, 500 };
        goldRwdArray = new float[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 400 };

        saveManager.Load();

        Stage = 1;
        EnemyName = enemyNameArray[Stage - 1];
        EnemyMaxHp = enemyHpMax[Stage - 1];
        EnemyCurrentHp = EnemyMaxHp;
        EnemyDef = enemyDefArray[Stage - 1];
        baseXpRwd = xpRwdArray[Stage - 1];
        XpRwd = baseXpRwd * prestige.prestigeMulti;
        GoldRwd = goldRwdArray[Stage - 1] * prestige.prestigeMulti;
        EnemyAtk = enemyAtkArray[Stage - 1];
        GoldAmt = 0;
        enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;

        UpdateEnemyStatsText();
    }

    private void Update()
    {
        UpdateEnemyStatsText();
        baseXpRwd = xpRwdArray[Stage - 1];
        XpRwd = baseXpRwd * prestige.prestigeMulti;
        GoldRwd = goldRwdArray[Stage - 1] * prestige.prestigeMulti;
        enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
        EnemyAtk = enemyAtkArray[Stage - 1];

    }

    public void TakeDamage()
    {
        if (EnemyCurrentHp > 0) // Check if enemy HP is above zero before taking damage
        {
            if (playerStats.atk > EnemyDef)
            {
                // Calculate the damage and ensure it doesn't reduce HP below zero
                EnemyCurrentHp = Mathf.Max(0, EnemyCurrentHp - (playerStats.atk - EnemyDef));
                enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
                UpdateEnemyStatsText();

                // Check if enemy has been defeated
                if (EnemyCurrentHp == 0)
                {
                    // Grant rewards to player
                    playerStats?.AddGold();
                    playerStats?.AddXp();

                    // Reset enemy HP for the next encounter
                    EnemyCurrentHp = EnemyMaxHp;
                    playerStats.currentHp = playerStats.maxHp;
                    UpdateEnemyStatsText();
                }
            }
            else
            {
                Debug.Log("Player's attack is not strong enough to damage the enemy.");
            }
        }
        else
        {
            Debug.Log("Enemy already at 0 HP.");
        }
    }



    public void IncreaseStage()
    {
        if (Stage < 20)
        {
            Stage += 1;
            EnemyName = enemyNameArray[Stage - 1];
            EnemyMaxHp = enemyHpMax[Stage - 1];
            EnemyCurrentHp = EnemyMaxHp;
            EnemyDef = enemyDefArray[Stage - 1];
            GoldRwd = goldRwdArray[Stage - 1];

            playerStats.currentHp = playerStats.maxHp;
            UpdateEnemyStatsText();
            playerStats.UpdateStatText();
        }
        else
        {
        }
        UpdateEnemyStatsText();
    }

    public void DecreaseStage()
    {
        if (Stage > 1)
        {
            Stage -= 1;
            EnemyName = enemyNameArray[Stage - 1];
            EnemyMaxHp = enemyHpMax[Stage - 1];
            EnemyCurrentHp = EnemyMaxHp;
            EnemyDef = enemyDefArray[Stage - 1];
            GoldRwd = goldRwdArray[Stage - 1];

            playerStats.currentHp = playerStats.maxHp;
            UpdateEnemyStatsText();
            playerStats.UpdateStatText();
        }
        else
        {
        }
        UpdateEnemyStatsText();
    }

    public void UpdateEnemyStatsText()
    {
        if (EnemyNameText != null)
        {
            EnemyNameText.text = EnemyName;
        }
        if (GoldAmtText != null)
        {
            GoldAmtText.text = "Gold : " + playerStats.FormatStatValue(GoldAmt);
        }
        if (EnemyHpText != null)
        {
            EnemyHpText.text = playerStats.FormatStatValue(EnemyCurrentHp) + "/" + playerStats.FormatStatValue(EnemyMaxHp);
        }
        if (StageNumber != null)
        {
            StageNumber.text = "Stage: " + Stage + "/20";
        }

    }
}


