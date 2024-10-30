using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public PlayerStats playerStats;
    public PassiveBonus passiveBonus;

    [System.Serializable]
    public struct Upgrade
    {
        public bool unlocked;
        public bool purchased;
        public bool blocked;
        public int metalCount;

        public int attackBoost;
        public int defenseBoost;
        public int healthBoost;
        // Add more fields as needed for each upgrade’s specific boosts or effects.
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
        upgrades[1].defenseBoost = 5; // Example: Tier 1, Upgrade 2
        upgrades[2].healthBoost = 20; // Example: Tier 1, Upgrade 3
        //Tier 2
        upgrades[3].attackBoost = 10;
        upgrades[4].defenseBoost = 10;
        upgrades[5].healthBoost = 30;
        //Tier 3
        upgrades[6].attackBoost = 15;
        upgrades[7].defenseBoost = 15;
        upgrades[8].healthBoost = 40;
        //Tier 4
        upgrades[9].attackBoost = 20;
        upgrades[10].defenseBoost = 20;
        upgrades[11].healthBoost = 50;
        // Continue to set values as needed
    }

    public void UpgradeLocks()
    {
        // Tier 1 unlocks at level 5
        if (playerStats.level >= 2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }

        // Tier 2 unlocks at level 10
        if (playerStats.level >= 3)
        {
            for (int i = 3; i < 6; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }
        if (playerStats.level >= 4)
        {
            for (int i = 3; i < 9; i++)
            {
                if (!upgrades[i].blocked) upgrades[i].unlocked = true;
            }
        }
        if (playerStats.level >= 5)
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

            upgrades[index].metalCount += 1;

            // Apply the upgrade's effects
            playerStats.atk += upgrades[index].attackBoost;
            playerStats.def += upgrades[index].defenseBoost;
            playerStats.maxHp += upgrades[index].healthBoost;

            // Lock other upgrades in the same tier
            int tierStart = (index / 3) * 3; // 0–2 for tier 1, 3–5 for tier 2
            for (int i = tierStart; i < tierStart + 3; i++)
            {
                if (i != index)
                {
                    upgrades[i].metalCount += 1;
                    upgrades[i].blocked = true;
                    upgrades[i].unlocked = false;
                }
            }
            for (int i = 0; i < upgrades.Length; i++)
            {
                playerStats.atk += (float)(upgrades[i].metalCount * 0.3);
            }
            Debug.Log("Metal Count for Upgrade " + index + ": " + upgrades[index].metalCount);
        }
    }
}
