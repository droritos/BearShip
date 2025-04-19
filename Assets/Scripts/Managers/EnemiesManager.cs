using System.Collections.Generic;
using UnityEngine;
public class EnemiesManager : MonoBehaviour
{
    public bool Pause { get; set; }

    [SerializeField] private List<EnemyManager> enemies;
    [SerializeField] private List<Obstacle> Obstacles;

    private void Start()
    {
        foreach (EnemyManager enemy in enemies)
        {
            enemy.OnCollisionEventAction += PushPlayer;
        }

        foreach (var obstacle in Obstacles)
        {
            obstacle.OnCollisionActionEvent += PushPlayer;
        }
    }

    private void Update()
    {
        if(Pause) return;

        foreach (EnemyManager enemy in enemies)
        {
            enemy.UpdateMe();
        }
    }
    public void PushPlayer(Vector3 direction)
    {
        RumbleManager.Instance.RumblePulse(0.5f,2f,0.25f);
        GameManager.Instance.PlayerManager.ThirdPersonController.AddForce(direction);
    }

    #region << Validation >> 
    private void OnValidate()
    {
        AddObjectsToList(enemies);
        AddObjectsToList(Obstacles);
    }
    private void AddObjectsToList<T>(List<T> list) where T : Object
    {
        list.Clear();

        if (list != null && list.Count == 0 || list[0] == null || list[list.Count - 1] == null)
        {
            T[] objects = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var obj in objects)
            {
                list.Add(obj);
            }
        }
    }
    #endregion
}
