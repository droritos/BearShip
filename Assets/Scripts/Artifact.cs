using System;
using UnityEngine;
using UnityEngine.Events;

public class Artifact : MonoBehaviour
{
    public event UnityAction OnPickUpActionEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnPickUpActionEvent?.Invoke();
        Destroy(this.gameObject);
    }
}
