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

        maxXP = xpArr[level - 1];
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
            prestige.AddBaseXp();
        }
    }

    public void LevelUp() // leveling up
    {
        if (level < 20) // Check to ensure level does not exceed max level
        {
            // Increment the level
            level += 1;
            currentXp = 0;

            // Increase maxXP based on the new level
            maxXP = xpArr[level - 1];

            // Accumulate level-based increase to atk and def
            float atkIncrease = atkArr[level - 1];
            float defIncrease = defArray[level - 1];

            // Apply the new level-based increases to the current stats
            atk += atkIncrease;
            def += defIncrease;

            // Adjust atk and def based on slot upgrades and passive bonuses
            atk += slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl] * (atkMetalCount * upgrades.atkPassiveMulti);
            def += slotUpgrades.slotStructs[2].slotAmtArr[slotUpgrades.slotStructs[2].slotLvl] * (defMetalCount * upgrades.defPassiveMulti);

            // Apply a level-based HP increase
            float hpIncrease = hpMaxArray[level - 1];
            maxHp += hpIncrease;

            // Adjust maxHp based on slot upgrades and passive bonuses
            maxHp += slotUpgrades.slotStructs[1].slotAmtArr[slotUpgrades.slotStructs[1].slotLvl] * (hpMetalCount * upgrades.hpPassiveMulti);

            // Set current HP to the new max HP
            currentHp = maxHp;

            // Update the UI elements
            hpBar.value = currentHp / maxHp;
            UpdateStatText();

            // Save the updated stats
            saveManager.Save();

            Debug.Log($"Level Up! New Level: {level}, atk: {atk}, def: {def}, maxHp: {maxHp}");
        }
        else
        {
            Debug.Log("Maximum level reached.");
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



    public void UpdateStatText() //Checks for new values and updates text fields accordingly
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
            if (level < 20)
            {
                maxXP = xpArr[level - 1];
                xpText.text = "XP: " + currentXp + "/" + maxXP;
            }
            else
            {
                xpText.text = "XP: MAX";
            }
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
