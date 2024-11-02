using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator; // Reference to Animator component
    public EnemyStats enemyStats; // Reference to EnemyStats script

    private int currentStage = -1;

    void Start()
    {
        // Set the initial stage
        UpdateStageAnimation();
    }

    void Update()
    {
        // Check if the stage has changed and update animation
        if (enemyStats.Stage != currentStage)
        {
            currentStage = enemyStats.Stage;
            UpdateStageAnimation();
        }
    }

    private void UpdateStageAnimation()
    {
        // Set the "Stage" parameter in Animator to control the animation state
        animator.SetInteger("Stage", currentStage);
    }
}