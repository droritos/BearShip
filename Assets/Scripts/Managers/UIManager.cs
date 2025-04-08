using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] MenuHandler menuHandler;
    public event UnityAction<bool> OnPause { add { menuHandler.OnPause += value; }
                                             remove { menuHandler.OnPause -= value; } }

    [SerializeField] private TextMeshProUGUI scoreCounter;
    [SerializeField] private TextMeshProUGUI levelName;
    
    [SerializeField] private PopUpsHandler popUpsHandler;

    private ThirdPersonActionAsset _playerActionAssets;

    void Start()
    {
        if (_playerActionAssets == null)
        {
            _playerActionAssets = GameManager.Instance.PlayerManager.ThirdPersonController.PlayerActionAssets;
        }
        if (PlayerPrefs.HasKey(GlobalInfo.Score))
        {
            UpdateScore();
        }
        else
        {
            PlayerPrefs.SetInt(GlobalInfo.Score, 0);
            UpdateScore();
        }
        SubscribeToPauseStarted();
        _playerActionAssets.Enable();
    }
    private void OnDisable()
    {
        _playerActionAssets.Player.Pause.started -= menuHandler.OpenPauseMenu;
    }

    private void SubscribeToPauseStarted()
    {
        _playerActionAssets.Player.Pause.started += menuHandler.OpenPauseMenu;
    }

    public void UpdateScore()
    {
        scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void UpdateLevel(string level) //We will call this each time we pass on to a new scene
    {
        levelName.text = level;
    }
    public void AssignActionAsset(ThirdPersonActionAsset inputActions)
    {
        _playerActionAssets = inputActions;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
