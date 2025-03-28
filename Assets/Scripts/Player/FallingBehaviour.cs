using UnityEngine;
using UnityEngine.Events;

public class FallingBehaviour : MonoBehaviour
{
    public event UnityAction<GameObject> OnFallingFromWorld; 
    
    [SerializeField] private float fallingThreshold = 10.0f;
    [SerializeField] private float checkInterval = 0.5f; 

    private bool _hasFallen = false;


    private void Start()
    {
        InvokeRepeating(nameof(CheckFalling), checkInterval, checkInterval);
    }

    private void CheckFalling()
    {
        if (!_hasFallen && transform.position.y <= -fallingThreshold) // Only trigger once
        {
            _hasFallen = true;
            //Debug.Log($"I falling - {transform.position.y}");
            OnFallingFromWorld.Invoke(this.gameObject);
            // Optional: Stop checking after falling
            //CancelInvoke(nameof(CheckFalling));
        }
    }

    public void ResetFallingState() // May not be used
    {
        _hasFallen = false;
    }
}
