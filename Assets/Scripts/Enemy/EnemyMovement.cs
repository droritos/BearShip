using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private List<Transform> patrolPositions;
    [SerializeField] private NavMeshAgent agent;

    private int currentPatrolPoint;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPatrolPoint = 0;
        SetNewDestination();
        agent.speed = data.Speed;
        agent.acceleration = data.Acceleration;
        agent.angularSpeed = data.AngularSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= 0.5f && !agent.isStopped)
        {
            SetNewDestination();
        }
    }

    public void SetNewDestination()
    {
        if (currentPatrolPoint >= patrolPositions.Count)
        {
            currentPatrolPoint = 0;
        }
        
        agent.SetDestination(patrolPositions[currentPatrolPoint++].position);
    }
}
