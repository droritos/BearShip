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

    public void ResetFallingState()
    {
        _hasFallen = false;
    }

    public void ChangeThreshold(float playerYPosition , float newThreshold = 15)
    {
        fallingThreshold = Mathf.Abs(playerYPosition) + newThreshold;
    }
    private void CheckFalling()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity)) return;

        // Check if the object has fallen below the threshold and hasn't triggered the event
        if (!_hasFallen && Mathf.Abs(transform.position.y) >= Mathf.Abs(fallingThreshold)) // Simplified logic
        {
            _hasFallen = true;
            OnFallingFromWorld?.Invoke(this); // Trigger the event
        }
    }
}
