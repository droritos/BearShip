using System;
using UnityEngine;
using UnityEngine.Events;

public class Artifacts : MonoBehaviour
{
    public event UnityAction OnPickUpActionEvent;

    private void OnCollisionEnter(Collision other)
    {
        OnPickUpActionEvent?.Invoke();
        Destroy(this);
    }
}
