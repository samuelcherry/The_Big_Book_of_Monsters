using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerStats playerStats;

/// <summary>
/// The passive is being multiplied by the atk boost of each upgrade, so if you don't select that upgrade you don't get the passive on the next run. What I'd like is for the
/// passive boost to be multiplied by the metal count regardless of the upgrades.
/// so passiveBonus = atkMetalCount * atkPassiveMulti
/// and when a purchase is made, add to that respective metal count.
/// we can keep the second variable in the upgrades because I want to be able to change how many metals each upgrade gives or at least increase the bonus for higher tier upgrades
/// </summary>
    public float atkPassiveMulti = 0.01f;
    public float defPassiveMulti = 0.01f;
    public float hpPassiveMulti = 0.01f;

    [System.Serializable]
    public struct Upgrade
    {
        public PlayerStats playerStats;
        public bool unlocked;
        public bool purchased;
        public bool blocked;

        public int attackBoost;
        public int defenseBoost;
        public int healthBoost;

        public float atkPassiveBonus;
        public float defPassiveBonus;
        public float hpPassiveBonus;

        // Add more fields as needed for each upgrade’s specific boosts or effects.
   
    public float GetAtkPassiveBonus()
    {
        return playerStats.atkMetalCount * atkPassiveBonus;
    }
        public float GetDefPassiveBonus()
    {
        return playerStats.defMetalCount * defPassiveBonus;
    }
        public float GetHpPassiveBonus()
    {
        return playerStats.hpMetalCount * hpPassiveBonus;
    }
   
    }


    public Upgrade[] upgrades = new Upgrade[12];

    void Start()
    {
        InitializeUpgrades();

    }

    void Update()
    {
        UpgradeLocks();
    }

    // Initialize each upgrade's properties (e.g., attackBoost, defenseBoost)
    private void InitializeUpgrades()
    {
        // Set default values for each upgrade here or in the Inspector.
        upgrades[0].attackBoost = 5; // Example: Tier 1, Upgrade 1
        upgrades[0].atkPassiveBonus = 1f;

        
        upgrades[1].defenseBoost = 5; // Example: Tier 1, Upgrade 2
        upgrades[1].defPassiveBonus = 1f;
        
        upgrades[2].healthBoost = 20; // Example: Tier 1, Upgrade 3
        upgrades[2].hpPassiveBonus = 1f;
        
        //Tier 2
        upgrades[3].attackBoost = 10;
        upgrades[3].atkPassiveBonus = 1f;

        upgrades[4].defenseBoost = 10;
        upgrades[4].defPassiveBonus = 1f;

        upgrades[5].healthBoost = 30;
        upgrades[5].hpPassiveBonus = 1f;
        //Tier 3
        upgrades[6].attackBoost = 15;
        upgrades[6].atkPassiveBonus = 1f;

        upgrades[7].defenseBoost = 15;
        upgrades[7].defPassiveBonus = 1f;

        upgrades[8].healthBoost = 40;
        upgrades[8].hpPassiveBonus = 1f;
        //Tier 4
        upgrades[9].attackBoost = 20;
        upgrades[9].atkPassiveBonus = 1f;

        upgrades[10].defenseBoost = 20;
        upgrades[10].defPassiveBonus = 1f;

        upgrades[11].healthBoost = 50;
        upgrades[11].hpPassiveBonus = 1f;
        // Continue to set values as needed
    }

    public void UpgradeLocks()
    {
        // Tier 1 unlocks at level 5
        if (playerStats.level >= 5)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }

        // Tier 2 unlocks at level 10
        if (playerStats.level >= 10)
        {
            for (int i = 3; i < 6; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }
        if (playerStats.level >= 15)
        {
            for (int i = 3; i < 9; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }
        if (playerStats.level >= 20)
        {
            for (int i = 3; i < 12; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }
    }

    public void PurchaseUpgrade(int index)
    {
        if (upgrades[index].unlocked && !upgrades[index].purchased)
        {
            upgrades[index].purchased = true;

            // Apply the upgrade's effects
            playerStats.atk += upgrades[index].attackBoost;
            playerStats.def += upgrades[index].defenseBoost;
            playerStats.maxHp += upgrades[index].healthBoost;
            

            if (upgrades[index].attackBoost > 0)
            {
                if(playerStats.atkMetalCount < 10)
                {
                    playerStats.atkMetalCount++;
                }
            }
             if (upgrades[index].defenseBoost > 0)
            {
                if(playerStats.defMetalCount < 10)
                {
                    playerStats.defMetalCount++;
                }
            }
             if (upgrades[index].healthBoost > 0)
            {
                if(playerStats.hpMetalCount < 10)
                {
                    playerStats.hpMetalCount++;
                }
            }

            // Lock other upgrades in the same tier
            int tierStart = index / 3 * 3; // 0–2 for tier 1, 3–5 for tier 2
            for (int i = tierStart; i < tierStart + 3; i++)
            {
                if (i != index)
                {
                    upgrades[i].blocked = true;
                    upgrades[i].unlocked = false;
                }
            }
        }
    }
}
