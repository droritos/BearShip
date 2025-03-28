using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    private Vector3 direction; //Rotate the asset to determine direction
    private Vector3 exitPos;

    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private ObstacleData data;

    public event UnityAction<Vector3> OnCollisionActionEvent; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.speed = data.Speed;
        this.range = data.Range;
        exitPos = transform.position;
        direction = new Vector3(1, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((exitPos - transform.position).magnitude < range)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            exitPos = transform.position;
            direction.x *= -1;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCollisionActionEvent?.Invoke(new Vector3(direction.x * 10,1,0));
        }
    }
}
