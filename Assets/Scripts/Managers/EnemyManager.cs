using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyBehavior> enemies;
    [SerializeField] private List<Obstacle> Obstacles;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (EnemyBehavior enemy in enemies)
        {
            enemy.OnCollisionEventAction += PushPlayer;
        }

        foreach (var obstacle in Obstacles)
        {
            obstacle.OnCollisionActionEvent += PushPlayer;
        }
    }

    public void PushPlayer(Vector3 direction)
    {
        GameManager.Instance.PlayerMovement.AddForce(direction * 10);
    }
}
