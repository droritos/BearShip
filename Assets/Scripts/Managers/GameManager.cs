using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
    public Settings Settings { get { return settings; } }

    [Header("Scene Belongings")]
    [SerializeField] SceneHandler sceneHandler;
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
        if (playerManager != null)
        {
            _uiManager.AssignActionAsset(PlayerManager.ThirdPersonController.PlayerActionAssets);
        }

        ApplyVSync();
    }

    private static void ApplyVSync()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        Time.fixedDeltaTime = 0.01667f; 
    }

    private void Start()
    {
        if (settings != null)
        {
            LoadSettingsData();
        }

        if (playerManager != null)
        {
            playerManager.FallingBehaviour.OnFallingFromWorld += sceneHandler.HandleFalling;
            playerManager.Followers.OnThreeBearsCollected += AddJumps;
        }

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        _uiManager?.UpdateLevel(_levelNames[_levelCounter]);

        if (artifacts.Count > 0)
        {
            foreach (Artifact artifact in artifacts)
            {
                artifact.OnPickUpActionEvent += AddScore;
                artifact.OnPickUpActionEvent += playerManager.Followers.AddFollower;
            }   
        }
    }
    private void Update()
    {
        GoToEndScene();
    }
    public void BackToMainMenu()
    {
        Debug.Log("Back to Main Menu");
    }
    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        _uiManager.UpdateScore();
    }
    
    private void LoadSettingsData()
    {
        if (settings != null)
        {
            settings.LoadVolumes();
            FreeLookCamera.m_XAxis.m_MaxSpeed = settings.DataToSave.MouseSensitivity;
            if (settings.SensitivitySetting != null)
            {
                settings.SensitivitySetting.UpdateDisplay(settings.DataToSave.MouseSensitivity);   
            }
        }
        // More setting can loaded here
    }
    private void OnDisable()
    {
        settings.SaveSettings();
    }

    private void AddJumps()
    {
        playerManager.ThirdPersonController.JumpCount++;
    }
    private void GoToEndScene()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.SetInt(GlobalInfo.Score, 55);
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }
}
