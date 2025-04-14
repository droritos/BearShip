using UnityEngine;
using UnityEngine.Events;

public class FallingBehaviour : MonoBehaviour
{
    public event UnityAction<FallingBehaviour> OnFallingFromWorld;
    private bool _hasFallen = false;
    /*
    
    [SerializeField] private float fallingThreshold = 15f;
    [SerializeField] private float checkInterval = 1f;



    private void Update()
    {
        CheckFalling();
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(CheckFalling));
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
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalInfo.FallBoxTag) && !_hasFallen)
        {
            _hasFallen = true;
            OnFallingFromWorld.Invoke(this);
            // InvokeFalling();
        }

    }
    public void InvokeFalling()
    {
        OnFallingFromWorld.Invoke(this);
    }
    public void ResetFallingState()
    {
        _hasFallen = false;
    }

}
