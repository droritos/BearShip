using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Header("Checkpoints")]
    [SerializeField] Checkpoint startingCheckpoint;
    [SerializeField] List<Checkpoint> checkpoints;
    private Checkpoint _currentCheckpoints;

    [Header("Falling Data")]
    [SerializeField] float fallingCooldown;
    [SerializeField] float yOffset;
    private void Start()
    {
        _currentCheckpoints = startingCheckpoint;
        SubscribeToCheckpointEvents();
    }
    public void HandleFalling(FallingBehaviour player)
    {
        player.gameObject.SetActive(false);
        StartCoroutine(FallingCooldown(player, fallingCooldown));
    }
    private IEnumerator FallingCooldown(FallingBehaviour player, float cd)
    {
        Debug.Log("I am falling");
        yield return new WaitForSeconds(cd);

        Vector3 returnPoint = _currentCheckpoints.transform.position;
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
    private void SubscribeToCheckpointEvents()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.OnCheckpointEnter += UpdateCurrentCheckpoint;
        }
    }
    private void UpdateCurrentCheckpoint(Checkpoint checkpoint)
    {
        _currentCheckpoints = checkpoint;
    }

    private void SceneTransition()
    {
        SceneManager.LoadScene(1);
    }
}
