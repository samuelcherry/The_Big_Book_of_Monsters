using JetBrains.Annotations;
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





    public float totalTime;
    private float timeLeft;
    public float enemyAtkTime;
    private float enemyAtkTimeLeft;


    public Animator animator;
    public Animator enemyAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalTime = playerStats.speedArray[playerStats.level - 1];
        enemyAtkTime = enemyStats.enemies[enemyStats.Stage - 1].enemySpeed;

        timeLeft = totalTime;
        progressBar.value = 1;

        enemyAtkTimeLeft = enemyAtkTime;
        enemyAtkTimer.value = 1;


        enemyAnimator.SetInteger("Stage", enemyStats.Stage - 1);

        UpdateSpdText();

        if (playerStats == null)
        {
            Debug.LogError("Score sciprt not found in the scene!");
        };

    }


    // Update is called once per frame
    void Update()
    {

        //PLAYER ATTACK
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            progressBar.value = timeLeft / totalTime;
        }
        else
        {

            enemyStats.TakeDamage();
            PlayerAttack();

            timeLeft = totalTime;
            progressBar.value = 1;
        }

        //ENEMY ATTACK
        if (enemyAtkTimeLeft > 0)
        {
            enemyAtkTimeLeft -= Time.deltaTime;
            enemyAtkTimer.value = enemyAtkTimeLeft / enemyAtkTime;
        }
        else
        {
            playerStats.PlayerTakeDamage();
            EnemyAttack();
            enemyAtkTimeLeft = enemyAtkTime;
            enemyAtkTimer.value = 1;
        }
    }

    public void SetStageAnimation()
    {
        enemyAnimator.SetInteger("Stage", enemyStats.Stage - 1);
    }

    private void PlayerAttack()
    {
        animator.SetTrigger("PlayerAttack");
        enemyAnimator.SetTrigger("EnemyTakeHit");
    }

    private void EnemyAttack()
    {
        enemyAnimator.SetTrigger("EnemyAttack");
        animator.SetTrigger("PlayerTakeHit");
    }


    public void UpdateSpdText()
    {
        if (spdText != null)
        {
            spdText.text = "Spd: " + totalTime;
        }
    }
}
