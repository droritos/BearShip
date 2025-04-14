using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Managers")]
    [SerializeField] UIManager _uiManager;
    [SerializeField] ControllerManager _controllerManagerPrefab;
    [SerializeField] EnemiesManager _enemiesManager;

    [Header("Player Belongings")]
    [SerializeField] PlayerManager playerManager;
    public CinemachineFreeLook FreeLookCamera;
    public PlayerManager PlayerManager { get { return playerManager; } }
    [SerializeField] Settings settings;
    public Settings Settings { get { return settings; } }

    [Header("Scene Belongings")]
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] private List<Artifact> artifacts;
    [SerializeField] List<IPausable> pausableObjects;

    private Dictionary<int, string> _levelNames;
    private static int _levelCounter;

    protected override void Awake() // Overriding cuz of MonoSingleton already using Awake
    {
        base.Awake();

        CreateControllerManager();

        // Note : Let's hold a scene manager that has a number for each level and that way we can get the name of it. We will pass these lines to him as well.
        _levelNames = new Dictionary<int, string>();
        _levelNames[0] = GlobalInfo.Level1Name;
        _levelNames[1] = GlobalInfo.Level2Name;
        _levelNames[2] = GlobalInfo.Level3Name;

        if (playerManager != null)
        {
            _uiManager.AssignActionAsset(PlayerManager.ThirdPersonController.PlayerActionAssets);
        }

        _uiManager.OnPausedMenu += HandlePause;

        Settings.ApplyVSync();
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

        if (!PlayerPrefs.HasKey(GlobalInfo.Score))
        {
            PlayerPrefs.SetInt(GlobalInfo.Score, 0);
        }

        _uiManager.OnCheckpointClicked += Checkpoint;
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
    private void OnDisable()
    {
        settings.SaveSettings();
        _uiManager.OnPausedMenu -= HandlePause;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeThreshold()
    {
        //PlayerManager.FallingBehaviour.ChangeThreshold(PlayerManager.transform.position.y);
    }
    private void AddScore()
    {
        PlayerPrefs.SetInt(GlobalInfo.Score, PlayerPrefs.GetInt(GlobalInfo.Score) + Random.Range(3,12));
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
    private void CreateControllerManager()
    {
        // Check if a ControllerManager exists in the scene
        if (FindAnyObjectByType<ControllerManager>() == null)
        {
            // Instantiate a new ControllerManager from the prefab
            ControllerManager controllerManager = Instantiate(_controllerManagerPrefab);

            controllerManager.OnGamepadDisconected += _uiManager.GamepadDisconnected;
        }
    }
    private void HandlePause(bool pauseState)
    {
        // Apply pause state on player elements : camare , movement
        PlayerManager.PauseState(pauseState);
        FreeLookCamera.enabled = !pauseState;

        _enemiesManager.Pause = pauseState;
    }
    private void Checkpoint()
    {
        sceneHandler.HandleFalling(PlayerManager.FallingBehaviour);
    }

    private void OnValidate()
    {
        if(!_enemiesManager)
            _enemiesManager = GetComponentInChildren<EnemiesManager>();
    }
}
