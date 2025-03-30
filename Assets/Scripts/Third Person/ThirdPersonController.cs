using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ThirdPersonController : MonoBehaviour
{
    public ThirdPersonActionAsset PlayerActionAssets {  get; private set; }
    public int JumpCount
    {
        get => _jumpCount;
        set => _jumpCount = value;
    }

    [Header("Serialize Field")]
    [SerializeField] ThirdPersonAnimation thirdPersonAnimation;
    [SerializeField] Camera _mainCamera;
    private InputAction _move;

    [Header("Movement")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] CharacterData characterData;
    private Vector3 _forceDirection = Vector3.zero;
    private Vector2 _moveInput;
    private bool _isWalking = false;
    private int _jumpCount;
    private int _currentJumpCount;

    [Header("Sounds")] 
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private List<AudioClip> walkSounds;
    private Coroutine _walkingSoundCoroutine;


    private void Awake()
    {
        _jumpCount = 1;
        PlayerActionAssets = new ThirdPersonActionAsset();
    }
    private void OnEnable()
    {
        // Subscribe to the Jump.started event
        _move = PlayerActionAssets.Player.Move;
        PlayerActionAssets.Player.Jump.started += DoJump;
        PlayerActionAssets.Player.Enable();
    }
    private void OnDisable()
    {
        // Unsubscribe from the Jump.started event
        PlayerActionAssets.Player.Jump.started -= DoJump;
        PlayerActionAssets.Player.Disable();
    }
    private void FixedUpdate()
    {
        HandleMove();

        // Apply exponential gravity
        if (_rigidbody.linearVelocity.y < 0) // Falling
        {
            _rigidbody.linearVelocity += Vector3.up * Physics.gravity.y * 2 * Time.fixedDeltaTime;
            //IsAboutToLand();
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
    public void AddForce(Vector3 direction)
    {
        _rigidbody.linearVelocity = Vector3.zero;

        StartCoroutine(ApplyKnockbackForce(direction));
    }
    private void HandleMove()
    {
        _moveInput = _move.ReadValue<Vector2>();

        _forceDirection += _moveInput.x * characterData.MovementForce * GetCameraRight(_mainCamera);
        _forceDirection += _moveInput.y * characterData.MovementForce * GetCameraForward(_mainCamera);

        HandleMovingSound();

        _rigidbody.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;
    }

    private void HandleMovingSound()
    {
        if (_moveInput.sqrMagnitude > 0.01f) // Check if the player is moving
        {
            if (!_isWalking) // Play sound only when starting to walk
            {
                _isWalking = true;
                _walkingSoundCoroutine = StartCoroutine(PlayWalkingSound(walkSounds, transform, 0.5f));
            }
        }
        else
        {
            _isWalking = false; // Stop tracking walking if movement stops
            if (_walkingSoundCoroutine != null)
            {
                StopCoroutine(_walkingSoundCoroutine);
                _walkingSoundCoroutine = null;
            }
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
        right.y = 0;    // Should be X? 
        return right.normalized;
    }

    // Updated DoJump method to accept CallbackContext
    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
            _currentJumpCount = _jumpCount;
        if (_currentJumpCount > 0)
        {
            thirdPersonAnimation.Animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector3.up * characterData.JumpForce, ForceMode.Impulse);
            if(jumpSound != null)
                SoundManager.Instance.PlaySfxSound(jumpSound, transform);
            _currentJumpCount--;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
        {
            return true;
        }
        return false;
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
    
    private IEnumerator PlayWalkingSound(List<AudioClip> clipArray,Transform pos,float interval)
    {
        while (_isWalking)
        {
            SoundManager.Instance.PlayRandomSfxSound(clipArray, pos);
            yield return new WaitForSeconds(interval); // Wait before playing next step sound
        }
    }
    private IEnumerator ApplyKnockbackForce(Vector3 direction)
    {
        // Store original drag
        float originalDrag = _rigidbody.linearDamping;

        _rigidbody.linearDamping = 0.2f;

        _rigidbody.AddForce(direction, ForceMode.Impulse);

        yield return new WaitForSeconds(0.3f);

        // Restore original drag
        _rigidbody.linearDamping = originalDrag;
    }
}