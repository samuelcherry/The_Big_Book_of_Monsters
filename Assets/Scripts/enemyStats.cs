using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EnemyStats : MonoBehaviour
{
    public PlayerStats playerStats;
    public SaveManager saveManager;
    public Prestige prestige;
    public ProgressBarTimer progressBarTimer;
    public TMP_Text EnemyHpText, StageNumber, EnemyNameText, GoldAmtText;
    public Slider enemyHpBar;

    public int Stage;
    public float GoldAmt;

    [System.Serializable]
    public class Enemy
    {
        public string enemyName;
        public float enemyMaxHp, enemyCurrentHp, enemyDef, enemyAtk, xpRwd, goldRwd, baseXpRwd, baseGoldRwd, enemySpeed;

        // Constructor to initialize all fields
        public Enemy(string name, float maxHp, float def, float atk, float xpRwd, float goldRwd, float baseXpRwd, float baseGoldRwd, float speed)
        {
            enemyName = name;
            enemyMaxHp = maxHp;
            enemyCurrentHp = maxHp;
            enemyDef = def;
            enemyAtk = atk;

            this.xpRwd = xpRwd;
            this.goldRwd = goldRwd;

            this.baseXpRwd = baseXpRwd;
            this.baseGoldRwd = baseGoldRwd;

            this.enemySpeed = speed;
        }
    }
    public Enemy[] enemies = new Enemy[20];
    void Awake()
    {
        Stage = 1;

        enemies[0] = new Enemy("Imp", 30, 2, 5, 5, 2, 5, 2, 4);
        enemies[1] = new Enemy("Ogre", 45, 3, 8, 10, 3, 10, 3, 3.8f);
        enemies[2] = new Enemy("Wight", 80, 5, 12, 15, 4, 15, 4, 3.6f);
        enemies[3] = new Enemy("Ghost", 100, 8, 18, 20, 5, 20, 5, 3.4f);
        enemies[4] = new Enemy("Hill Giant", 200, 16, 27, 30, 6, 30, 6, 3.2f);
        enemies[5] = new Enemy("Drider", 300, 24, 41, 40, 8, 40, 8, 3f);
        enemies[6] = new Enemy("Stone Giant", 500, 36, 62, 50, 10, 50, 10, 2.8f);
        enemies[7] = new Enemy("Chain Devil", 800, 54, 93, 60, 12, 60, 12, 2.6f);
        enemies[8] = new Enemy("Treant", 1000, 81, 140, 70, 14, 70, 14, 2.4f);
        enemies[9] = new Enemy("Guardian Naga", 2000, 162, 210, 90, 16, 90, 16, 2.2f);
        enemies[10] = new Enemy("Djinni", 3000, 243, 315, 110, 20, 110, 20, 2F);
        enemies[11] = new Enemy("Arch Mage", 5000, 365, 473, 130, 25, 130, 25, 1.8f);
        enemies[12] = new Enemy("Adult White Dragon", 8000, 548, 710, 150, 30, 150, 30, 1.6f);
        enemies[13] = new Enemy("Ice Devil", 10000, 822, 1065, 170, 35, 170, 35, 1.4f);
        enemies[14] = new Enemy("Purple Worm", 20000, 1644, 1598, 200, 40, 200, 40, 1.2f);
        enemies[15] = new Enemy("Iron Golem", 30000, 2466, 2397, 240, 50, 240, 50, 1f);
        enemies[16] = new Enemy("Adult Red Dragon", 50000, 3699, 3596, 280, 75, 280, 75, 0.8f);
        enemies[17] = new Enemy("Dragon Turtle", 80000, 5549, 5394, 320, 125, 320, 125, 0.6f);
        enemies[18] = new Enemy("Balor", 100000, 8324, 8091, 360, 200, 360, 200, 0.4f);
        enemies[19] = new Enemy("Lich", 200000, 16648, 12137, 400, 300, 400, 300, 0.2f);
    }



    public void ResetEnemies()
    {
        enemies[Stage - 1].goldRwd = enemies[Stage - 1].baseGoldRwd * prestige.prestigeMulti;
        enemies[Stage - 1].xpRwd = enemies[Stage - 1].baseXpRwd * prestige.prestigeMulti;

        enemies[Stage - 1].enemyCurrentHp = enemies[Stage - 1].enemyMaxHp;
        enemyHpBar.value = enemies[Stage - 1].enemyCurrentHp / enemies[Stage - 1].enemyMaxHp;
        UpdateEnemyStatsText();
    }

    void Start()
    {
        ResetEnemies();
    }

    public void TakeDamage()
    {
        if (Stage <= 0 || Stage > enemies.Length) return;  // Bounds check

        var currentEnemy = enemies[Stage - 1];
        if (currentEnemy.enemyCurrentHp > 0 && playerStats.atk > currentEnemy.enemyDef)
        {
            currentEnemy.enemyCurrentHp -= playerStats.atk - currentEnemy.enemyDef;
            enemyHpBar.value = currentEnemy.enemyCurrentHp / currentEnemy.enemyMaxHp;

            if (currentEnemy.enemyCurrentHp <= 0)  // Prevent going negative
            {
                progressBarTimer.EnemyDies();
                playerStats.AddGold();
                playerStats.AddXp();
                currentEnemy.enemyCurrentHp = currentEnemy.enemyMaxHp;
                playerStats.currentHp = playerStats.maxHp;
            }
        }
        UpdateEnemyStatsText();
    }


    public void IncreaseStage()
    {
        if (Stage < 20)
        {
            Stage += 1;
            playerStats.currentHp = playerStats.maxHp;
            progressBarTimer.enemyAtkTime = enemies[Stage - 1].enemySpeed;
            progressBarTimer.SetStageAnimation();
            ResetEnemies();
            UpdateEnemyStatsText();
            playerStats.UpdateStatText();
        }
    }

    public void DecreaseStage()
    {
        if (Stage > 1)
        {
            Stage -= 1;
            playerStats.currentHp = playerStats.maxHp;
            progressBarTimer.enemyAtkTime = enemies[Stage - 1].enemySpeed;
            progressBarTimer.SetStageAnimation();
            ResetEnemies();
            UpdateEnemyStatsText();
            playerStats.UpdateStatText();
        }
    }

    public void UpdateEnemyStatsText()
    {

        EnemyNameText.text = enemies[Stage - 1].enemyName;
        enemyHpBar.value = enemies[Stage - 1].enemyCurrentHp / enemies[Stage - 1].enemyMaxHp;
        EnemyHpText.text = playerStats.FormatStatValue(enemies[Stage - 1].enemyCurrentHp) + "/" + playerStats.FormatStatValue(enemies[Stage - 1].enemyMaxHp);
        StageNumber.text = "Stage: " + Stage + "/20";
    }
}



