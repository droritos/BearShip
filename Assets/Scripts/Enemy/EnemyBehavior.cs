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

    [Header("Knockback Data")]
    [SerializeField] float knockbackStrength = 15f;
    [SerializeField] float knockbackYAxis = 0.4f;
    [SerializeField] float horizontalMultiplier = 1.5f;

    //Add force to the player when colliding with him
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalInfo.PlayerTag))
        {
            SoundManager.Instance.PlaySfxSound(attackSound, transform);
            animator.SetTrigger(GlobalInfo.AttackAnimation);

            // Get direction directly from enemy to player
            Vector3 knockbackDirection = other.transform.position - transform.position;
            knockbackDirection.y = 0; 
            knockbackDirection.Normalize();

            Vector3 finalKnockback = new Vector3(
                knockbackDirection.x * horizontalMultiplier,
                knockbackYAxis,
                knockbackDirection.z * horizontalMultiplier
            );

            //Vector3 finalKnockback = new Vector3(knockbackDirection.x, knockbackYAxis, knockbackDirection.z);

            OnCollisionEventAction?.Invoke(finalKnockback * knockbackStrength);
        }
    }
}
