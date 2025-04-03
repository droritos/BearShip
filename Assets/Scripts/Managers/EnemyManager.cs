using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyBehavior> enemies;
    [SerializeField] private List<Obstacle> Obstacles;
    
    
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
        RumbleManager.Instance.RumblePulse(0.5f,2f,0.25f);
        GameManager.Instance.PlayerManager.ThirdPersonController.AddForce(direction);
    }
}
