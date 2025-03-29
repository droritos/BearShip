using UnityEngine;

public class OnAttack : StateMachineBehaviour
{
    EnemyBehavior enemyBehavior;
    private EnemyBehavior FindEnemyBehaviorInParents(Transform child)
    {
        while (child != null) // Keep moving up until no parent exists
        {
            EnemyBehavior enemy = child.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                return enemy; // Found it, return immediately
            }
            child = child.parent; // Move to the next parent
        }
        return null; // No EnemyBehavior found in any parent
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyBehavior = FindEnemyBehaviorInParents(animator.transform); // Find EnemyBehavior in parents

        if (enemyBehavior != null)
        {
            enemyBehavior.InvokeAttack();
            Debug.Log("Finish Attack Animation Now Knockback");
        }
        else
        {
            Debug.LogError("EnemyBehavior not found in any parent of: " + animator.gameObject.name);
        }
    }
}