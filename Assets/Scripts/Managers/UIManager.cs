using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCounter;

    [SerializeField] private TextMeshProUGUI levelName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void UpdateScore()
    {
        scoreCounter.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void UpdateLevel(string level) //We will call this each time we pass on to a new scene
    {
        levelName.text = level;
    }
}
