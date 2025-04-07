using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    public void GoToLevel(int levelIndex = 1) // Main Menu Scene is index 0
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    private void Update()
    {
        EnsureButtonSelected();
    }
    private void EnsureButtonSelected()
    {
        if (startButton != null && EventSystem.current != null)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(startButton);
                Debug.Log("startButton has been re-selected.");
            }
        }
    }



}
