using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;

public class WinningScene : MonoBehaviour
{
    private int bearsAmount;
    private Bounds bounds;
    private quaternion rotation;

    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject bear;
    [SerializeField] private Collider collider;
    [SerializeField] private UIManager UIManager;
    [SerializeField] TextMeshProUGUI bearText;

    private const string _score = "Score";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = collider.bounds;

        if (PlayerPrefs.HasKey(_score))
        {
            bearsAmount = PlayerPrefs.GetInt(_score);

            bearText.text = bearsAmount.ToString();

            StartCoroutine(SpawnBear());

        }
        else
        {
            Debug.Log("Player Pref " + _score + " Not found");
        }

    }
    private void Update()
    {
        EnsureButtonSelected();

    }
    private IEnumerator SpawnBear()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 pos = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.center.y - 0.6f, Random.Range(bounds.min.z, bounds.max.z));
        rotation = Quaternion.Euler(0, Random.Range(120,235), 0);
        Instantiate(bear, pos, rotation);
        if (bearsAmount > 0)
        {
            bearsAmount--;
            StartCoroutine(SpawnBear());
            
            bearText.text = bearsAmount.ToString();
        }
    }

    private void EnsureButtonSelected()
    {
        if (firstButton != null && EventSystem.current != null)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
                Debug.Log("startButton has been re-selected.");
            }
        }
    }

}
