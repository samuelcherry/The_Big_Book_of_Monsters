using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sprites : MonoBehaviour
{
    public Upgrades upgrades; // Reference to the Upgrades script
    public PlayerStats playerStats;



    [System.Serializable]
    public class UpgradeButtonRoles
    {
        public int roleIndex;
        public Sprite lockedSprite;
        public Sprite unlockedSprite;
        public Button RoleSelectButton;
        public UpgradeButtonSprite[] upgradeButtonSprites = new UpgradeButtonSprite[12];
    }

    [System.Serializable]
    public class UpgradeButtonSprite
    {
        public Sprite lockedSprite;
        public Sprite unlockedSprite;
        public Sprite purchasedSprite;
        public int upgradeIndex;
        public Button upgradeButtons;
    }
    public UpgradeButtonRoles[] upgradeButtonRoles = new UpgradeButtonRoles[4];

    void Update()
    {
        for (int r = 0; r < upgradeButtonRoles.Length; r++)
        {
            for (int i = 0; i < upgradeButtonRoles[r].upgradeButtonSprites.Length; i++)
            {
                UpdateSprites(r, i);
            }
        }

    }

    // Update each button's sprite based on the upgrade's state
    public void UpdateSprites(int roleIndex, int upgradeIndex)
    {
        var upgrade = upgrades.roles[roleIndex].upgrades[upgradeIndex];
        var upgradeButton = upgradeButtonRoles[roleIndex].upgradeButtonSprites[upgradeIndex];

        //If the role is unlocked show the unlocked sprite otherwise show the locked sprite
        if (upgrades.roles[roleIndex].roleUnlocked == true)
        {
            upgradeButtonRoles[roleIndex].RoleSelectButton.image.sprite = upgradeButtonRoles[roleIndex].unlockedSprite;
        }
        else
        {
            upgradeButtonRoles[roleIndex].RoleSelectButton.image.sprite = upgradeButtonRoles[roleIndex].lockedSprite;
        }


        if (upgrade.metalCount == upgrade.metalMax)
        {
            upgradeButton.upgradeButtons.image.sprite = upgradeButton.purchasedSprite;
        }


        else if (!upgrade.unlocked && !upgrade.purchased)
        {
            upgradeButton.upgradeButtons.image.sprite = upgradeButton.lockedSprite;
        }
        else if (upgrade.unlocked && !upgrade.purchased)
        {
            upgradeButton.upgradeButtons.image.sprite = upgradeButton.unlockedSprite;
        }
        else if (upgrade.purchased)
        {
            upgradeButton.upgradeButtons.image.sprite = upgradeButton.purchasedSprite;
        }


    }
}


