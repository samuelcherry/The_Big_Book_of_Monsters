using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyTimers : MonoBehaviour
{

    public Prestige prestige;
    public PlayerStats playerStats;
    public EnemyStats enemyStats;
    public SaveManager saveManager;

    [System.Serializable]
    public struct AlchemyProgressBars
    {
        public Slider progressBar, alchLvlBar;
        public float totalTime, baseTime, timeLeft, alchXP, alchMaxXp;
        public int rwd, req, limit, alchLvl;
        public bool canRun, previousToggleStates;
        public TMP_Text limitText, lvlText;
    }
    public TMP_Text AlchPrestigeAmtText, AlchPrestigeCostText;

    public int[] AlchemyPrestigeCost;
    public int AlchAutoBuyerLvl, AlchAutoBuyerAmt, AlchAutoBuyerMax;


    [System.Serializable]
    public struct Potions
    {
        public int PotionAmt, PotionMax, PotionStrenght, PotionReq;
        public TMP_Text PotionAmtText, PotionInvText;
    }


    public AlchemyProgressBars[] alchemyProgressBar = new AlchemyProgressBars[5];
    public Toggle[] alchemyToggles = new Toggle[5];

    public Potions[] potion = new Potions[3];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        AlchemyPrestigeCost = new int[] { 1, 5, 10, 15, 20 };
        AlchAutoBuyerLvl = 0;
        AlchAutoBuyerMax = 5;

        potion[0].PotionStrenght = 25;
        potion[1].PotionStrenght = 50;
        potion[2].PotionStrenght = 100;

        potion[0].PotionReq = 1;
        potion[1].PotionReq = 2;
        potion[2].PotionReq = 3;

        for (int i = 0; i < alchemyProgressBar.Length; i++)
        {
            alchemyProgressBar[i].alchMaxXp = math.floor(100 * alchemyProgressBar[i].alchLvl / (i + 1));
            alchemyProgressBar[i].totalTime = alchemyProgressBar[i].baseTime / alchemyProgressBar[i].alchLvl;
            UpdateAlchText();

        }

        for (int i = 0; i < potion.Length; i++)
        {
            potion[i].PotionAmt = 0;
            potion[i].PotionMax = 5;
        }

        for (int i = 0; i < alchemyProgressBar.Length; i++)  //sets the timers to 0 and reward total to 0
        {
            alchemyProgressBar[i].timeLeft = alchemyProgressBar[i].totalTime;
            alchemyProgressBar[i].limit = 10 * alchemyProgressBar[i].alchLvl;
        }


        for (int i = 0; i < alchemyToggles.Length; i++)
        {
            alchemyProgressBar[i].previousToggleStates = alchemyToggles[i].isOn;
        }

        for (int i = 0; i < alchemyProgressBar.Length; i++)
        {
            UpdateTimerText(i);
        }

        AlchAutoBuyerAmt = AlchAutoBuyerLvl;
        saveManager.AlchemyLoad();
    }

    // Update is called once per frame
    public void Update()
    {
        if (alchemyToggles[0].isOn)
        {
            // Handle the first timer separately
            if (alchemyProgressBar[0].rwd < alchemyProgressBar[0].limit)
            {
                if (alchemyProgressBar[0].timeLeft > 0)
                {
                    alchemyProgressBar[0].timeLeft -= Time.deltaTime;
                    alchemyProgressBar[0].progressBar.value = alchemyProgressBar[0].timeLeft / alchemyProgressBar[0].totalTime;
                }
                else
                {

                    alchemyProgressBar[0].rwd += 1;
                    AlchAddXp(0);
                    alchemyProgressBar[0].timeLeft = alchemyProgressBar[0].totalTime;
                    alchemyProgressBar[0].progressBar.value = 1;

                    alchemyProgressBar[0].previousToggleStates = true;
                    alchemyToggles[0].isOn = true;
                    alchemyProgressBar[0].progressBar.value = alchemyProgressBar[0].timeLeft / alchemyProgressBar[0].totalTime;
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
                        alchemyProgressBar[i].progressBar.value = alchemyProgressBar[i].timeLeft / alchemyProgressBar[i].totalTime;
                        alchemyProgressBar[i].canRun = true;
                    }

                    // Only run the current timer if it has started (timeLeft < totalTime)
                    if (alchemyProgressBar[i].timeLeft < alchemyProgressBar[i].totalTime && alchemyProgressBar[i].canRun)
                    {
                        alchemyProgressBar[i].timeLeft -= Time.deltaTime;
                        alchemyProgressBar[i].progressBar.value = alchemyProgressBar[i].timeLeft / alchemyProgressBar[i].totalTime;

                        if (alchemyProgressBar[i].timeLeft <= 0) // If the timer finishes
                        {
                            alchemyProgressBar[i].rwd += 1;
                            AlchAddXp(i);
                            alchemyProgressBar[i].timeLeft = alchemyProgressBar[i].totalTime;
                            alchemyProgressBar[i].progressBar.value = 1;
                            alchemyProgressBar[i].canRun = true;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < alchemyProgressBar.Length; i++)
        {
            UpdateTimerText(i);
        }
    }

    public void ShowButtons(int index)
    {
        // Get current states
        bool isOn = alchemyToggles[index].isOn;
        bool previousState = alchemyProgressBar[index].previousToggleStates;

        if (AlchAutoBuyerAmt > 0 && isOn && !previousState)
        {
            AlchAutoBuyerAmt -= 1;
            alchemyToggles[index].isOn = true;

        }
        else if (AlchAutoBuyerAmt <= 0 && isOn && !previousState)
        {
            alchemyToggles[index].isOn = false;
        }
        else if (!isOn && previousState)
        {
            AlchAutoBuyerAmt += 1;
            alchemyToggles[index].isOn = false;
        }
        alchemyProgressBar[index].canRun = alchemyToggles[index].isOn;
        alchemyProgressBar[index].previousToggleStates = alchemyToggles[index].isOn;
    }

    public void AlchAddXp(int index) //Adding XP and triggering Level up function
    {
        if (alchemyProgressBar[index].alchLvl < 10)
        {

            alchemyProgressBar[index].alchXP += 1;

            //Rest timers
            alchemyProgressBar[index].limit = 10 * alchemyProgressBar[index].alchLvl;
            alchemyProgressBar[index].alchMaxXp = math.floor(100 * alchemyProgressBar[index].alchLvl / (index + 1));
            alchemyProgressBar[index].totalTime = alchemyProgressBar[index].baseTime / alchemyProgressBar[index].alchLvl;
            alchemyProgressBar[index].timeLeft = alchemyProgressBar[index].totalTime;
            UpdateTimerText(index);

            if (alchemyProgressBar[index].alchXP == 0)
            {
                alchemyProgressBar[index].alchLvlBar.value = 0;
            }
            else
            {
                alchemyProgressBar[index].alchLvlBar.value = alchemyProgressBar[index].alchXP / alchemyProgressBar[index].alchMaxXp;
            }

            if (alchemyProgressBar[index].alchXP >= alchemyProgressBar[index].alchMaxXp)
            {
                alchemyProgressBar[index].alchXP = 0;
                alchemyProgressBar[index].alchLvl += 1;
                alchemyProgressBar[index].limit = 10 * alchemyProgressBar[index].alchLvl;
                alchemyProgressBar[index].alchMaxXp = math.floor(100 * alchemyProgressBar[index].alchLvl / (index + 1));
                alchemyProgressBar[index].totalTime = alchemyProgressBar[index].baseTime / alchemyProgressBar[index].alchLvl;
                UpdateAlchText();
            }
        }
        saveManager.AlchemySave();
    }



    public void PurchaseAutoBuyer()
    {
        if (AlchAutoBuyerLvl < AlchAutoBuyerMax)
        {
            if (prestige.prestigeMulti - 1 >= AlchemyPrestigeCost[AlchAutoBuyerLvl])
            {
                prestige.prestigeMulti -= AlchemyPrestigeCost[AlchAutoBuyerLvl];
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
            AlchPrestigeCostText.text = "Prestige Cost: " + AlchemyPrestigeCost[AlchAutoBuyerLvl];
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
        if (potion[index].PotionAmt < potion[index].PotionMax)
        {
            if (alchemyProgressBar[4].rwd >= potion[index].PotionReq)
            {
                alchemyProgressBar[4].rwd -= potion[index].PotionReq;
                potion[index].PotionAmt += 1;
            }
        }
        saveManager.AlchemySave();
    }

    public void UsePotions(int index)
    {
        if (potion[index].PotionAmt > 0)
        {
            Debug.Log("USE POTION");
            potion[index].PotionAmt -= 1;
            playerStats.currentHp += 100 * (index + 1);
            if (playerStats.currentHp > playerStats.maxHp)
            {
                playerStats.currentHp = playerStats.maxHp;
            }
            UpdateTimerText(index);
            playerStats.UpdateHpText();


        }
        saveManager.AlchemySave();
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
            }
        }
    }

}

