using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyTimers : MonoBehaviour
{

    public Prestige prestige;
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public SaveManager saveManager;
    public Inventory playerInventory;
    public Sprite genericPotionIcon;
    public Inventory inventory;

    public float[] alchLvlUp;

    [Serializable]
    public struct AlchemyProgressBars
    {
        public Slider progressBar, alchLvlBar;
        public float totalTime, baseTime, timeLeft, alchXP, alchMaxXp, alchLvl, rwd, limit;
        public bool previousToggleStates;
        public TMP_Text limitText, lvlText;
    }
    public TMP_Text AlchPrestigeAmtText, AlchPrestigeCostText;

    public int[] AlchemyPrestigeCost;
    public float AlchAutoBuyerLvl, AlchAutoBuyerAmt, AlchAutoBuyerMax;


    [Serializable]
    public struct Potions
    {
        public int PotionAmt, PotionMax, PotionStrenght, PotionReq;
        public TMP_Text PotionAmtText, PotionInvText;
    }

    public class PotionRecipe
    {
        public string potionName;
        public List<Ingredient> ingredients;
        public int alchemyReq;
        public int maxPotionAmt;
    }

    [Serializable]
    public class Ingredient
    {
        public string name;
        public int quantity;
    }

    public List<PotionRecipe> potionRecipes;

    public AlchemyProgressBars[] alchemyProgressBar = new AlchemyProgressBars[5];
    public Toggle[] alchemyToggles = new Toggle[5];

    public Potions[] potion = new Potions[3];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlchemyPrestigeCost = new int[] { 0, 5, 10, 15, 20 };
        alchLvlUp = new float[] { 20, 40, 80, 160, 320, 640, 1280, 2560, 5120, 10240 };
        AlchAutoBuyerLvl = 0;
        AlchAutoBuyerMax = 5;

        for (int i = 0; i < alchemyProgressBar.Length; i++)
        {
            if (alchemyProgressBar[i].alchLvl < 1)
            {
                alchemyProgressBar[i].alchLvl = 1;
            }

            alchemyProgressBar[i].alchMaxXp = alchLvlUp[(int)(alchemyProgressBar[i].alchLvl - 1)];
            alchemyProgressBar[i].totalTime = alchemyProgressBar[i].baseTime / alchemyProgressBar[i].alchLvl;
            alchemyProgressBar[i].timeLeft = alchemyProgressBar[i].totalTime;

            alchemyProgressBar[i].limit = 10 * alchemyProgressBar[i].alchLvl;

            alchemyProgressBar[i].previousToggleStates = false;
            alchemyToggles[i].isOn = false;


            UpdateTimerText(i);
            UpdateAlchText();
        }

        for (int i = 0; i < potion.Length; i++)
        {
            potion[i].PotionAmt = 0;
            potion[i].PotionMax = 5;
        }

        AlchAutoBuyerAmt = AlchAutoBuyerLvl;

        //CREATING RECIPES

        potionRecipes = new List<PotionRecipe>
        {
            new() {
                potionName = "Hp Potion 1",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Snake Flower", quantity = 1},
                    new() {name = "Generic Potion", quantity = 1}
                },
                maxPotionAmt = 5
            },
            new() {
                potionName = "Atk Potion 1",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Fire Fruit", quantity = 1},
                    new() {name = "Generic Potion", quantity = 1}
                },
                maxPotionAmt = 5
            },

            new() {
                potionName = "Def Potion 1",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Water Weed", quantity = 1},
                    new() {name = "Generic Potion", quantity = 1}
                },
                maxPotionAmt = 5
            },

            new() {
                potionName = "Spd Potion 1",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Spark Flowers", quantity = 1},
                    new() {name = "Generic Potion", quantity = 1}
                },
                maxPotionAmt = 5
            },

                        new() {
                potionName = "Hp Potion 2",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Animum Powder", quantity = 2},
                    new() {name = "Hp Potion 1", quantity = 1}
                },
                maxPotionAmt = 5
            },
            new() {
                potionName = "Atk Potion 2",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Ember Buds", quantity = 1},
                    new() {name = "Atk Potion 1", quantity = 1}
                },
                maxPotionAmt = 5
            },

            new() {
                potionName = "Def Potion 2",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Frost Berries", quantity = 1},
                    new() {name = "Def Potion 1", quantity = 1}
                },
                maxPotionAmt = 5
            },

            new() {
                potionName = "Spd Potion 2",
                ingredients = new List<Ingredient>
                {
                    new() {name = "Volt Apples", quantity = 1},
                    new() {name = "Spd Potion 1", quantity = 1}
                },
                maxPotionAmt = 5
            }
        };
    }


    public void Update()
    {
        if (alchemyToggles[0].isOn)
        {
            if (alchemyProgressBar[0].rwd < alchemyProgressBar[0].limit)// Handle the first timer separately
            {
                if (alchemyProgressBar[0].timeLeft > 0)
                {
                    alchemyProgressBar[0].timeLeft -= Time.deltaTime;
                }
                else
                {
                    alchemyProgressBar[0].rwd += 1;
                    AlchAddXp(0);
                    alchemyProgressBar[0].timeLeft = alchemyProgressBar[0].totalTime;
                    alchemyProgressBar[0].progressBar.value = 1;

                    alchemyProgressBar[0].previousToggleStates = true;
                }
            }
        }

        // Handle remaining timers
        for (int i = 1; i < alchemyProgressBar.Length; i++)
        {
            if (alchemyToggles[i].isOn) // Check if the toggle is on for this timer
            {
                if (alchemyProgressBar[i].rwd < alchemyProgressBar[i].limit) // Sets a limit to tiers rwd
                {
                    // Start the next timer only if the previous timer has at least one reward and this timer hasn't started yet
                    if (alchemyProgressBar[i - 1].rwd > 0 && alchemyProgressBar[i].timeLeft == alchemyProgressBar[i].totalTime)
                    {
                        alchemyProgressBar[i - 1].rwd -= 1; // Deduct one reward from the previous timer to start this timer
                        alchemyProgressBar[i].timeLeft -= Time.deltaTime;
                    }

                    // Only run the current timer if it has started (timeLeft < totalTime)
                    if (alchemyProgressBar[i].timeLeft < alchemyProgressBar[i].totalTime && alchemyToggles[i].isOn)
                    {
                        alchemyProgressBar[i].timeLeft -= Time.deltaTime;

                        if (alchemyProgressBar[i].timeLeft <= 0) // If the timer finishes
                        {
                            alchemyProgressBar[i].rwd += 1;
                            AlchAddXp(i);
                            alchemyProgressBar[i].timeLeft = alchemyProgressBar[i].totalTime;
                            alchemyProgressBar[i].progressBar.value = 1;
                            UpdateAlchText();
                        }
                    }
                }
            }
        }

        for (int i = 0; i < alchemyProgressBar.Length; i++)
        {
            alchemyProgressBar[i].progressBar.value = alchemyProgressBar[i].timeLeft / alchemyProgressBar[i].totalTime;
            UpdateTimerText(i);
        }
    }

    public void ShowButtons(int index)
    {
        // Get current states
        bool isOn = alchemyToggles[index].isOn;

        if (AlchAutoBuyerAmt > 0 && isOn && !alchemyProgressBar[index].previousToggleStates)
        {
            AlchAutoBuyerAmt -= 1;
            alchemyToggles[index].isOn = true;

        }
        else if (AlchAutoBuyerAmt <= 0 && isOn && !alchemyProgressBar[index].previousToggleStates)
        {
            alchemyToggles[index].isOn = false;
        }
        else if (!isOn && alchemyProgressBar[index].previousToggleStates)
        {
            AlchAutoBuyerAmt += 1;
            alchemyToggles[index].isOn = false;
        }
        alchemyProgressBar[index].previousToggleStates = alchemyToggles[index].isOn;
    }

    public void AlchAddXp(int index) //Adding XP and triggering Level up function
    {
        if (alchemyProgressBar[index].alchLvl < 10)
        {
            alchemyProgressBar[index].alchXP += 1;
            UpdateTimerText(index);

            if (alchemyProgressBar[index].alchXP == 0)
            {
                alchemyProgressBar[index].alchLvlBar.value = 0;
            }
            else
            {
                alchemyProgressBar[index].alchLvlBar.value = alchemyProgressBar[index].alchXP / alchLvlUp[(int)(alchemyProgressBar[index].alchLvl - 1)];
            }

            if (alchemyProgressBar[index].alchXP >= alchemyProgressBar[index].alchMaxXp)
            {
                alchemyProgressBar[index].alchXP = 0;
                alchemyProgressBar[index].alchLvl += 1;
                alchemyProgressBar[index].limit = 10 * alchemyProgressBar[index].alchLvl;
                alchemyProgressBar[index].alchMaxXp = alchLvlUp[(int)(alchemyProgressBar[index].alchLvl - 1)];
                alchemyProgressBar[index].totalTime = alchemyProgressBar[index].baseTime / alchemyProgressBar[index].alchLvl;
                UpdateAlchText();
            }
        }
        saveManager.Save();
    }

    public void PurchaseAutoBuyer()
    {
        if (AlchAutoBuyerLvl < AlchAutoBuyerMax)
        {
            if (prestige.prestigeMulti - 1 >= AlchemyPrestigeCost[(int)AlchAutoBuyerLvl])
            {
                prestige.prestigeMulti -= AlchemyPrestigeCost[(int)AlchAutoBuyerLvl];
                enemyStats.ResetPrestige();
                AlchAutoBuyerAmt += 1;
                AlchAutoBuyerLvl += 1;
            }
        }

    }

    public void UpdateTimerText(int index)
    {
        if (alchemyProgressBar[index].limitText != null)
        {
            alchemyProgressBar[index].limitText.text = alchemyProgressBar[index].rwd + "/" + alchemyProgressBar[index].limit;
        }
        if (AlchPrestigeAmtText != null)
        {
            AlchPrestigeAmtText.text = AlchAutoBuyerAmt + "/" + AlchAutoBuyerMax;
        }
        if (AlchPrestigeCostText != null && AlchAutoBuyerLvl < AlchAutoBuyerMax)
        {
            AlchPrestigeCostText.text = "Prestige Cost: " + AlchemyPrestigeCost[(int)AlchAutoBuyerLvl];
        }
        else if (AlchPrestigeCostText != null && AlchAutoBuyerLvl == AlchAutoBuyerMax)
        {
            AlchPrestigeCostText.text = "Prestige Cost: MAX ";
        }
        alchemyProgressBar[index].alchLvlBar.value = alchemyProgressBar[index].alchXP / alchemyProgressBar[index].alchMaxXp;

        //POTION TEXT

        if (index < potion.Length && potion[index].PotionAmtText != null)
        {
            potion[index].PotionAmtText.text = potion[index].PotionAmt + "/" + potion[index].PotionMax;
        }
        if (index < potion.Length && potion[index].PotionInvText != null)
        {
            potion[index].PotionInvText.text = potion[index].PotionAmt + "";
        }

    }

    public void BrewPotion(int index)
    {
        PotionRecipe recipe = potionRecipes[index];
        var itemDefinition = playerInventory.masterItemList.Find(item => item.name == recipe.potionName);
        var inventoryItem = playerInventory.sampleList.Find(item => item.name == recipe.potionName);
        bool hasAllIngredients = true;

        // Check if player has all required ingredients
        foreach (var ingredient in recipe.ingredients)
        {
            if (!playerInventory.HasItem(ingredient.name, ingredient.quantity)) // If not, return
            {
                Debug.Log($"Missing required ingredient: {ingredient.name}");
                hasAllIngredients = false;
                return;
            }
            else
            {
                Debug.Log($"Has {ingredient.quantity}: {ingredient.name}");
            }
        }

        // Only proceed if player has all ingredients
        if (hasAllIngredients)
        {
            if (inventoryItem == null || inventoryItem.quantity < 99)
            {
                // Subtract ingredients
                foreach (var ingredient in recipe.ingredients)
                {
                    var ingredientList = playerInventory.sampleList.Find(item => item.name == ingredient.name);
                    ingredientList.quantity -= ingredient.quantity;

                    if (ingredientList.quantity <= 0)
                    {
                        playerInventory.sampleList.Remove(ingredientList);
                        Debug.Log($"{ingredient.name} was used up and removed from inventory.");
                    }
                }
                playerInventory.AddItem(itemDefinition.name, 1, itemDefinition.icon);
                saveManager.Save();
            }
            return;
        }
    }
    public void BrewGeneric()
    {
        Inventory.InventoryItem genericPotion = inventory.sampleList.Find(item => item.name == "Generic Potion");
        if (alchemyProgressBar[4].rwd > 0 && (genericPotion == null || genericPotion.quantity < 99))
        {
            //SET CONDITION FOR IF POTION IS NULL AND ALSO A CONDITION FOR IF IT'S OVER 99
            alchemyProgressBar[4].rwd -= 1;
            playerInventory.AddItem("Generic Potion", 1, genericPotionIcon);
        }

    }

    public void UpdateAlchText()
    {
        for (int i = 0; i < alchemyProgressBar.Length; i++)
        {
            if (alchemyProgressBar[i].alchLvl < 10)
            {
                alchemyProgressBar[i].lvlText.text = "Lvl: " + alchemyProgressBar[i].alchLvl;
            }
            else
            {
                alchemyProgressBar[i].lvlText.text = "Lvl: MAX";
                alchemyProgressBar[i].progressBar.value = 0;
            }
        }
    }

}

