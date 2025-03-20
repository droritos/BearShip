using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] ThirdPersonActionAsset _playerActionAssets;
    private InputAction _move;

    [Header("Movement")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] private float movementForce = 1f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float maxSpeed = 5f;
    private Vector3 _forceDirection = Vector3.zero;


    [SerializeField] Camera _mainCamera;

    private void Awake()
    {
        _playerActionAssets = new ThirdPersonActionAsset();
    }
    private void OnEnable()
    {
        // Subscribe to the Jump.started event
        _playerActionAssets.Player.Jump.started += DoJump;
        _move = _playerActionAssets.Player.Move;
        _playerActionAssets.Player.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from the Jump.started event
        _playerActionAssets.Player.Jump.started -= DoJump;
        _playerActionAssets.Player.Disable();
    }

    private void FixedUpdate()
    {
        _forceDirection += _move.ReadValue<Vector2>().x * GetCameraRight(_mainCamera) * movementForce;
        _forceDirection += _move.ReadValue<Vector2>().y * GetCameraFoward(_mainCamera) * movementForce;

        _rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;

        
        if (_rigidbody.linearVelocity.y < 0f)
        {
            _rigidbody.linearVelocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = _rigidbody.linearVelocity;
        horizontalVelocity.y = 0f;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            _rigidbody.linearVelocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * _rigidbody.linearVelocity.y;
        }

        LookAt();
    }
    private Vector3 GetCameraFoward(Camera mainCamera)
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
            _forceDirection = Vector3.up * jumpForce;
        }
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
