using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BearCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bearText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bearText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
