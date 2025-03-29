using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToLevel(int levelIndex = 1) // Main Menu Scene is index 0
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game And Save Setttings");
        GameManager.Instance.Settings.SaveSettings();
        Application.Quit();
    }
}
