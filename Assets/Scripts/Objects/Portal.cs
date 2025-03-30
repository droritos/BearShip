using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool isBack;
    private int _currentLevel;

    void Start()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBack)
        {
            SceneManager.LoadScene(_currentLevel + 1);
        }
        else
        {
            SceneManager.LoadScene(_currentLevel - 1);
        }
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
