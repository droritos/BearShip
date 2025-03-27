using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
     public float Speed = 4f;
     public float AngularSpeed = 360f;
     public float Acceleration = 6f;
 }
