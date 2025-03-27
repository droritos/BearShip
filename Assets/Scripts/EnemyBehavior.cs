using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    public event UnityAction<float> onCollisionEventAction; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Add force to the player when colliding with him
    public void OnCollisionEnter(Collision other)
    {
        onCollisionEventAction.Invoke(data.PushStreangth);
    }
}
