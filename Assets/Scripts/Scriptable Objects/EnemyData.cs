using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Movement data")]
    public float Speed = 4f;
     public float AngularSpeed = 360f;
     public float Acceleration = 6f;

    [Header("Knockback data")]
    public float KnockbackForce = 10f;
    public float BounceForce = 5f;
    public float KnockbackDuration = 0.5f;



}
