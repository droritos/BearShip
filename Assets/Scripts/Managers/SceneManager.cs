using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    [SerializeField] float fallingCooldown;
    [SerializeField] float yOffset;

    public void HandleFalling(GameObject player)
    {
        player.SetActive(false);
        StartCoroutine(FallingCooldown(player, fallingCooldown));
    }

    private IEnumerator FallingCooldown(GameObject player, float cd)
    {
        yield return new WaitForSeconds(cd);

        Vector3 returnPoint = startingPoint.position;
        returnPoint.y += yOffset;

        player.transform.position = returnPoint;
        player.SetActive(true);
    }
}
