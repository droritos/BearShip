using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private ThirdPersonController playerMovement;
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private List<Artifact> artifacts;
    
    public ThirdPersonController PlayerMovement
    {
        get { return playerMovement;}
    }

    public CinemachineFreeLook FreeLookCamera
    {
        get { return freeLookCamera; } 
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        
        foreach (Artifact artifact in artifacts)
        {
            artifact.OnPickUpActionEvent += AddScore;
        }
    }

    private void AddScore()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        Debug.Log(PlayerPrefs.GetInt("Score"));
    }
}
