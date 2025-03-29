using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    [Header("UI")]
    //[SerializeField] private UIManager uiManagerPrefab;
    [SerializeField] UIManager _uiManager;

    [Header("Player Belongings")]
    public CinemachineFreeLook FreeLookCamera;
    [SerializeField] PlayerManager playerManager;
    public PlayerManager PlayerManager { get { return playerManager; } }
    [SerializeField] Settings settings;

    [Header("Scene Belongings")]
    [SerializeField] SceneManager sceneManager;
    [SerializeField] private List<Artifact> artifacts;
    private Dictionary<int, string> _levelNames;
    private static int _levelCounter;

    protected override void Awake() // Overriding cuz of MonoSingleton already using Awake
    {
        base.Awake();
        //Let's hold a scene manager that has a number for each level and that way we can get the name of it. We will pass these lines to him as well.
        _levelNames = new Dictionary<int, string>();
        _levelNames[0] = "Floating Isles";
        _levelNames[1] = "Boom Boom Beach";
        _levelNames[2] = "Lazy Forest";


        //_uiManager = Instantiate(uiManagerPrefab);
        _uiManager.AssignActionAsset(PlayerManager.ThirdPersonController.PlayerActionAssets);
    }

    private void Start()
    {
        LoadSettingsData();
        playerManager.FallingBehaviour.OnFallingFromWorld += sceneManager.HandleFalling;

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        _uiManager?.UpdateLevel(_levelNames[_levelCounter]);
        
        foreach (Artifact artifact in artifacts)
        {
            artifact.OnPickUpActionEvent += AddScore;
            artifact.OnPickUpActionEvent += playerManager.Followers.AddFollower;
        }

    }

    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        _uiManager.UpdateScore();
    }
    
    private void LoadSettingsData()
    {
        if (!settings)
            Debug.Log("Settings Script Is Null!!");

        FreeLookCamera.m_XAxis.m_MaxSpeed = settings.DataToSave.MouseSensitivity;
        // More setting can loaded here
    }
    //private void ReturnToStartPoint()
    //{
    //    playerManager.FallingBehaviour.OnFallingFromWorld += sceneManager.HandleFalling;
    //    //playerManager.FallingBehaviour.ResetFallingState();
    //}
    private void OnApplicationQuit()
    {
        settings.SaveSettings();
    }

    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
    }
}
