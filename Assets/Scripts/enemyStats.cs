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
    public EnemyObjects enemyObjects;

    public int Stage;
    public string EnemyName;
    public float EnemyCurrentHp;
    public float EnemyMaxHp;
    public float EnemyAtk;
    public float EnemyDef;

    public float GoldRwd;
    public float baseXpRwd;
    public float XpRwd;
    public float GoldAmt;

    void Start()
    {
        // Initialize arrays inside the Start function
        saveManager.Load();

        Stage = 1;
        EnemyName = enemyObjects.enemies[Stage - 1].enemyName;
        EnemyMaxHp = enemyObjects.enemies[Stage - 1].enemyMaxHp;
        EnemyCurrentHp = EnemyMaxHp;
        EnemyDef = enemyObjects.enemies[Stage - 1].enemyDef;
        baseXpRwd = enemyObjects.enemies[Stage - 1].xpRwd;
        XpRwd = baseXpRwd * prestige.prestigeMulti;
        GoldRwd = enemyObjects.enemies[Stage - 1].goldRwd * prestige.prestigeMulti;
        EnemyAtk = enemyObjects.enemies[Stage - 1].enemyAtk;
        GoldAmt = 0;
        enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
        UpdateEnemyStatsText();
    }

    private void Update()
    {
        UpdateEnemyStatsText();
        baseXpRwd = enemyObjects.enemies[Stage - 1].xpRwd;
        XpRwd = baseXpRwd * prestige.prestigeMulti;
        GoldRwd = enemyObjects.enemies[Stage - 1].goldRwd * prestige.prestigeMulti;
        enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
        EnemyAtk = enemyObjects.enemies[Stage - 1].enemyAtk;
    }

    public void TakeDamage()
    {
        if (EnemyCurrentHp > 0) // Check if enemy HP is above zero before taking damage
        {
            if (playerStats.atk > EnemyDef)
            {
                // Calculate the damage and ensure it doesn't reduce HP below zero
                EnemyCurrentHp = Mathf.Max(0, EnemyCurrentHp - (playerStats.atk - EnemyDef));
                enemyHpBar.value = EnemyCurrentHp / EnemyMaxHp;
                UpdateEnemyStatsText();

                // Check if enemy has been defeated
                if (EnemyCurrentHp == 0)
                {
                    // Grant rewards to player
                    playerStats?.AddGold();
                    playerStats?.AddXp();

                    // Reset enemy HP for the next encounter
                    EnemyCurrentHp = EnemyMaxHp;
                    playerStats.currentHp = playerStats.maxHp;
                    UpdateEnemyStatsText();
                }
            }
            else
            {
                Debug.Log("Player's attack is not strong enough to damage the enemy.");
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
            EnemyName = enemyObjects.enemies[Stage - 1].enemyName;
            EnemyMaxHp = enemyObjects.enemies[Stage - 1].enemyMaxHp;
            EnemyCurrentHp = EnemyMaxHp;
            EnemyDef = enemyObjects.enemies[Stage - 1].enemyDef;
            GoldRwd = enemyObjects.enemies[Stage - 1].goldRwd;

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
            EnemyName = enemyObjects.enemies[Stage - 1].enemyName;
            EnemyMaxHp = enemyObjects.enemies[Stage - 1].enemyMaxHp;
            EnemyCurrentHp = EnemyMaxHp;
            EnemyDef = enemyObjects.enemies[Stage - 1].enemyDef;
            GoldRwd = enemyObjects.enemies[Stage - 1].goldRwd;

            playerStats.currentHp = playerStats.maxHp;
            UpdateEnemyStatsText();
            playerStats.UpdateStatText();
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
            GoldAmtText.text = "Gold : " + playerStats.FormatStatValue(GoldAmt);
        }
        if (EnemyHpText != null)
        {
            EnemyHpText.text = playerStats.FormatStatValue(EnemyCurrentHp) + "/" + playerStats.FormatStatValue(EnemyMaxHp);
        }
        if (StageNumber != null)
        {
            StageNumber.text = "Stage: " + Stage + "/20";
        }

    }
}


