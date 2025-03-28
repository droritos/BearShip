using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetection : MonoBehaviour
{
    public event UnityAction<Transform> OnTargetDetectedEventAction;
    public event UnityAction OnTargetEscapedEventAction;

    [SerializeField] private string targetTag;

    [SerializeField] private AudioClip detectionSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            OnTargetDetectedEventAction?.Invoke(other.transform);
            SoundManager.Instance.PlaySfxSound(detectionSound, transform);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag)) //Maybe add an extra 3 seconds wait before dropping it
        {
            OnTargetEscapedEventAction?.Invoke();
        }
    }
}
