using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public ProgressBarTimer progressBarTimer;
    public Prestige prestige;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;

    public TMP_Text levelText, xpText, hpText, atkText, defText, spdText;
    public Slider xpBar, hpBar;

    public int level, atkMetalCount, defMetalCount, hpMetalCount, role;
    public float currentXp, maxXP, currentHp, maxHp, atk, def;

    public int[] xpArray;
    public int[] hpMaxArray;
    public float[] atkArray;
    public int[] defArray;
    public float[] speedArray;

    // Arrays removed from field declarations

    void Awake()
    {

        level = 1;
        currentXp = 0;

        atkMetalCount = 0;
        defMetalCount = 0;
        hpMetalCount = 0;
        xpArray = new int[] { 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200, 102400, 204800, 409600, 820000, 1640000, 3275000, 6500000, 13000000, 26000000, 52500000 };
        hpMaxArray = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000 };
        atkArray = new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
        defArray = new int[] { 2, 2, 2, 2, 4, 4, 5, 5, 5, 5, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14 };
        speedArray = new float[] { 4F, 3.8F, 3.6F, 3.4F, 3.2F, 3F, 2.8F, 2.6F, 2.4F, 2.2F, 2F, 1.8F, 1.6F, 1.4F, 1.2F, 1F, 0.8F, 0.6F, 0.4F, 0.2F };


        if (level < 20)
        {
            maxXP = xpArray[level - 1];
        }
        currentHp = maxHp;
    }

    void Start()
    {
        saveManager.Load();
        UpdateStats();
        UpdateStatText();
    }



    // A generic method to format any stat value based on its range
    public string FormatStatValue(float value)
    {
        if (value >= 100000)
        {
            return value.ToString("0.##E+0"); // Scientific notation for values 100,000 and above
        }
        else if (value >= 1000)
        {
            return value.ToString("F0"); // No decimal places for values 1000 and above
        }
        else if (value >= 100)
        {
            return value.ToString("F1"); // One decimal place for values in the hundreds
        }
        else
        {
            return value.ToString("F2"); // Two decimal places for values below 100
        }
    }
    //AFTER KILLING AN ENEMY
    public void AddGold() //Adds Gold when an enemy is killed
    {
        var currentEnemy = enemyStats.currentAdventure.enemies[enemyStats.Stage - 1];

        enemyStats.GoldAmt += currentEnemy.goldRwd;
        UpdateStats();
        Debug.Log("Expected Value: " + currentEnemy.baseGoldRwd * prestige.prestigeMulti);
        Debug.Log("Actual Value: " + currentEnemy.goldRwd);
    }
    public void AddXp() //Adding XP and triggering Level up function
    {
        if (level < 20)
        {
            currentXp += enemyStats.currentAdventure.enemies[enemyStats.Stage - 1].xpRwd;
            prestige.AddBaseXp();

            if (currentXp == 0)
            {
                xpBar.value = 0;
            }
            //RESET Xp bar on update
            xpBar.value = currentXp / maxXP;

            if (currentXp >= maxXP)
            {
                LevelUp();
            }
        }
        else
        {
            currentXp = 0; // Prevent XP overflow at max level
            if (currentXp >= maxXP)
            {
                currentXp = maxXP;
            }
            prestige.AddBaseXp();
        }
        UpdateStatText();
    }

    public void LevelUp()
    {
        if (level <= 19) // Normal level-up logic up to level 20
        {
            level += 1;
            currentXp = 0;
            xpBar.value = currentXp / maxXP;
            progressBarTimer.playerAtkTime = speedArray[level - 1];
            UpdateStats();
            saveManager.Save();
        }
        else
        {
            level = 20;
            currentXp = 0;
            xpBar.value = currentXp / maxXP;
            progressBarTimer.playerAtkTime = speedArray[level - 1];
            UpdateStats();
            saveManager.Save();
        }

        UpdateStatText();
    }

    public void PlayerTakeDamage()
    {
        if (currentHp > 0)  // Ensure HP is greater than 0
        {
            if (enemyStats.currentAdventure.enemies[enemyStats.Stage - 1].enemyAtk > def)
            {
                currentHp -= enemyStats.currentAdventure.enemies[enemyStats.Stage - 1].enemyAtk - def;
            }
        }

        else if (currentHp <= 0)
        {
            enemyStats.DecreaseStage();
            Reset();
        }
        UpdateStatText();
    }


    public void UpdateStats()
    {
        var slotUpgrade = slotUpgrades.slotStructs;

        maxHp = hpMaxArray[level - 1];
        maxHp += slotUpgrade[0].slotAmtArr[slotUpgrade[0].slotLvl];
        maxHp += hpMaxArray[level - 1] * (hpMetalCount * upgrades.hpPassiveMulti);

        currentHp = maxHp;
        atk = atkArray[level - 1];
        atk += slotUpgrade[1].slotAmtArr[slotUpgrade[1].slotLvl];
        atk += atkArray[level - 1] * (atkMetalCount * upgrades.atkPassiveMulti);

        def = defArray[level - 1];
        def += slotUpgrade[2].slotAmtArr[slotUpgrade[2].slotLvl];
        def += defArray[level - 1] * (defMetalCount * upgrades.defPassiveMulti);

        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgrades.roles[r].upgrades.Count; i++)
            {
                if (upgrades.roles[r].upgrades[i].purchased)
                {
                    atk += upgrades.roles[r].upgrades[i].attackBoost;
                    def += upgrades.roles[r].upgrades[i].defenseBoost;
                    maxHp += upgrades.roles[r].upgrades[i].healthBoost;
                }
            }
        }

        UpdateStatText();
    }


    public void UpdateStatText() //Checks for new values and updates text fields accordingly
    {
        UpdateHpText();
        UpdateAtkText();
        UpdateDefText();
        UpdateLevelText();
        UpdateGoldText();
        progressBarTimer.UpdateSpdText();
    }

    public void UpdateHpText()
    {
        hpText.text = "HP: " + FormatStatValue(currentHp) + "/" + FormatStatValue(maxHp);

        if (!float.IsNaN(currentHp) && !float.IsNaN(maxHp) && maxHp > 0)
        {
            hpBar.value = currentHp / maxHp;
        }
    }
    public void UpdateAtkText()
    {
        atkText.text = "Atk: " + FormatStatValue(atk);
    }
    public void UpdateDefText()
    {
        defText.text = "Def: " + FormatStatValue(def);
    }

    public void UpdateSpeedText()
    {

    }


    public void UpdateLevelText()
    {
        if (level < 20)
        {
            maxXP = xpArray[level - 1];
            xpText.text = "XP: " + FormatStatValue(currentXp) + "/" + FormatStatValue(maxXP);
        }
        else
        {
            xpText.text = "XP: MAX";
        }
        levelText.text = "Lvl: " + level;
    }
    public void UpdateGoldText()
    {
        enemyStats.GoldAmtText.text = "Gold: " + FormatStatValue(enemyStats.GoldAmt);
    }

    public void Reset()
    {
        currentHp = maxHp;
        UpdateHpText();

        var currentEnemy = enemyStats.currentAdventure.enemies[enemyStats.Stage - 1];

        currentEnemy.enemyCurrentHp = currentEnemy.enemyMaxHp;
        currentEnemy.xpRwd = currentEnemy.baseXpRwd * prestige.prestigeMulti;
        currentEnemy.goldRwd = currentEnemy.baseGoldRwd * prestige.prestigeMulti;

        enemyStats.enemyHpBar.value = currentEnemy.enemyCurrentHp / currentEnemy.enemyMaxHp;

        enemyStats.UpdateEnemyStatsText();
    }
}
