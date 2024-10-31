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

    public string[] enemyNameArray = new string[] { "Imp", "Ogre", "Wight", "Ghost", "Hill Giant", "Drider", "Stone Giant", "Chain Devil", "Treant", "Guardian Naga", "Djinni", "Arch Mage", "Adult White Dragon", "Ice Devil", "Purple Worm", "Iron Golem", "Adult Red Dragon", "Dragon Turtle", "Balor", "Lich" };
    public int[] enemyHpMax = new int[] { 30, 100, 150, 200, 500, 600, 700, 750, 800, 950, 1000, 1050, 1150, 1250, 1400, 1600, 1700, 1800, 1900, 200000 };
    public int[] enemyDefArray = new int[] { 2, 4, 4, 4, 10, 10, 10, 10, 10, 20, 20, 20, 20, 25, 30, 30, 30, 30, 30, 350 };
    public int[] enemyAtkArray = new int[] { 5, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 1250 };
    public int[] xpRwdArray = new int[] { 5, 10, 20, 50, 75, 100, 125, 150, 175, 200, 225, 250, 275, 300, 325, 350, 375, 400, 450, 500 };
    public float[] goldRwdArray = new float[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 400 };






    void Start()
    {
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
        if (EnemyCurrentHp > 0)
        {
            if (playerStats.atk > EnemyDef)
            {
                EnemyCurrentHp -= playerStats.atk - EnemyDef;
                enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
                UpdateEnemyStatsText();
                if (EnemyCurrentHp <= 0)
                {
                    playerStats?.AddXp();
                    playerStats?.AddGold();

                    EnemyCurrentHp = EnemyMaxHp;
                    playerStats.currentHp = playerStats.maxHp;
                    UpdateEnemyStatsText();
                }
            }
            else
            {
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
            GoldAmtText.text = "Gold : " + GoldAmt;
        }
        if (EnemyHpText != null)
        {
            EnemyHpText.text = EnemyCurrentHp + "/" + EnemyMaxHp;
        }
        if (StageNumber != null)
        {
            StageNumber.text = "Stage: " + Stage + "/20";
        }

    }
}


