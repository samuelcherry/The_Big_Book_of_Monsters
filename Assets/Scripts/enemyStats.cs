using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class EnemyStats : MonoBehaviour
{
    public PlayerStats playerStats;
    public SaveManager saveManager;
    public Prestige prestige;
    public ProgressBarTimer progressBarTimer;
    public Bestiary bestiary;
    public TMP_Text EnemyHpText, StageNumber, EnemyNameText, GoldAmtText;
    public Slider enemyHpBar;
    private bool isButtonPressed;


    public int Stage;
    public float GoldAmt;

    [System.Serializable]

    public class Adventure
    {
        public int maxStages;
        public string adventureTitle;
        public Enemy[] enemies;

        public Adventure(string adventureTitle, int maxStages, Enemy[] enemies)
        {
            this.adventureTitle = adventureTitle;
            this.maxStages = maxStages;
            this.enemies = enemies;
        }

        [System.Serializable]

        public class Enemy
        {
            public string enemyName;
            public float enemyMaxHp, enemyCurrentHp, enemyDef, enemyAtk, xpRwd, baseXpRwd, goldRwd, baseGoldRwd, enemySpeed, enemyId;

            // Constructor to initialize all fields
            public Enemy(string name, float maxHp, float def, float atk, float xpRwd, float baseXpRwd, float baseGoldRwd, float goldRwd, float speed, int enemyId)
            {
                enemyName = name;
                enemyMaxHp = maxHp;
                enemyCurrentHp = maxHp;
                enemyDef = def;
                enemyAtk = atk;

                this.xpRwd = xpRwd;
                this.baseXpRwd = baseXpRwd;

                this.goldRwd = goldRwd;
                this.baseGoldRwd = baseGoldRwd;

                enemySpeed = speed;
                this.enemyId = enemyId;
            }
        }
    }

    public Adventure[] adventures;
    public Adventure currentAdventure;

    void Awake()
    {
        Stage = 1;

        adventures = new Adventure[4];

        Adventure.Enemy[] adventure1Enemies = new Adventure.Enemy[5];
        Adventure.Enemy[] adventure2Enemies = new Adventure.Enemy[10];
        Adventure.Enemy[] adventure3Enemies = new Adventure.Enemy[15];
        Adventure.Enemy[] adventure4Enemies = new Adventure.Enemy[20];

        adventure1Enemies[0] = new Adventure.Enemy("Goblin grunt", 30, 2, 5, 5, 5, 2, 2, 4, 0);
        adventure1Enemies[1] = new Adventure.Enemy("Goblin Soldier", 45, 3, 8, 10, 10, 3, 3, 3.8f, 1);
        adventure1Enemies[2] = new Adventure.Enemy("Goblin Elite", 80, 5, 12, 15, 15, 4, 4, 3.6f, 2);
        adventure1Enemies[3] = new Adventure.Enemy("Goblin Captain", 100, 8, 18, 20, 20, 5, 5, 3.4f, 3);
        adventure1Enemies[4] = new Adventure.Enemy("Goblin Chief", 200, 16, 27, 30, 30, 6, 6, 3.2f, 4);

        adventure2Enemies[0] = new Adventure.Enemy("Goblin grunt", 60, 4, 10, 10, 10, 4, 4, 4, 0);
        adventure2Enemies[1] = new Adventure.Enemy("Goblin Soldier", 90, 6, 16, 20, 20, 6, 6, 3.8f, 1);
        adventure2Enemies[2] = new Adventure.Enemy("Goblin Elite", 160, 10, 24, 30, 30, 8, 8, 3.6f, 2);
        adventure2Enemies[3] = new Adventure.Enemy("Goblin Captain", 200, 16, 36, 40, 40, 10, 10, 3.4f, 3);
        adventure2Enemies[4] = new Adventure.Enemy("Goblin Chief", 400, 32, 54, 60, 60, 12, 12, 3.2f, 4);
        adventure2Enemies[5] = new Adventure.Enemy("Mushroom Varient", 600, 48, 82, 80, 80, 16, 16, 3f, 5);
        adventure2Enemies[6] = new Adventure.Enemy("Mushroom Specimen", 1000, 72, 124, 100, 100, 20, 20, 2.8f, 6);
        adventure2Enemies[7] = new Adventure.Enemy("Mushroom Mutant", 1600, 108, 186, 120, 120, 24, 24, 2.6f, 7);
        adventure2Enemies[8] = new Adventure.Enemy("Mushroom Monstrosity", 2000, 162, 280, 140, 140, 28, 28, 2.4f, 8);
        adventure2Enemies[9] = new Adventure.Enemy("Mushroom Abomination", 4000, 324, 410, 180, 180, 32, 32, 2.2f, 9);

        adventure3Enemies[0] = new Adventure.Enemy("Goblin grunt", 90, 6, 15, 15, 15, 6, 6, 4, 0);
        adventure3Enemies[1] = new Adventure.Enemy("Goblin Soldier", 135, 9, 24, 30, 30, 9, 9, 3.8f, 1);
        adventure3Enemies[2] = new Adventure.Enemy("Goblin Elite", 240, 15, 36, 45, 45, 12, 12, 3.6f, 2);
        adventure3Enemies[3] = new Adventure.Enemy("Goblin Captain", 300, 24, 54, 60, 60, 15, 15, 3.4f, 3);
        adventure3Enemies[4] = new Adventure.Enemy("Goblin Chief", 600, 48, 81, 90, 90, 18, 18, 3.2f, 4);
        adventure3Enemies[5] = new Adventure.Enemy("Mushroom Varient", 900, 72, 123, 120, 120, 24, 24, 3f, 5);
        adventure3Enemies[6] = new Adventure.Enemy("Mushroom Specimen", 1500, 108, 186, 150, 150, 30, 30, 2.8f, 6);
        adventure3Enemies[7] = new Adventure.Enemy("Mushroom Mutant", 2400, 162, 279, 180, 180, 36, 36, 2.6f, 7);
        adventure3Enemies[8] = new Adventure.Enemy("Mushroom Monstrosity", 3000, 243, 420, 210, 210, 41, 41, 2.4f, 8);
        adventure3Enemies[9] = new Adventure.Enemy("Mushroom Abomination", 6000, 486, 630, 270, 270, 48, 48, 2.2f, 9);
        adventure3Enemies[10] = new Adventure.Enemy("Flying Eye Tracker", 9000, 729, 945, 330, 330, 60, 60, 2f, 10);
        adventure3Enemies[11] = new Adventure.Enemy("Flying Eye Informer", 15000, 1095, 1419, 390, 390, 75, 75, 1.8f, 11);
        adventure3Enemies[12] = new Adventure.Enemy("Flying Eye Spy", 24000, 1644, 2130, 450, 450, 90, 90, 1.6f, 12);
        adventure3Enemies[13] = new Adventure.Enemy("Flying Eye Agent", 30000, 2466, 3195, 510, 510, 105, 105, 1.4f, 13);
        adventure3Enemies[14] = new Adventure.Enemy("Flying Eye Master Mind", 60000, 4932, 4794, 600, 600, 120, 120, 1.2f, 14);

        adventure4Enemies[0] = new Adventure.Enemy("Goblin grunt", 480, 8, 20, 20, 20, 8, 8, 4, 0);
        adventure4Enemies[1] = new Adventure.Enemy("Goblin Soldier", 180, 12, 32, 40, 40, 12, 12, 3.8f, 1);
        adventure4Enemies[2] = new Adventure.Enemy("Goblin Elite", 320, 20, 48, 60, 60, 16, 16, 3.6f, 2);
        adventure4Enemies[3] = new Adventure.Enemy("Goblin Captain", 400, 32, 72, 80, 80, 20, 20, 3.4f, 3);
        adventure4Enemies[4] = new Adventure.Enemy("Goblin Chief", 800, 64, 108, 120, 120, 24, 24, 3.2f, 4);
        adventure4Enemies[5] = new Adventure.Enemy("Mushroom Varient", 1200, 96, 164, 160, 160, 32, 32, 3f, 5);
        adventure4Enemies[6] = new Adventure.Enemy("Mushroom Specimen", 2000, 144, 248, 200, 200, 40, 40, 2.8f, 6);
        adventure4Enemies[7] = new Adventure.Enemy("Mushroom Mutant", 3200, 216, 372, 240, 240, 48, 48, 2.6f, 7);
        adventure4Enemies[8] = new Adventure.Enemy("Mushroom Monstrosity", 4000, 324, 560, 280, 280, 56, 56, 2.4f, 8);
        adventure4Enemies[9] = new Adventure.Enemy("Mushroom Abomination", 8000, 648, 840, 360, 360, 64, 64, 2.2f, 9);
        adventure4Enemies[10] = new Adventure.Enemy("Flying Eye Tracker", 12000, 972, 1260, 440, 440, 80, 80, 2f, 10);
        adventure4Enemies[11] = new Adventure.Enemy("Flying Eye Informer", 20000, 1460, 1892, 520, 520, 100, 100, 1.8f, 11);
        adventure4Enemies[12] = new Adventure.Enemy("Flying Eye Spy", 32000, 2192, 2840, 600, 600, 120, 120, 1.6f, 12);
        adventure4Enemies[13] = new Adventure.Enemy("Flying Eye Agent", 40000, 3288, 4260, 680, 680, 140, 140, 1.4f, 13);
        adventure4Enemies[14] = new Adventure.Enemy("Flying Eye Master Mind", 80000, 6576, 6392, 800, 800, 800, 160, 1.2f, 14);
        adventure4Enemies[15] = new Adventure.Enemy("Skeleton Fighter", 120000, 9864, 9588, 960, 960, 200, 200, 1f, 15);
        adventure4Enemies[16] = new Adventure.Enemy("Skeleton Guard", 200000, 14796, 14384, 1120, 1120, 300, 300, 0.8f, 16);
        adventure4Enemies[17] = new Adventure.Enemy("Skeleton Soldier", 320000, 22196, 21576, 1280, 1280, 500, 500, 0.6f, 17);
        adventure4Enemies[18] = new Adventure.Enemy("Skeleton Knight", 400000, 33296, 32364, 1440, 1440, 800, 800, 0.4f, 18);
        adventure4Enemies[19] = new Adventure.Enemy("Skeleton King", 800000, 66592, 48548, 1600, 1600, 1200, 1200, 0.2f, 19);



        adventures[0] = new Adventure("Goblin Camp", 5, adventure1Enemies);
        adventures[1] = new Adventure("Mushroom Farm", 10, adventure2Enemies);
        adventures[2] = new Adventure("Mushroom Farm", 15, adventure3Enemies);
        adventures[3] = new Adventure("Mushroom Farm", 20, adventure4Enemies);


        currentAdventure = adventures[0];
    }



    public void ResetEnemies()
    {
        var currentEnemy = currentAdventure.enemies[Stage - 1];
        ResetPrestige();
        currentEnemy.enemyCurrentHp = currentEnemy.enemyMaxHp;
        enemyHpBar.value = currentEnemy.enemyCurrentHp / currentEnemy.enemyMaxHp;
        //Speed Reset
        progressBarTimer.enemyAtkTime = currentAdventure.enemies[Stage - 1].enemySpeed;
        progressBarTimer.playerAtkTime = playerStats.speedArray[playerStats.level - 1];
        UpdateEnemyStatsText();
    }

    public void ResetPrestige()
    {
        var currentEnemy = currentAdventure.enemies[Stage - 1];
        currentEnemy.goldRwd = currentEnemy.baseGoldRwd * prestige.prestigeMulti;
        currentEnemy.xpRwd = currentEnemy.baseXpRwd * prestige.prestigeMulti;
    }

    void Start()
    {
        ResetEnemies();
    }

    public void TakeDamage()
    {
        if (Stage <= 0 || Stage > currentAdventure.maxStages) return;  // Bounds check

        var currentEnemy = currentAdventure.enemies[Stage - 1];
        if (currentEnemy.enemyCurrentHp > 0 && playerStats.atk > currentEnemy.enemyDef)
        {
            currentEnemy.enemyCurrentHp -= playerStats.atk - currentEnemy.enemyDef;
            enemyHpBar.value = currentEnemy.enemyCurrentHp / currentEnemy.enemyMaxHp;
            if (currentEnemy.enemyCurrentHp <= 0)  // KILL ENEMY
            {
                playerStats.AddGold();
                playerStats.AddXp();
                currentEnemy.enemyCurrentHp = currentEnemy.enemyMaxHp;
                playerStats.currentHp = playerStats.maxHp;
                for (int i = 0; i < bestiary.entry.Length; i++)
                {
                    if (bestiary.entry[i].EntryId == currentEnemy.enemyId)
                    {
                        if (bestiary.entry[i].IsDefeated == 0)
                        {
                            bestiary.entry[i].IsDefeated = 1;
                            saveManager.Save();
                        }
                    }
                }
            }
        }
        UpdateEnemyStatsText();
    }


    public void IncreaseStage()
    {
        if (Stage < currentAdventure.enemies.Length)
        {
            if (isButtonPressed) return;  // Prevent action if button is still in cooldown
            StartCoroutine(HandleButtonCooldown());

            Stage++;

            playerStats.currentHp = playerStats.maxHp;
            progressBarTimer.enemyAtkTime = currentAdventure.enemies[Stage - 1].enemySpeed;
            progressBarTimer.playerAtkTime = playerStats.speedArray[playerStats.level - 1];
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
            if (isButtonPressed) return;  // Prevent action if button is still in cooldown
            StartCoroutine(HandleButtonCooldown());

            Stage--;

            playerStats.currentHp = playerStats.maxHp;
            progressBarTimer.enemyAtkTime = currentAdventure.enemies[Stage - 1].enemySpeed;
            progressBarTimer.playerAtkTime = playerStats.speedArray[playerStats.level - 1];
            progressBarTimer.SetStageAnimation();
            ResetEnemies();
            UpdateEnemyStatsText();
            playerStats.UpdateStatText();
        }
    }

    private IEnumerator HandleButtonCooldown()
    {
        isButtonPressed = true;
        yield return new WaitForSeconds(1f);  // Wait for 0.1 seconds (adjust as needed)
        isButtonPressed = false;
        Debug.Log("CoolDown");
    }

    public void SelectAdventure(int adventureIndex)
    {
        if (adventures[adventureIndex] != currentAdventure)
        {
            currentAdventure = adventures[adventureIndex];
            ResetEnemies();
            prestige.PrestigeHero();
        }
    }

    public void UpdateEnemyStatsText()
    {

        EnemyNameText.text = currentAdventure.enemies[Stage - 1].enemyName;
        enemyHpBar.value = currentAdventure.enemies[Stage - 1].enemyCurrentHp / currentAdventure.enemies[Stage - 1].enemyMaxHp;
        EnemyHpText.text = playerStats.FormatStatValue(currentAdventure.enemies[Stage - 1].enemyCurrentHp) + "/" + playerStats.FormatStatValue(currentAdventure.enemies[Stage - 1].enemyMaxHp);
        StageNumber.text = "Stage: " + Stage + "/" + currentAdventure.maxStages;
    }
}



