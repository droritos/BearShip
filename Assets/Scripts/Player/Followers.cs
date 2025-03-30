using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Followers : MonoBehaviour
{
    public event UnityAction OnThreeBearsCollected;
    
    private float distanceFromPlayer = 1f;
    private int followersAmount;
    
    [SerializeField] private GameObject follower;
    
    void Start()
    {
        followersAmount = 0;
    }

    public void AddFollower()
    {
        GameObject bear = Instantiate(follower, transform);

        bear.transform.position += transform.forward * -1 * distanceFromPlayer * (followersAmount + 1);
        followersAmount++;

        if (followersAmount == 3)
        {
            OnThreeBearsCollected?.Invoke();
        }
    }
}
