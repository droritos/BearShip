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
        _playerActionAssets.Player.Move.performed -= OnMovePerformed;
        _playerActionAssets.Player.Jump.started -= DoJump;
        _playerActionAssets.Player.Action.started -= DoPickUp;
        _playerActionAssets.Player.Disable();
    }
    private void Update()
    {
        _moveInput = _move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        HandleMove();

        if (_rigidbody.linearVelocity.y < 0f)
        {
            _rigidbody.linearVelocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = _rigidbody.linearVelocity;
        horizontalVelocity.y = 0f; // No need to y cuz no need for Vertically
        if (horizontalVelocity.sqrMagnitude > characterData.MaxSpeed * characterData.MaxSpeed)
        {
            _rigidbody.linearVelocity = horizontalVelocity.normalized * characterData.MaxSpeed + Vector3.up * _rigidbody.linearVelocity.y;
        }

        LookAt();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _forceDirection += _moveInput.x * characterData.MovementForce * GetCameraRight(_mainCamera);
        _forceDirection += _moveInput.y * characterData.MovementForce * GetCameraForward(_mainCamera);

        _rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;
    }
    private void OnMovCancled(InputAction.CallbackContext context)
    {
        //_rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;
    }

    private void HandleMove()
    {
        _forceDirection += _moveInput.x * characterData.MovementForce * GetCameraRight(_mainCamera);
        _forceDirection += _moveInput.y * characterData.MovementForce * GetCameraForward(_mainCamera);

        _rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;
    } // WORKS


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
            _forceDirection = Vector3.up * characterData.JumpForce;
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
}