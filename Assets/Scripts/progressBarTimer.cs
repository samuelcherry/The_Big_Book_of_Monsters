using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarTimer : MonoBehaviour
{

    public PlayerStats playerStats;
    public EnemyStats enemyStats;

    public Slider progressBar;
    public Slider enemyAtkTimer;

    public TMP_Text spdText;

    private int level;
    private int stage;


    readonly float[] speedArray = new float[] { 2F, 2F, 2F, 1F, 0.5F, 0.25F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F };
    readonly float[] enemySpeedArray = new float[] { 2F, 2F, 2F, 2F, 1F, 0.5F, 0.25F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F, 0.125F };



    private float totalTime;
    private float timeLeft;
    private float enemyAtkTime;
    private float enemyAtkTimeLeft;




    public Animator animator;
    public Animator enemyAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateSpdText();
        level = playerStats.level;
        stage = enemyStats.Stage;

        totalTime = speedArray[level];
        enemyAtkTime = enemySpeedArray[stage];

        timeLeft = totalTime;
        progressBar.value = 1;

        enemyAtkTimeLeft = enemyAtkTime;
        enemyAtkTimer.value = 1;



        if (playerStats == null)
        {
            Debug.LogError("Score sciprt not found in the scene!");
        };

    }


    // Update is called once per frame
    void Update()
    {
        level = playerStats.level;
        stage = enemyStats.Stage;
        totalTime = speedArray[level - 1];
        enemyAtkTime = enemySpeedArray[stage - 1];


        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            progressBar.value = timeLeft / totalTime;
        }
        else
        {
            enemyStats?.TakeDamage();
            TriggerAnimation();

            timeLeft = totalTime;
            progressBar.value = 1;
        }


        if (enemyAtkTimeLeft > 0)
        {
            enemyAtkTimeLeft -= Time.deltaTime;
            enemyAtkTimer.value = enemyAtkTimeLeft / enemyAtkTime;
        }
        else
        {
            playerStats?.PlayerTakeDamage();
            enemyAtkTimeLeft = enemyAtkTime;
            enemyAtkTimer.value = 1;
        }



        if (enemyAtkTimeLeft > 0)
        {
            enemyAtkTimeLeft -= Time.deltaTime;
            enemyAtkTimer.value = enemyAtkTimeLeft / enemyAtkTime;
        }
        else
        {
            enemyAtkTimeLeft = enemyAtkTime;
            enemyAtkTimer.value = 1;
        }
    }
    private void TriggerAnimation()
    {
        animator.SetTrigger("PlayAnimation");
        enemyAnimator.SetTrigger("Take_Hit");


    }

    public void UpdateSpdText()
    {
        if (spdText != null)
        {
            spdText.text = "Spd: " + totalTime;
        }
    }
}
