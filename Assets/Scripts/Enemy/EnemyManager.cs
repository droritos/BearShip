using UnityEngine;
using UnityEngine.Events;
public class EnemyManager : MonoBehaviour
{

    [SerializeField] EnemyBehavior enemybehavior;
    [SerializeField] EnemyDetection enemyDetection;
    [SerializeField] Enemy enemy;

    // Refernce the event
    public event UnityAction<Vector3> OnCollisionEventAction { add { enemybehavior.OnCollisionEventAction += value; }
                                                               remove { enemybehavior.OnCollisionEventAction -= value; } }

    public event UnityAction<Transform> OnTargetDetectedEventAction { add { enemyDetection.OnTargetDetectedEventAction += value; }
                                                               remove { enemyDetection.OnTargetDetectedEventAction -= value; } }


    public void UpdateMe()
    {
        enemy.EnemyUpdate();
    }

}
