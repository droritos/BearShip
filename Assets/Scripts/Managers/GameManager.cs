using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    private static int _levelCounter;
    
    private Dictionary<int, string> _levelNames;
    
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private ThirdPersonController playerMovement;
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private List<Artifact> artifacts;
    [SerializeField] Settings settings;

    public ThirdPersonController PlayerMovement
    {
        get { return playerMovement;}
    }

    public CinemachineFreeLook FreeLookCamera
    {
        get { return freeLookCamera; } 
    }

    protected override void Awake()
    {
        base.Awake();
        //Let's hold a scene manager that has a number for each level and that way we can get the name of it. We will pass these lines to him as well.
        _levelNames = new Dictionary<int, string>();
        _levelNames[0] = "Floating Isles";
        _levelNames[1] = "Boom Boom Beach";
        _levelNames[2] = "Lazy Forest";
    }
    
    private void Start()
    {
        LoadSettingsData();

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        
        uiManager.UpdateLevel(_levelNames[_levelCounter]);
        
        foreach (Artifact artifact in artifacts)
        {
            artifact.OnPickUpActionEvent += AddScore;
        }
    }

    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        uiManager.UpdateScore();
    }

    private void LoadSettingsData()
    {
        if (!settings)
            Debug.Log("Settings Script Is Null!!");

        FreeLookCamera.m_XAxis.m_MaxSpeed = settings.DataToSave.MouseSensitivity;
        // More setting can loaded here
    }

    private void OnApplicationQuit()
    {
        settings.SaveSettings();
    }

}
