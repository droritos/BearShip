using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private int currentLevel;
    
    [SerializeField] private bool isBack;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBack)
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
        else
        {
            SceneManager.LoadScene(currentLevel - 1);
        }
    }
}
