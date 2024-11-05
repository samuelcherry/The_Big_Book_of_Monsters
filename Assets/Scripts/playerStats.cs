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
    public TMP_Text passiveAtkBonusText;
    public TMP_Text passiveDefBonusText;
    public TMP_Text passiveHpBonusText;

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

    public int[] xpArr;
    public int[] hpMaxArray;
    public float[] atkArr;
    public int[] defArray;

    // Arrays removed from field declarations

    void Start()
    {
        // Initializing arrays in Start to avoid inspector interference
        xpArr = new int[] { 300, 1000, 3000, 6500, 14000, 25000, 35000, 50000, 65000, 85000, 100000, 120000, 140000, 165000, 200000, 225000, 265000, 305000, 355000 };
        hpMaxArray = new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        atkArr = new float[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        defArray = new int[] { 2, 0, 0, 0, 2, 0, 1, 0, 0, 0, 3, 0, 0, 2, 0, 0, 2, 0, 0, 2 };

        level = 1;
        currentXp = 0;
        atkMetalCount = 0;
        defMetalCount = 0;
        hpMetalCount = 0;
        currentHp = 100;

        saveManager.Load();
        if (level < 20)
        {
            maxXP = xpArr[level - 1];
        }
        for (int i = 0; i < slotUpgrades.slotStructs.Length; i++)
        {
            maxHp = hpMaxArray[level - 1] + slotUpgrades.slotStructs[0].slotAmtArr[slotUpgrades.slotStructs[0].slotLvl];
            atk = atkArr[level - 1] + slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl];
            def = defArray[level - 1] + slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl];
        }

        atk *= atkMetalCount * upgrades.atkPassiveMulti + 1;
        def *= defMetalCount * upgrades.defPassiveMulti + 1;
        maxHp *= hpMetalCount * upgrades.hpPassiveMulti + 1;

        hpBar.value = currentHp / maxHp;

        UpdateStatText();
        progressBarTimer.UpdateSpdText();
    }
    private void Update()
    {
        UpdateStatText();
        progressBarTimer.UpdateSpdText();
    }

    // A generic method to format any stat value based on its range
    public string FormatStatValue(float value)
    {
        if (value >= 1000)
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

    public void AddGold() //Adds Gold when an enemy is killed
    {
        enemyStats.GoldAmt += enemyStats.GoldRwd;
    }
    public void AddXp() //Adding XP and triggering Level up function
    {
        if (level < 20)
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
        else
        {
            currentXp = 0; // Prevent XP overflow at max level

            currentHp = maxHp;
            enemyStats.EnemyCurrentHp = enemyStats.EnemyMaxHp;

            hpBar.value = currentHp / maxHp;
            enemyStats.enemyHpBar.value = enemyStats.EnemyCurrentHp / enemyStats.EnemyMaxHp;
            UpdateStatText();
            enemyStats.UpdateEnemyStatsText();
            prestige.AddBaseXp();
        }
    }

    public void LevelUp()
    {
        if (level < 19) // Normal level-up logic up to level 20
        {
            level += 1;
            currentXp = 0;
            maxXP = xpArr[level - 1];

            // Apply level-based increases to stats
            float atkIncrease = atkArr[level - 1];
            float defIncrease = defArray[level - 1];
            float hpIncrease = hpMaxArray[level - 1];

            atk += atkIncrease;
            def += defIncrease;
            maxHp += hpIncrease;

            // Apply slot upgrades and passive bonuses
            atk += slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl] * (atkMetalCount * upgrades.atkPassiveMulti);
            def += slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl] * (defMetalCount * upgrades.defPassiveMulti);
            maxHp += slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl] * (hpMetalCount * upgrades.hpPassiveMulti);

            currentHp = maxHp;
            hpBar.value = currentHp / maxHp;
            UpdateStatText();
            saveManager.Save();
            Debug.Log($"Level Up! New Level: {level}, atk: {atk}, def: {def}, maxHp: {maxHp}");
        }
        else if (level == 19) // Handle max level case
        {
            level += 1;
            currentXp = 0;

            // Apply level-based increases to stats
            float atkIncrease = atkArr[level - 1];
            float defIncrease = defArray[level - 1];
            float hpIncrease = hpMaxArray[level - 1];

            atk += atkIncrease;
            def += defIncrease;
            maxHp += hpIncrease;

            // Apply slot upgrades and passive bonuses
            atk += slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl] * (atkMetalCount * upgrades.atkPassiveMulti);
            def += slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl] * (defMetalCount * upgrades.defPassiveMulti);
            maxHp += slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl] * (hpMetalCount * upgrades.hpPassiveMulti);

            currentHp = maxHp;
            hpBar.value = currentHp / maxHp;
            UpdateStatText();
            saveManager.Save();
            Debug.Log($"Level Up! New Level: {level}, atk: {atk}, def: {def}, maxHp: {maxHp}");
        }
    }

    public void ResetPlayerHealth()
    {
        currentHp = maxHp;
        hpBar.value = currentHp / maxHp;
        UpdateStatText();
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



    public void UpdateStatText() //Checks for new values and updates text fields accordingly
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + FormatStatValue(currentHp) + "/" + FormatStatValue(maxHp);
        }
        if (atkText != null)
        {
            atkText.text = "Atk: " + FormatStatValue(atk);
        }
        if (defText != null)
        {
            defText.text = "Def: " + FormatStatValue(def);
        }
        if (levelText != null)
        {
            levelText.text = "Lvl: " + level;
        }
        if (xpText != null)
        {
            if (level < 20)
            {
                maxXP = xpArr[level - 1];
                xpText.text = "XP: " + FormatStatValue(currentXp) + "/" + FormatStatValue(maxXP);
            }
            else
            {
                xpText.text = "XP: MAX";
            }
        }
        if (enemyStats.GoldAmtText != null)
        {
            enemyStats.GoldAmtText.text = "Gold: " + FormatStatValue(enemyStats.GoldAmt);
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
