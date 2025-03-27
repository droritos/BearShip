using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyBehavior> enemies;
    [SerializeField] private ThirdPersonController playerMovement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (EnemyBehavior enemy in enemies)
        {
            enemy.onCollisionEventAction += PushPlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushPlayer(float pushAmount)
    {
        
    }
}
