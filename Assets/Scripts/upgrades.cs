using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public PlayerStats playerStats;
    public SaveManager saveManager;
    public float atkPassiveMulti = 0.02f, defPassiveMulti = 0.02f, hpPassiveMulti = 0.02f;
    //CREATING ROLES
    [Serializable]
    public class Roles
    {
        public string roleName;
        public TMP_Text passiveAtkBonusText, passiveDefBonusText, passiveHpBonusText;
        public int roleUnlocked = 0;
        public List<Upgrade> upgrades = new();
        //SET OF UPGRADES INSIDE ROLES
        [Serializable]
        public class Upgrade
        {
            //declare variables
            public string upgradeName;
            public bool unlocked = false, purchased = false, blocked = false;
            public int attackBoost = 0, defenseBoost = 0, healthBoost = 0;
            public float metalCount, metalMax;
            public Button upgradeButton;
            public Slider skillPointSlider;
        }
    }

    public Roles[] roles;

    void Update()
    {
        UpgradeLocks();
    }
    public void UpgradeLocks()
    {
        // Loop through each role in the roles array
        for (int r = 0; r < roles.Length; r++)
        {
            // Loop through the upgrades for the current role
            for (int i = 0; i < roles[r].upgrades.Count; i++)
            {
                var upgrade = roles[r].upgrades[i]; // Get the current upgrade for the role
                // Check the player's level and unlock upgrades based on the conditions
                if (playerStats.level >= 5)
                {
                    // Unlock upgrades in the first tier (0-2)
                    if (i < 3 && !upgrade.blocked && upgrade.metalCount < upgrade.metalMax)
                    {
                        upgrade.unlocked = true;
                    }
                }
                if (playerStats.level >= 10)
                {
                    // Unlock upgrades in the second tier (3-5)
                    if (i >= 3 && i < 6 && !upgrade.blocked && upgrade.metalCount < upgrade.metalMax)
                    {
                        upgrade.unlocked = true;
                    }
                }
                if (playerStats.level >= 15)
                {
                    // Unlock upgrades in the third tier (6-8)
                    if (i >= 6 && i < 9 && !upgrade.blocked && upgrade.metalCount < upgrade.metalMax)
                    {
                        upgrade.unlocked = true;
                    }
                }
                if (playerStats.level >= 20)
                {
                    // Unlock upgrades in the fourth tier (9-11)
                    if (i >= 9 && i < 12 && !upgrade.blocked && upgrade.metalCount < upgrade.metalMax)
                    {
                        upgrade.unlocked = true;
                    }
                }
            }
        }
    }

    public void PurchaseUpgrade(int roleIndex, int upgradeIndex)
    {
        // Get the role and upgrade based on the passed indices
        var role = roles[roleIndex];
        var upgrade = role.upgrades[upgradeIndex];

        if (upgrade.unlocked && !upgrade.purchased)
        {
            upgrade.purchased = true;

            // Apply the upgrade's effects
            playerStats.atk += upgrade.attackBoost;
            playerStats.def += upgrade.defenseBoost;
            playerStats.maxHp += upgrade.healthBoost;

            upgrade.skillPointSlider.value = upgrade.metalCount / upgrade.metalMax;

            Debug.Log(playerStats.atk);

            // Set the limits based on the tier (metal count)
            if (upgrade.attackBoost > 0)
            {
                if (upgrade.metalCount < upgrade.metalMax)
                {
                    playerStats.atkMetalCount++;
                }
            }
            if (upgrade.defenseBoost > 0)
            {
                if (upgrade.metalCount < upgrade.metalMax)
                {
                    playerStats.defMetalCount++;
                }
            }
            if (upgrade.healthBoost > 0)
            {
                if (upgrade.metalCount < upgrade.metalMax)
                {
                    playerStats.hpMetalCount++;
                }
            }
            upgrade.metalCount++;

            // Lock other upgrades in the same tier
            int tierStart = upgradeIndex / 3 * 3; // 0–2 for tier 1, 3–5 for tier 2
            for (int i = tierStart; i < tierStart + 3; i++)
            {
                if (i != upgradeIndex)
                {
                    role.upgrades[i].blocked = true;
                    role.upgrades[i].unlocked = false;
                }
            }
        }
        saveManager.Save();
    }


}
