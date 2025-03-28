using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] MenuHandler menuHandler;
    

    [SerializeField] private TextMeshProUGUI scoreCounter;
    [SerializeField] private TextMeshProUGUI levelName;
    
    [SerializeField] private PopUpsHandler popUpsHandler;

    private ThirdPersonActionAsset _playerActionAssets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_playerActionAssets.Player.Pause.started += menuHandler.OpenPauseMenu;
        if (PlayerPrefs.HasKey("Score"))
        {
            scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("Score", 0);
            scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
        }
        //_playerActionAssets.Enable();
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
    //public void OpenPauseMenu(InputAction.CallbackContext context)
    //{
    //    menuHandler.OptionsMenu.SetActive(!menuHandler.OptionsMenu.activeSelf);

    //    if (menuHandler.OptionsMenu.activeSelf)
    //        Time.timeScale = 0f;
    //    else
    //        Time.timeScale = 1.0f;

    //    // Invoke(bool activeSelf) ??
    //}
}
