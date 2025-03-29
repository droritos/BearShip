using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    public event UnityAction<Vector3> OnCollisionEventAction;

    [SerializeField] Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioClip attackSound;

    //Add force to the player when colliding with him
    public void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.CompareTag("Player"))
        SoundManager.Instance.PlaySfxSound(attackSound, transform);
        //InvokeAttack();
        animator.SetTrigger("Attack");
    }

    public void InvokeAttack()
    {
        Vector3 destinationDirection = new Vector3(agent.destination.x - transform.position.x, 1, agent.destination.z - transform.position.z);
        destinationDirection.Normalize();
        OnCollisionEventAction?.Invoke(new Vector3(destinationDirection.x * 10, 1, destinationDirection.z * 10));
    }
}
