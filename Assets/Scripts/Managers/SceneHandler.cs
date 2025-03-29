using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] Transform startingPoint;
    [SerializeField] float fallingCooldown;
    [SerializeField] float yOffset;

    public void HandleFalling(FallingBehaviour player)
    {
        player.gameObject.SetActive(false);
        StartCoroutine(FallingCooldown(player, fallingCooldown));
    }
    private IEnumerator FallingCooldown(FallingBehaviour player, float cd)
    {
        yield return new WaitForSeconds(cd);

        Vector3 returnPoint = startingPoint.position;
        returnPoint.y += yOffset;

        player.transform.position = returnPoint;
        player.gameObject.SetActive(true);

        StartCoroutine(ResetFallFlag(player, 1.5f));
    }

    private IEnumerator ResetFallFlag(FallingBehaviour player,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.ResetFallingState();
    }

    private void SceneTransition()
    {
        SceneManager.LoadScene(1);
    }
}
