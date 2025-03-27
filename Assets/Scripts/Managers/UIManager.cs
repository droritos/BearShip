using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void UpdateScore()
    {
        scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
    }
    
}
