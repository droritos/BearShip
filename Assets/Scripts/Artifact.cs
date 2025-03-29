using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Artifact : MonoBehaviour
{
    private const string triggerName = "PickUp";
    
    [SerializeField] private List<AudioClip> collectSound;
    [SerializeField] private Animator animator;
    
    public event UnityAction OnPickUpActionEvent;

    private void OnTriggerEnter(Collider other) //Won't collide with enemies since neither have rigidbodies
    {
        OnPickUpActionEvent?.Invoke();
        SoundManager.Instance.PlayRandomSfxSound(collectSound,transform);
        animator.SetBool("PickUp", true);
        Destroy(this.gameObject, 0.25f);
    }
}
