using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ThirdPersonAnimation animator;
    [SerializeField] Camera _mainCamera;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] CharacterData characterData;

    private ThirdPersonActionAsset _playerActionAssets;
    private InputAction _moveAction;

    // Tracking input state more explicitly
    private Vector2 _currentMoveInput;
    private bool _isMoving = false;

    private void Awake()
    {
        _playerActionAssets = new ThirdPersonActionAsset();
    }

    private void OnEnable()
    {
        // Get the move action
        _moveAction = _playerActionAssets.Player.Move;

        // Add explicit event handlers
        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;

        _playerActionAssets.Player.Jump.started += DoJump;
        _playerActionAssets.Player.Action.started += DoPickUp;

        _playerActionAssets.Player.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        _moveAction.performed -= OnMovePerformed;
        _moveAction.canceled -= OnMoveCanceled;
        _playerActionAssets.Player.Jump.started -= DoJump;
        _playerActionAssets.Player.Action.started -= DoPickUp;
        _playerActionAssets.Player.Disable();
    }

    // Explicit handlers to track input state
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _currentMoveInput = context.ReadValue<Vector2>();
        _isMoving = true;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _currentMoveInput = Vector2.zero;
        _isMoving = false;
    }

    private void Update()
    {
        // Fallback input reading
        // This ensures input is captured even if events fail
        if (_moveAction != null)
        {
            Vector2 input = _moveAction.ReadValue<Vector2>();
            if (input != Vector2.zero)
            {
                _currentMoveInput = input;
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // Only process movement if there's input
        if (_isMoving && _currentMoveInput != Vector2.zero)
        {
            // Calculate camera-relative movement direction
            Vector3 cameraForward = GetCameraForward(_mainCamera);
            Vector3 cameraRight = GetCameraRight(_mainCamera);

            // Combine input with camera orientation
            Vector3 moveDirection = (cameraForward * _currentMoveInput.y + cameraRight * _currentMoveInput.x).normalized;

            // Set velocity directly
            Vector3 targetVelocity = moveDirection * characterData.MaxSpeed;

            // Preserve vertical velocity
            targetVelocity.y = _rigidbody.linearVelocity.y;

            // Set the new velocity
            _rigidbody.linearVelocity = targetVelocity;

            // Rotate to face movement direction
            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            }
        }
        else
        {
            // Gradually reduce horizontal velocity when not moving
            Vector3 velocity = _rigidbody.linearVelocity;
            velocity.x = Mathf.Lerp(velocity.x, 0, Time.fixedDeltaTime * 10f);
            velocity.z = Mathf.Lerp(velocity.z, 0, Time.fixedDeltaTime * 10f);
            _rigidbody.linearVelocity = velocity;
        }
    }

    private Vector3 GetCameraForward(Camera mainCamera)
    {
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera mainCamera)
    {
        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * characterData.JumpForce, ForceMode.Impulse);
        }
    }

    private void DoPickUp(InputAction.CallbackContext context)
    {
        animator.Animator.SetTrigger("PickUp");
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        return Physics.Raycast(ray, 0.3f);
    }
}