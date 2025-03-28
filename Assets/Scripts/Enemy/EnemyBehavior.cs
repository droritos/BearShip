using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioClip attackSound;
    public event UnityAction<Vector3> OnCollisionEventAction; 

    //Add force to the player when colliding with him
    public void OnCollisionEnter(Collision other)
    {
        SoundManager.Instance.PlaySfxSound(attackSound, transform);
        Vector3 destinationDirection = new Vector3(agent.destination.x - transform.position.x, 1, agent.destination.z - transform.position.z);
        destinationDirection.Normalize();
        OnCollisionEventAction?.Invoke(new Vector3(destinationDirection.x * 10, 1, destinationDirection.z * 10));
    }
}
