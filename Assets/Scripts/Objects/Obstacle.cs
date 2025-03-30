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
    [SerializeField] private AudioClip hitSound;

    public event UnityAction<Vector3> OnCollisionActionEvent; 
    void Start()
    {
        this.speed = data.Speed;
        this.range = data.Range;
        exitPos = transform.position;
        direction = new Vector3(1, 0, 0);
        
    }
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
        if (other.gameObject.CompareTag(GlobalInfo.PlayerTag))
        {
            SoundManager.Instance.PlaySfxSound(hitSound, transform);
            OnCollisionActionEvent?.Invoke(other.transform.forward * -1 * 10 + new Vector3(0,1,0));
        }
    }
}
