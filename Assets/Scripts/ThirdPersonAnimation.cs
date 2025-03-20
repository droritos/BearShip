using UnityEngine;

public class ThirdPersonAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator Animator { get { return _animator; } }

    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] CharacterData characterData;
    void Update()
    {
        _animator.SetFloat("Speed", Rigidbody.linearVelocity.magnitude / characterData.MaxSpeed);
    }
}
