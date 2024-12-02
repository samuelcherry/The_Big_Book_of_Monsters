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



    public float playerAtkTime, playerAtkTimeLeft, enemyAtkTime, enemyAtkTimeLeft, spdBuff;

    public Animator animator;
    public Animator enemyAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAtkTime = playerStats.speedArray[playerStats.level - 1];
        enemyAtkTime = enemyStats.currentAdventure.enemies[enemyStats.Stage - 1].enemySpeed;

        playerAtkTimeLeft = playerAtkTime;
        progressBar.value = 1;

        enemyAtkTimeLeft = enemyAtkTime;
        enemyAtkTimer.value = 1;


        enemyAnimator.SetInteger("Stage", enemyStats.Stage - 1);

        if (playerStats == null)
        {
            Debug.LogError("Score sciprt not found in the scene!");
        };

    }


    // Update is called once per frame
    void Update()
    {

        //PLAYER ATTACK
        if (playerAtkTimeLeft > 0)
        {
            playerAtkTimeLeft -= Time.deltaTime;
            progressBar.value = playerAtkTimeLeft / playerAtkTime;
        }
        else
        {
            enemyStats.TakeDamage();
            PlayerAttack();
            playerAtkTimeLeft = playerAtkTime;
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
        animator.SetTrigger("Attack1");
        enemyAnimator.SetTrigger("EnemyTakeHit");
    }

    private void EnemyAttack()
    {
        enemyAnimator.SetTrigger("EnemyAttack");
        animator.SetTrigger("Hurt");
    }
}
