using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPausable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private List<Transform> patrolPositions;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyDetection detection;

    private int currentPatrolPoint;
    private bool _isChasing;
    private Transform target;

    private bool _pause;
    
    void Start()
    {
        _isChasing = false;
        currentPatrolPoint = 0;
        SetNewDestination();
        agent.speed = data.Speed;
        agent.acceleration = data.Acceleration;
        agent.angularSpeed = data.AngularSpeed;

        detection.OnTargetDetectedEventAction += ChaseTargetListener;
        detection.OnTargetEscapedEventAction += StopChasing;
    }
    public void EnemyUpdate()
    {
        if(_pause) return;

        if(agent.remainingDistance <= 0.5f && !agent.isStopped && !_isChasing)
        {
            SetNewDestination();
        }
        else if (_isChasing)
        {
            ChaseTarget(target);
        }
    }

    private void SetNewDestination()
    {
        // Apply Walking Animation
        
        if (currentPatrolPoint >= patrolPositions.Count)
        {
            currentPatrolPoint = 0;
        }
        
        agent.SetDestination(patrolPositions[currentPatrolPoint++].position);
    }

    private void ChaseTargetListener(Transform target)
    {
        this.target = target;
        _isChasing = true;
    }

    private void ChaseTarget(Transform target)
    {
        agent.SetDestination(target.position);
        agent.speed = 15;
    }

    private void StopChasing()
    {
        _isChasing = false;
        target = null;
        agent.speed = 5;
    }

    public bool PauseState(bool isPaused)
    {
        _pause = isPaused;
        return _pause;
    }
}
