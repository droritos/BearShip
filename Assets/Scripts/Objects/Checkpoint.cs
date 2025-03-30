using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public event UnityAction<Checkpoint> OnCheckpointEnter;

    private bool entered;
    
    [SerializeField] float wantedSize = 30f;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] private AudioClip sound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalInfo.PlayerTag))
        {
            OnCheckpointEnter?.Invoke(this);
            if (!entered)
            {
                SoundManager.Instance.PlaySfxSound(sound,other.transform);
            }
            entered = true;
        }
    }
    private void OnValidate() // Easy way to adjust the collider size though out the inspector
    {
        if (boxCollider != null && boxCollider.size.x != wantedSize || boxCollider.size.z != wantedSize || boxCollider.size.y != 1)
        {
            boxCollider.size = new Vector3(wantedSize, 1, wantedSize); // For 3D colliders
        }
    }
}
