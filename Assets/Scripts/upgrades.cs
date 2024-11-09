using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public PlayerStats playerStats;
    public float atkPassiveMulti;
    public float defPassiveMulti;
    public float hpPassiveMulti;




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

        public float metalCount;

        public Slider metalSlider;

        public float metalMax;

        // Add more fields as needed for each upgrade’s specific boosts or effects.


    }


    public Upgrade[] upgrades = new Upgrade[12];

    void Start()
    {
        InitializeUpgrades();
        atkPassiveMulti = 0.02f;
        defPassiveMulti = 0.02f;
        hpPassiveMulti = 0.02f;

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
        upgrades[0].metalMax = 1000;

        upgrades[1].defenseBoost = 5; // Example: Tier 1, Upgrade 2
        upgrades[1].metalMax = 1000;

        upgrades[2].healthBoost = 20; // Example: Tier 1, Upgrade 3
        upgrades[2].metalMax = 1000;

        //Tier 2
        upgrades[3].attackBoost = 10;
        upgrades[3].metalMax = 2000;

        upgrades[4].defenseBoost = 10;
        upgrades[4].metalMax = 2000;

        upgrades[5].healthBoost = 30;
        upgrades[5].metalMax = 2000;

        //Tier 3
        upgrades[6].attackBoost = 15;
        upgrades[6].metalMax = 3000;

        upgrades[7].defenseBoost = 15;
        upgrades[7].metalMax = 3000;

        upgrades[8].healthBoost = 40;
        upgrades[8].metalMax = 3000;

        //Tier 4
        upgrades[9].attackBoost = 20;
        upgrades[9].metalMax = 4000;

        upgrades[10].defenseBoost = 20;
        upgrades[10].metalMax = 4000;

        upgrades[11].healthBoost = 50;
        upgrades[11].metalMax = 4000;

        // Continue to set values as needed
    }

    public void UpgradeLocks()
    {
        if (playerStats.level >= 5)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!upgrades[i].blocked && upgrades[i].metalCount < upgrades[i].metalMax)
                {
                    upgrades[i].unlocked = true;
                }

            }

        }
        if (playerStats.level >= 10)
        {
            for (int i = 3; i < 6; i++)
            {
                if (!upgrades[i].blocked && upgrades[i].metalCount < upgrades[i].metalMax) upgrades[i].unlocked = true;
            }
        }
        if (playerStats.level >= 15)
        {
            for (int i = 3; i < 9; i++)
            {
                if (!upgrades[i].blocked && upgrades[i].metalCount < upgrades[i].metalMax) upgrades[i].unlocked = true;
            }
        }
        if (playerStats.level >= 20)
        {
            for (int i = 3; i < 12; i++)
            {
                if (!upgrades[i].blocked && upgrades[i].metalCount < upgrades[i].metalMax) upgrades[i].unlocked = true;
            }
        }
    }

    public void PurchaseUpgrade(int index)
    {
        if (upgrades[index].unlocked && !upgrades[index].purchased)
        {
            UpdateMetalSliders(index);
            upgrades[index].purchased = true;

            // Apply the upgrade's effects
            playerStats.atk += upgrades[index].attackBoost;
            playerStats.def += upgrades[index].defenseBoost;
            playerStats.maxHp += upgrades[index].healthBoost;

            //sets the limits based on the tier

            if (upgrades[index].attackBoost > 0)
            {
                if (upgrades[index].metalCount < upgrades[index].metalMax)
                {
                    upgrades[index].metalCount++;
                    playerStats.atkMetalCount++;
                    UpdateMetalSliders(index);
                }
            }
            if (upgrades[index].defenseBoost > 0)
            {
                if (upgrades[index].metalCount < upgrades[index].metalMax)
                {
                    upgrades[index].metalCount++;
                    playerStats.defMetalCount++;
                    UpdateMetalSliders(index);
                }
            }
            if (upgrades[index].healthBoost > 0)
            {
                if (upgrades[index].metalCount < upgrades[index].metalMax)
                {
                    upgrades[index].metalCount++;
                    playerStats.hpMetalCount++;
                    UpdateMetalSliders(index);
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

    public void UpdateMetalSliders(int index)
    {
        if (upgrades[index].metalCount == 0)
        {
            upgrades[index].metalCount = 0;
        }
        else
        {
            upgrades[index].metalSlider.value = upgrades[index].metalCount / upgrades[index].metalMax;
        }
    }
}
