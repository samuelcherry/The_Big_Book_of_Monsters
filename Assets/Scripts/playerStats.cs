using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class PlayerStats : MonoBehaviour
{
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public ProgressBarTimer progressBarTimer;
    public Prestige prestige;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;


    public TMP_Text levelText;
    public TMP_Text xpText;

    public Slider xpBar;
    public Slider hpBar;


    public TMP_Text hpText;
    public TMP_Text atkText;
    public TMP_Text defText;
    public TMP_Text spdText;

    public int level;
    public float currentXp;
    public float maxXP;


    public float currentHp;
    public float maxHp;
    public float atk;
    public float def;

    public int atkMetalCount;
    public int defMetalCount;
    public int hpMetalCount;

    public int[] xpArr = new int[] { 300, 1000, 3000, 6500, 1400, 25000, 35000, 50000, 65000, 85000, 100000, 120000, 140000, 165000, 200000, 225000, 265000, 305000, 355000 };
    public int[] hpMaxArray = new int[] { 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1050 };
    public float[] atkArr = new float[] { 5, 10, 15, 20, 25, 30, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
    public int[] defArray = new int[] { 2, 2, 2, 2, 4, 4, 5, 5, 5, 5, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14 };







    void Start()
    {
        level = 1;
        currentXp = 0;

        saveManager.Load();

        maxXP = xpArr[level - 1];

        currentHp = 100;
        maxHp = hpMaxArray[level - 1] + slotUpgrades.slotOneAmtArr[slotUpgrades.slotOneLvl];
        atk = atkArr[level - 1] + slotUpgrades.slotTwoAmtArr[slotUpgrades.slotTwoLvl];
        def = defArray[level - 1] + slotUpgrades.slotThreeAmtArr[slotUpgrades.slotThreeLvl];

        hpBar.value = currentHp / maxHp;

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
        prestige.AddBaseXp();
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

    public void LevelUp()
    {
        if (level <= 20)
        {
            level += 1;
            currentXp = 0;
            atk = atkArr[level - 1] + slotUpgrades.slotTwoAmtArr[slotUpgrades.slotTwoLvl];
            maxXP = xpArr[level - 1];

            maxHp = hpMaxArray[level - 1] + slotUpgrades.slotOneAmtArr[slotUpgrades.slotOneLvl];
            currentHp = maxHp;
            hpBar.value = currentHp / maxHp;

            UpdateStatText();
            saveManager.Save();
        }
        else
        {
        }
    }

    public void PlayerTakeDamage()
    {

        if (currentHp > 0)  // Ensure HP is greater than 0
        {
            hpBar.value = currentHp / maxHp;
            if (enemyStats.EnemyAtk > def)
            {
                currentHp -= enemyStats.EnemyAtk - def;
            }
            else
            {
            }
        }

        else if (currentHp <= 0)
        {
            enemyStats.Stage -= 1;
            Reset();
            currentHp = maxHp;
            enemyStats.EnemyCurrentHp = enemyStats.EnemyMaxHp;
            hpBar.value = currentHp / maxHp;

            UpdateStatText();
            enemyStats.UpdateEnemyStatsText();

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

        hpBar.value = currentHp / maxHp;
    }
    public void Reset()
    {
        enemyStats.EnemyName = enemyStats.enemyNameArray[enemyStats.Stage - 1];
        enemyStats.EnemyMaxHp = enemyStats.enemyHpMax[enemyStats.Stage - 1];
        enemyStats.EnemyCurrentHp = enemyStats.EnemyMaxHp;
        enemyStats.EnemyDef = enemyStats.enemyDefArray[enemyStats.Stage - 1];
        enemyStats.baseXpRwd = enemyStats.xpRwdArray[enemyStats.Stage - 1];
        enemyStats.XpRwd = enemyStats.baseXpRwd * enemyStats.prestige.prestigeMulti;
        enemyStats.GoldRwd = enemyStats.goldRwdArray[enemyStats.Stage - 1] * enemyStats.prestige.prestigeMulti;
        enemyStats.EnemyAtk = enemyStats.enemyAtkArray[enemyStats.Stage - 1];
        enemyStats.enemyHpBar.value = enemyStats.EnemyCurrentHp / enemyStats.EnemyMaxHp;

        enemyStats.UpdateEnemyStatsText();
    }
}
