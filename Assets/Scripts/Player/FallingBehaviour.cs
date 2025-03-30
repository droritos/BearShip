using UnityEngine;
using UnityEngine.Events;

public class FallingBehaviour : MonoBehaviour
{
    public event UnityAction<FallingBehaviour> OnFallingFromWorld; 
    
    [SerializeField] private float fallingThreshold = 15f;
    [SerializeField] private float checkInterval = 1f; 

    private bool _hasFallen = false;


    private void Update()
    {
        CheckFalling();
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(CheckFalling));
    }

    private void CheckFalling()
    {
        if (!_hasFallen && transform.position.y <= -fallingThreshold) // Only trigger once
        {
            _hasFallen = true;
            OnFallingFromWorld?.Invoke(this);
        }
    }

    public void ResetFallingState() 
    {
        _hasFallen = false;
    }
}
