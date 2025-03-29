using UnityEngine;

public class OnAttack : StateMachineBehaviour
{
    EnemyBehavior enemyBehavior;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    enemyBehavior = animator.GetComponent<EnemyBehavior>();
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyBehavior = animator.transform.root.GetComponent<EnemyBehavior>(); // Reaching the main Parent component

        if (enemyBehavior != null)
        {
            enemyBehavior.InvokeAttack();
            Debug.Log("Finish Attack Animation Now Knockback");
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)

    //}


    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    _toucanEnemy = animator.GetComponentInParent<ToucanEnemy>();

    //    if (_toucanEnemy != null)
    //    {
    //        _toucanEnemy.TryAttack(_toucanEnemy.MyFeets.CurrentEdgedAttached, _damage);
    //        Debug.Log($"ToucanEnemy Damage Done {_damage}");
    //        _counterAttacks++;

    //    }
    //    else
    //    {
    //        Debug.LogWarning("ToucanEnemy component not found on Animator's GameObject!");
    //    }
    //}

}