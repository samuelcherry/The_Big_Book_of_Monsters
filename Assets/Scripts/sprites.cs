using UnityEngine;
using UnityEngine.UI;

public class Sprites : MonoBehaviour
{
    public Upgrades upgrades; // Reference to the Upgrades script

    [System.Serializable]
    public struct SpriteSet
    {
        public Sprite lockedSprite;
        public Sprite unlockedSprite;
        public Sprite purchasedSprite;
    }

    public SpriteSet[] upgradeSprites; // Array of SpriteSets for each upgrade
    public Image[] upgradeButtons; // Array for each button’s Image component

    void Update()
    {
        UpdateSprites();
    }

    // Update each button's sprite based on the upgrade's state
    private void UpdateSprites()
    {
        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                if (i >= upgrades.roles[r].upgrades.Length) break;

                var upgrade = upgrades.roles[r].upgrades[i];
                var spriteSet = upgradeSprites[i];


                if (upgrade.metalCount == upgrade.metalMax)
                {
                    upgradeButtons[i].sprite = spriteSet.purchasedSprite;
                }


                else if (!upgrade.unlocked && !upgrade.purchased)
                {
                    upgradeButtons[i].sprite = spriteSet.lockedSprite;
                }
                else if (upgrade.unlocked && !upgrade.purchased)
                {
                    upgradeButtons[i].sprite = spriteSet.unlockedSprite;
                }
                else if (upgrade.purchased)
                {
                    upgradeButtons[i].sprite = spriteSet.purchasedSprite;
                }


            }
        }
    }
}
