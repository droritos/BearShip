using UnityEngine;
using UnityEngine.EventSystems;

public static class GlobalInfo
{
    public const string PlayerTag = "Player";
    public const string FallBoxTag = "FallBox";
    public const string Score = "Score";
    public const string JumpAnimation = "Jump";
    public const string AttackAnimation = "Attack";
    public const string TriggerName = "PickUp";
    public const string SavePath = "/savefile.json";
    public const string SpeedAnimation = "Speed";
    public const string WorldMeshes = "Mesh";

    #region << Level Names >>
    public const string Level1Name = "Floating Isles";
    public const string Level2Name = "Boom Boom Beach";
    public const string Level3Name = "Lazy Forest";
    #endregion




}
public enum SoundMixerType
{
    Master,
    SFX,
    SoundTrack,
    UI
}