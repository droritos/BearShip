using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MovementForce = 1f;
    public float JumpForce = 5f;
    public float MaxSpeed = 5f;
}
