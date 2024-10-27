using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EnemyStats : MonoBehaviour
{
    public int Stage;
    public string EnemyName;

    public float EnemyCurrentHp;
    public float EnemyMaxHp;
    public int EnemyAtk;
    public int EnemyDef;

    public int GoldRwd;
    public int XpRwd;
    public int GoldAmt;

    readonly string[] enemyNameArray = new string[] { "IMP", "Ogre", "Wight", "Ghost", "Hill Giant", "Drider", "Stone Giant", "Chain Devil", "Treant", "Guardian Naga", "Djinni", "Arch Mage", "Adult White Dragon", "Ice Devil", "Purple Worm", "Iron Golem", "Adult Red Dragon", "Dragon Turtle", "Balor", "LICH" };
    readonly int[] enemyHpMax = new int[] { 30, 100, 150, 200, 500, 600, 700, 750, 800, 950, 1000, 1050, 1150, 1250, 1400, 1600, 1700, 1800, 1900, 2000 };
    readonly int[] enemyDefArray = new int[] { 2, 4, 4, 4, 10, 10, 10, 10, 10, 20, 20, 20, 20, 25, 30, 30, 30, 30, 30, 35 };
    readonly int[] enemyAtkArray = new int[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 55, 60, 65, 70, 75, 80, 95, 100, 105, 105, 125 };
    readonly int[] xpRwdArray = new int[] { 5, 10, 20, 50, 75, 100, 125, 150, 175, 200, 225, 250, 275, 300, 325, 350, 375, 400, 450, 500 };
    readonly int[] goldRwdArray = new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40 };

    public PlayerStats playerStats;
    public SaveManager saveManager;
    public TMP_Text EnemyHpText;
    public TMP_Text EnemyDefText;
    public TMP_Text EnemyNameText;
    public TMP_Text GoldAmtText;

    public Slider enemyHpBar;




    void Start()
    {

        Stage = 1;
        EnemyName = enemyNameArray[Stage - 1];
        EnemyMaxHp = enemyHpMax[Stage - 1];
        EnemyCurrentHp = EnemyMaxHp;
        EnemyDef = enemyDefArray[Stage - 1];
        XpRwd = xpRwdArray[Stage - 1];
        GoldRwd = goldRwdArray[Stage - 1];
        EnemyAtk = enemyAtkArray[Stage - 1];
        GoldAmt = 0;
        enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
        saveManager.Load();
        UpdateEnemyStatsText();


    }

    private void Update()
    {
        UpdateEnemyStatsText();
        XpRwd = xpRwdArray[Stage - 1];
        GoldRwd = goldRwdArray[Stage - 1];
        enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;

    }

    public void TakeDamage()
    {
        if (EnemyCurrentHp > 0)
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
            Debug.Log("Enemy already at 0 HP.");
        }
    }


    public void IncreaseStage()
    {
        Debug.Log("Current Stage " + Stage);
        if (Stage < 20)
        {
            Stage += 1;
            EnemyName = enemyNameArray[Stage - 1];
            EnemyMaxHp = enemyHpMax[Stage - 1];
            EnemyCurrentHp = EnemyMaxHp;
            EnemyDef = enemyDefArray[Stage - 1];
            GoldRwd = goldRwdArray[Stage - 1];
            UpdateEnemyStatsText();
            Debug.Log("New Stage " + Stage);
        }
        else
        {
        }
    }

    public void DecreaseStage()
    {
        Debug.Log("Current Stage " + Stage);
        if (Stage > 1)
        {
            Stage -= 1;
            EnemyName = enemyNameArray[Stage - 1];
            EnemyMaxHp = enemyHpMax[Stage - 1];
            EnemyCurrentHp = EnemyMaxHp;
            EnemyDef = enemyDefArray[Stage - 1];
            GoldRwd = goldRwdArray[Stage - 1];
            UpdateEnemyStatsText();
            Debug.Log("New Stage " + Stage);
        }
        else
        {
        }
    }

    public void UpdateEnemyStatsText()
    {
        if (EnemyNameText != null)
        {
            EnemyNameText.text = EnemyName.ToString();
        }
        if (GoldAmtText != null)
        {
            GoldAmtText.text = GoldAmt.ToString();
        }
        if (EnemyHpText != null)
        {
            EnemyHpText.text = EnemyCurrentHp.ToString() + "/" + EnemyMaxHp.ToString();
        }
        if (EnemyDefText != null)
        {
            EnemyDefText.text = "Def: " + EnemyDef.ToString();
        }

    }
}


