using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class PlayerStats : MonoBehaviour
{
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public ProgressBarTimer progressBarTimer;
    public TMP_Text levelText;
    public TMP_Text xpText;
    public TMP_Text upgradeCostText;
    public Slider xpBar;
    public Slider hpBar;


    public TMP_Text hpText;
    public TMP_Text atkText;
    public TMP_Text defText;
    public TMP_Text spdText;

    public int level;
    public float currentXp;
    public float maxXP;
    public int upgradeLvl = 0;
    public float xpPercent;

    public float currentHp;
    public float maxHp;
    public int atk;
    public int def;

    readonly int[] xpArr = new int[] { 300, 1000, 3000, 6500, 1400, 25000, 35000, 50000, 65000, 85000, 100000, 120000, 140000, 165000, 200000, 225000, 265000, 305000, 355000 };
    readonly int[] hpMaxArray = new int[] { 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050 };
    readonly int[] atkArr = new int[] { 5, 10, 15, 20, 25, 30, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
    readonly int[] defArray = new int[] { 2, 2, 2, 2, 4, 4, 5, 5, 5, 5, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14 };
    readonly int[] upgradeCostArr = new int[] { 100, 1000, 10000, 100000 };
    readonly int[] upgradeAmtArr = new int[] { 0, 10, 30, 50, 100 };


    void Start()
    {
        level = 1;
        currentXp = 0;

        saveManager.Load();

        maxXP = xpArr[level - 1];

        currentHp = 100;
        maxHp = hpMaxArray[level - 1];
        atk = atkArr[level - 1] + upgradeAmtArr[upgradeLvl];
        def = defArray[level - 1];

        hpBar.value = currentHp / maxHp;


        saveManager.Load();
        UpdateStatText();
        progressBarTimer.UpdateSpdText();
    }
    private void Update()
    {
        UpdateStatText();
        progressBarTimer.UpdateSpdText();
    }
    public void AddXp()
    {
        currentXp += enemyStats.XpRwd;
        UpdateStatText();
        if (currentXp == 0)
        {
            xpBar.value = 0;
        }
        else
        {
            xpBar.value = currentXp / maxXP;
        }

        if (currentXp >= maxXP)
        {
            LevelUp();
        }
    }

    public void SubstractXp()
    {
        currentXp -= 10;
    }

    public void LevelUp()
    {
        if (level <= 20)
        {
            level += 1;
            currentXp = 0;
            atk = atkArr[level - 1] + upgradeAmtArr[upgradeLvl];
            maxXP = xpArr[level - 1];

            maxHp = hpMaxArray[level - 1];
            currentHp = maxHp;
            hpBar.value = currentHp / maxHp;

            UpdateStatText();
            saveManager.Save();
        }
        else
        {
            level = 20;
            currentXp = 0;
            maxXP = 0;

            maxHp = hpMaxArray[level - 1];
            currentHp = maxHp;
            hpBar.value = currentHp / maxHp;

            UpdateStatText();
            saveManager.Save();

        }
    }

    public void PlayerTakeDamage()
    {
        if (currentHp > 0)  // Ensure HP is greater than 0
        {
            hpBar.value = currentHp / maxHp;
            currentHp -= enemyStats.EnemyAtk - def;
            enemyStats.UpdateEnemyStatsText();
            UpdateStatText();
            if (currentHp <= 0)
            {
                enemyStats.Stage -= 1;
                currentHp = maxHp;
                enemyStats.EnemyCurrentHp = enemyStats.EnemyMaxHp;

                hpBar.value = currentHp / maxHp;

                UpdateStatText();
                enemyStats.UpdateEnemyStatsText();
            }
        }
        else
        {
            hpBar.value = currentHp / maxHp;
        }
    }

    public void AddGold()
    {
        enemyStats.GoldAmt += enemyStats.GoldRwd;
    }

    public void UpdateStatText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHp + "/" + maxHp;
        }
        if (atkText != null)
        {
            atkText.text = "Atk: " + atk;
        }
        if (defText != null)
        {
            defText.text = "Def: " + def;
        }
        if (levelText != null)
        {
            levelText.text = "Lvl: " + level;
        }
        if (xpText != null)
        {
            maxXP = xpArr[level - 1];
            xpText.text = "XP: " + currentXp + "/" + maxXP;
        }
        if (enemyStats.GoldAmtText != null)
        {
            enemyStats.GoldAmtText.text = "Gold: " + enemyStats.GoldAmt;
        }
        if (upgradeCostText != null)
        {
            upgradeCostText.text = "Cost: " + upgradeCostArr[upgradeLvl].ToString();
        }

        hpBar.value = currentHp / maxHp;
    }


    public void Upgrade()
    {
        if (enemyStats.GoldAmt >= upgradeCostArr[upgradeLvl])
        {
            enemyStats.GoldAmt -= upgradeCostArr[upgradeLvl];
            upgradeLvl += 1;
            atk += upgradeAmtArr[upgradeLvl];
            UpdateStatText();
            saveManager.Save();
        }
        else
        {
        }
    }

}
