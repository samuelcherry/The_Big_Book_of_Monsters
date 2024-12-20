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
    public ConfirmManager confirmManager;
    public TMP_Text EnemyHpText, StageNumber, EnemyNameText, GoldAmtText;
    public Slider enemyHpBar;
    private bool isButtonPressed;


    public int Stage;
    public float GoldAmt;
    public int tempAdventureNumber;

    [System.Serializable]
    public struct AdventureConfirmMenu
    {
        public GameObject Menu;
        public bool isVisible;
    }

    public AdventureConfirmMenu[] adventureConfirmMenus = new AdventureConfirmMenu[1];

    [System.Serializable]

    public class Adventure
    {
        public int maxStages, adventureId;
        public string adventureTitle;
        public Enemy[] enemies;
        public int isCompleted;
        public int prevCompleted;

        public Adventure(string adventureTitle, int maxStages, Enemy[] enemies, int adventureId, int isCompleted)
        {
            this.adventureTitle = adventureTitle;
            this.maxStages = maxStages;
            this.enemies = enemies;
            this.adventureId = adventureId;
            this.isCompleted = isCompleted;
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
        adventure1Enemies[1] = new Adventure.Enemy("Goblin Soldier", 45, 4, 8, 10, 10, 3, 3, 3.8f, 1);
        adventure1Enemies[2] = new Adventure.Enemy("Goblin Elite", 80, 12, 8, 15, 15, 4, 4, 3.6f, 2);
        adventure1Enemies[3] = new Adventure.Enemy("Goblin Captain", 100, 20, 16, 20, 20, 5, 5, 3.4f, 3);
        adventure1Enemies[4] = new Adventure.Enemy("Goblin Chief", 200, 30, 32, 30, 30, 6, 6, 3.2f, 4);

        adventure2Enemies[0] = new Adventure.Enemy("Goblin grunt", 60, 4, 4, 10, 10, 4, 4, 4, 0);
        adventure2Enemies[1] = new Adventure.Enemy("Goblin Soldier", 90, 8, 6, 20, 20, 6, 6, 3.8f, 1);
        adventure2Enemies[2] = new Adventure.Enemy("Goblin Elite", 160, 14, 10, 30, 30, 8, 8, 3.6f, 2);
        adventure2Enemies[3] = new Adventure.Enemy("Goblin Captain", 200, 20, 18, 40, 40, 10, 10, 3.4f, 3);
        adventure2Enemies[4] = new Adventure.Enemy("Goblin Chief", 400, 32, 35, 60, 60, 12, 12, 3.2f, 4);
        adventure2Enemies[5] = new Adventure.Enemy("Mushroom Varient", 600, 45, 45, 80, 80, 16, 16, 3f, 5);
        adventure2Enemies[6] = new Adventure.Enemy("Mushroom Specimen", 1000, 70, 60, 100, 100, 20, 20, 2.8f, 6);
        adventure2Enemies[7] = new Adventure.Enemy("Mushroom Mutant", 1600, 100, 80, 120, 120, 24, 24, 2.6f, 7);
        adventure2Enemies[8] = new Adventure.Enemy("Mushroom Monstrosity", 2000, 140, 100, 140, 140, 28, 28, 2.4f, 8);
        adventure2Enemies[9] = new Adventure.Enemy("Mushroom Abomination", 4000, 190, 135, 180, 180, 32, 32, 2.2f, 9);

        adventure3Enemies[0] = new Adventure.Enemy("Goblin grunt", 90, 6, 5, 15, 15, 6, 6, 4, 0);
        adventure3Enemies[1] = new Adventure.Enemy("Goblin Soldier", 135, 10, 12, 30, 30, 9, 9, 3.8f, 1);
        adventure3Enemies[2] = new Adventure.Enemy("Goblin Elite", 240, 15, 18, 45, 45, 12, 12, 3.6f, 2);
        adventure3Enemies[3] = new Adventure.Enemy("Goblin Captain", 300, 22, 25, 60, 60, 15, 15, 3.4f, 3);
        adventure3Enemies[4] = new Adventure.Enemy("Goblin Chief", 600, 32, 35, 90, 90, 18, 18, 3.2f, 4);
        adventure3Enemies[5] = new Adventure.Enemy("Mushroom Varient", 900, 44, 60, 120, 120, 24, 24, 3f, 5);
        adventure3Enemies[6] = new Adventure.Enemy("Mushroom Specimen", 1500, 60, 90, 150, 150, 30, 30, 2.8f, 6);
        adventure3Enemies[7] = new Adventure.Enemy("Mushroom Mutant", 2400, 80, 120, 180, 180, 36, 36, 2.6f, 7);
        adventure3Enemies[8] = new Adventure.Enemy("Mushroom Monstrosity", 3000, 110, 150, 210, 210, 41, 41, 2.4f, 8);
        adventure3Enemies[9] = new Adventure.Enemy("Mushroom Abomination", 6000, 150, 190, 270, 270, 48, 48, 2.2f, 9);
        adventure3Enemies[10] = new Adventure.Enemy("Flying Eye Tracker", 9000, 200, 240, 330, 330, 60, 60, 2f, 10);
        adventure3Enemies[11] = new Adventure.Enemy("Flying Eye Informer", 15000, 275, 300, 390, 390, 75, 75, 1.8f, 11);
        adventure3Enemies[12] = new Adventure.Enemy("Flying Eye Spy", 24000, 375, 400, 450, 450, 90, 90, 1.6f, 12);
        adventure3Enemies[13] = new Adventure.Enemy("Flying Eye Agent", 30000, 500, 500, 510, 510, 105, 105, 1.4f, 13);
        adventure3Enemies[14] = new Adventure.Enemy("Flying Eye Master Mind", 60000, 650, 700, 600, 600, 120, 120, 1.2f, 14);

        adventure4Enemies[0] = new Adventure.Enemy("Goblin grunt", 480, 8, 8, 20, 20, 8, 8, 4, 0);
        adventure4Enemies[1] = new Adventure.Enemy("Goblin Soldier", 180, 15, 12, 40, 40, 12, 12, 3.8f, 1);
        adventure4Enemies[2] = new Adventure.Enemy("Goblin Elite", 320, 25, 18, 60, 60, 16, 16, 3.6f, 2);
        adventure4Enemies[3] = new Adventure.Enemy("Goblin Captain", 400, 50, 25, 80, 80, 20, 20, 3.4f, 3);
        adventure4Enemies[4] = new Adventure.Enemy("Goblin Chief", 800, 80, 35, 120, 120, 24, 24, 3.2f, 4);
        adventure4Enemies[5] = new Adventure.Enemy("Mushroom Varient", 1200, 105, 60, 160, 160, 32, 32, 3f, 5);
        adventure4Enemies[6] = new Adventure.Enemy("Mushroom Specimen", 2000, 140, 90, 200, 200, 40, 40, 2.8f, 6);
        adventure4Enemies[7] = new Adventure.Enemy("Mushroom Mutant", 3200, 180, 120, 240, 240, 48, 48, 2.6f, 7);
        adventure4Enemies[8] = new Adventure.Enemy("Mushroom Monstrosity", 4000, 220, 150, 280, 280, 56, 56, 2.4f, 8);
        adventure4Enemies[9] = new Adventure.Enemy("Mushroom Abomination", 8000, 300, 190, 360, 360, 64, 64, 2.2f, 9);
        adventure4Enemies[10] = new Adventure.Enemy("Flying Eye Tracker", 12000, 400, 240, 440, 440, 80, 80, 2f, 10);
        adventure4Enemies[11] = new Adventure.Enemy("Flying Eye Informer", 20000, 600, 300, 520, 520, 100, 100, 1.8f, 11);
        adventure4Enemies[12] = new Adventure.Enemy("Flying Eye Spy", 32000, 1000, 400, 600, 600, 120, 120, 1.6f, 12);
        adventure4Enemies[13] = new Adventure.Enemy("Flying Eye Agent", 40000, 1500, 500, 680, 680, 140, 140, 1.4f, 13);
        adventure4Enemies[14] = new Adventure.Enemy("Flying Eye Master Mind", 80000, 2000, 700, 800, 800, 800, 160, 1.2f, 14);
        adventure4Enemies[15] = new Adventure.Enemy("Skeleton Fighter", 120000, 2250, 900, 960, 960, 200, 200, 1f, 15);
        adventure4Enemies[16] = new Adventure.Enemy("Skeleton Guard", 200000, 2500, 1100, 1120, 1120, 300, 300, 0.8f, 16);
        adventure4Enemies[17] = new Adventure.Enemy("Skeleton Soldier", 320000, 3200, 1300, 1280, 1280, 500, 500, 0.6f, 17);
        adventure4Enemies[18] = new Adventure.Enemy("Skeleton Knight", 400000, 4000, 1600, 1440, 1440, 800, 800, 0.4f, 18);
        adventure4Enemies[19] = new Adventure.Enemy("Skeleton King", 800000, 5000, 2000, 1600, 1600, 1200, 1200, 0.2f, 19);



        adventures[0] = new Adventure("Goblin Camp", 5, adventure1Enemies, 1, 0);
        adventures[1] = new Adventure("Mushroom Farm", 10, adventure2Enemies, 2, 0);
        adventures[2] = new Adventure("Mystic Cave", 15, adventure3Enemies, 3, 0);
        adventures[3] = new Adventure("The Crypt", 20, adventure4Enemies, 4, 0);


        currentAdventure = adventures[tempAdventureNumber];
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
                if (Stage == currentAdventure.maxStages)
                {
                    currentAdventure.isCompleted = 1;
                    if (currentAdventure.isCompleted == 1 && currentAdventure.prevCompleted == 0)
                    {
                        confirmManager.ToggleShow(4);
                        currentAdventure.prevCompleted = 1;
                    }

                    saveManager.Save();
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
        yield return new WaitForSeconds(0.1f);  // Wait for 0.1 seconds (adjust as needed)
        isButtonPressed = false;
        Debug.Log("CoolDown");
    }

    public void SelectAdventure(int adventureIndex)
    {
        if (tempAdventureNumber != adventureIndex)
        {
            adventureConfirmMenus[0].Menu.SetActive(true);
            tempAdventureNumber = adventureIndex;
        }
    }

    public void ConfirmAdventure()
    {

        if (adventures[tempAdventureNumber] != currentAdventure)
        {
            currentAdventure = adventures[tempAdventureNumber];
            ResetEnemies();
            Stage = 1;
            progressBarTimer.SetStageAnimation();
            adventureConfirmMenus[0].Menu.SetActive(false);
            prestige.PrestigeHero();
        }
    }
    public void CancelConfirm()
    {
        adventureConfirmMenus[0].Menu.SetActive(false);
    }

    public void UpdateEnemyStatsText()
    {

        EnemyNameText.text = currentAdventure.enemies[Stage - 1].enemyName;
        enemyHpBar.value = currentAdventure.enemies[Stage - 1].enemyCurrentHp / currentAdventure.enemies[Stage - 1].enemyMaxHp;
        EnemyHpText.text = playerStats.FormatStatValue(currentAdventure.enemies[Stage - 1].enemyCurrentHp) + "/" + playerStats.FormatStatValue(currentAdventure.enemies[Stage - 1].enemyMaxHp);
        StageNumber.text = "Stage: " + Stage + "/" + currentAdventure.maxStages;
    }
}



