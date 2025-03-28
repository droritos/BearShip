using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCounter;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI levelName;
    
    [SerializeField] private PopUpsHandler popUpsHandler;

    private ThirdPersonActionAsset _playerActionAssets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerActionAssets.Player.Pause.started += OpenPauseMenu;
        scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();

        _playerActionAssets.Enable();
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
    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        // Invoke!!
    }
}
