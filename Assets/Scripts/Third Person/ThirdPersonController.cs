using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Serialize Field")]
    [SerializeField] ThirdPersonAnimation animator;
    [SerializeField] Camera _mainCamera;
    private ThirdPersonActionAsset _playerActionAssets;
    private InputAction _move;

    [Header("Movement")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] CharacterData characterData;
    private Vector3 _forceDirection = Vector3.zero;
    private Vector2 _moveInput;

    [Header("Sounds")] 
    [SerializeField] private AudioClip jumpSound;


    private void Awake()
    {
        _playerActionAssets = new ThirdPersonActionAsset();
    }
    private void OnEnable()
    {
        // Subscribe to the Jump.started event
        _move = _playerActionAssets.Player.Move;
        _playerActionAssets.Player.Jump.started += DoJump;
        _playerActionAssets.Player.Action.started += DoPickUp;
        _playerActionAssets.Player.Enable();
    }
    private void OnDisable()
    {
        // Unsubscribe from the Jump.started event
        _playerActionAssets.Player.Jump.started -= DoJump;
        _playerActionAssets.Player.Action.started -= DoPickUp;
        _playerActionAssets.Player.Disable();
    }
    private void FixedUpdate()
    {
        HandleMove();

        // Apply exponential gravity
        if (_rigidbody.linearVelocity.y < 0) // Falling
        {
            _rigidbody.linearVelocity += Vector3.up * Physics.gravity.y * 2 * Time.fixedDeltaTime;
        }

        // Clamp velocity to prevent excessive speed
        Vector3 horizontalVelocity = _rigidbody.linearVelocity;
        horizontalVelocity.y = 0f;

        if (horizontalVelocity.sqrMagnitude > characterData.MaxSpeed * characterData.MaxSpeed)
        {
            _rigidbody.linearVelocity = horizontalVelocity.normalized * characterData.MaxSpeed + Vector3.up * _rigidbody.linearVelocity.y;
        }

        LookAt();
    }
    private void HandleMove()
    {
        _moveInput = _move.ReadValue<Vector2>();

        _forceDirection += _moveInput.x * characterData.MovementForce * GetCameraRight(_mainCamera);
        _forceDirection += _moveInput.y * characterData.MovementForce * GetCameraForward(_mainCamera);

        _rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;
    } 

    private Vector3 GetCameraForward(Camera mainCamera)
    {
        Vector3 foward = mainCamera.transform.forward;
        foward.y = 0;
        return foward.normalized;
    }

    private Vector3 GetCameraRight(Camera mainCamera)
    {
        Vector3 right = mainCamera.transform.right;
        right.y = 0;    // Should be X? 
        return right.normalized;
    }

    // Updated DoJump method to accept CallbackContext
    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            SoundManager.Instance.PlaySfxSound(jumpSound, transform);
            _rigidbody.AddForce(Vector3.up * characterData.JumpForce, ForceMode.Impulse);
        }
    }
    private void DoPickUp(InputAction.CallbackContext context)
    {
        //Debug.Log("DoAttack");
        animator.Animator.SetTrigger("PickUp");
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void LookAt()
    {
        Vector3 direction = _rigidbody.linearVelocity;
        direction.y = 0;

        if (_move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this._rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            _rigidbody.angularVelocity = Vector3.zero;
    }

    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction * 5, ForceMode.Impulse);
    }
    
}