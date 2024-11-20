
using TMPro;
using UnityEngine;

public class Prestige : MonoBehaviour
{
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
    public Upgrades.Roles.Upgrade upgrade;
    public ProgressBarTimer progressBarTimer;
    public AlchemyTimers alchemyTimers;
    public ResetManager resetManager;


    public float baseXP;
    public float prestigeMulti;
    public TMP_Text currentMultiText, futureMultiText, roleSelectPrestigePoints;

    void Awake()
    {
        if (PlayerPrefs.GetFloat("PrestigeMulti") >= 1)
        {
            prestigeMulti = PlayerPrefs.GetFloat("PrestigeMulti");
        }
        else
        {
            prestigeMulti = 1;
        }
    }

    void Update()
    {
        UpdatePrestigeText();
    }
    public void AddBaseXp()
    {
        baseXP += enemyStats.currentAdventure.enemies[enemyStats.Stage - 1].xpRwd / prestigeMulti;
    }

    public void PrestigeHero()
    {
        prestigeMulti += baseXP / 10000;
        resetManager.SoftReset();
    }


    public void UpdatePrestigeText()
    {

        if (currentMultiText != null)
        {
            currentMultiText.text = playerStats.FormatStatValue(prestigeMulti).ToString();
        }
        if (futureMultiText != null)
        {
            futureMultiText.text = playerStats.FormatStatValue(prestigeMulti + baseXP / 10000).ToString();
        }
        if (roleSelectPrestigePoints != null)
        {
            roleSelectPrestigePoints.text = "Prestige Points: " + playerStats.FormatStatValue(prestigeMulti).ToString();
        }


    }
    public void UpdatePostPrestigeText()
    {
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            if (upgrades.roles[r].passiveAtkBonusText != null)
            {
                upgrades.roles[r].passiveAtkBonusText.text = "Passive ATK Bonus: \n" + playerStats.atkMetalCount * upgrades.atkPassiveMulti;
            }
            if (upgrades.roles[r].passiveDefBonusText != null)
            {
                upgrades.roles[r].passiveDefBonusText.text = "Passive DEF Bonus: \n" + playerStats.defMetalCount * upgrades.defPassiveMulti;
            }
            if (upgrades.roles[r].passiveHpBonusText != null)
            {
                upgrades.roles[r].passiveHpBonusText.text = "Passive HP Bonus: \n" + playerStats.hpMetalCount * upgrades.hpPassiveMulti;
            }
        }
    }



}