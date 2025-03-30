using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WinningScene : MonoBehaviour
{
    private int bearsAmount;
    private Bounds bounds;
    private quaternion rotation;
    
    [SerializeField] private GameObject bear;
    [SerializeField] private Collider collider;
    [SerializeField] private UIManager UIManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bounds = collider.bounds;
        if (PlayerPrefs.HasKey("Score"))
        {
            bearsAmount = PlayerPrefs.GetInt("Score");
            StartCoroutine(SpawnBear());
        }
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
        }
    }
}
