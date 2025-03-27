using UnityEngine;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] CinemachineFreeLook freeLookCamera;
    public CinemachineFreeLook FreeLookCamera { get { return freeLookCamera; } }


}
