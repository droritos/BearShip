using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    private static int levelCounter;
    
    private Dictionary<int, string> levelNames;
    
    [SerializeField] private UIManager UIManager;
    
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
        levelNames = new Dictionary<int, string>();
        levelNames[0] = "Floating Isles";
        levelNames[1] = "Boom Boom Beach";
        levelNames[2] = "Lazy Forest";
    }
    
    private void Start()
    {
        LoadSettingsData();

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        
        UIManager.UpdateLevel(levelNames[levelCounter]);
        
        foreach (Artifact artifact in artifacts)
        {
            artifact.OnPickUpActionEvent += AddScore;
        }
    }

    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        UIManager.UpdateScore();
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
