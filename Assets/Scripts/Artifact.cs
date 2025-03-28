using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Artifact : MonoBehaviour
{
    [SerializeField] private List<AudioClip> collectSound;
    
    public event UnityAction OnPickUpActionEvent;

    private void OnTriggerEnter(Collider other) //Won't collide with enemies since neither have rigidbodies
    {
        OnPickUpActionEvent?.Invoke();
        SoundManager.Instance.PlayRandomSfxSound(collectSound,transform);
        Destroy(this.gameObject);
    }
}
