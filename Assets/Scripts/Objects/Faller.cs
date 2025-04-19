using UnityEngine;
using UnityEngine.Events;

public class Faller : MonoBehaviour
{

    [SerializeField] BoxCollider boxCollider;
    [SerializeField] float wantedX = 1f;
    [SerializeField] float wantedY = 1f;
    [SerializeField] float wantedZ = 1f;

    //private void OnValidate() // Easy way to adjust the collider size throughout the inspector
    //{
    //    if (boxCollider != null &&
    //        (boxCollider.size.x != wantedX ||
    //         boxCollider.size.z != wantedZ))
    //    {
    //        boxCollider.size = new Vector3(wantedX, 1, wantedZ);
    //    }
    //}
}
