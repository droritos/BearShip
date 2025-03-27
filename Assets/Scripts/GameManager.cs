using UnityEngine;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] Settings settings;
    [SerializeField] CinemachineFreeLook freeLookCamera;
    public CinemachineFreeLook FreeLookCamera { get { return freeLookCamera; } }


    private void Start()
    {
        LoadSettingsData();
    }

    private void LoadSettingsData()
    {
        FreeLookCamera.m_XAxis.m_MaxSpeed = settings.DataToSave.MouseSensitivity;
        // More setting can loaded here
    }

    private void OnApplicationQuit()
    {
        settings.SaveSettings();
    }
}
