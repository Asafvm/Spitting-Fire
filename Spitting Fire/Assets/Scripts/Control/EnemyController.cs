using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Typical enemy behaviour. point at player. shoot player.
/// </summary>
public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Target self;
    [SerializeField] float attackRange = 20f;
    [SerializeField] ParticleSystem projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float destroyProjectileDelay = 4f;
    [SerializeField] Transform gunPoint;
    [SerializeField] float lookAheadFactor = 15f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        self = GetComponentInParent<Target>();
    }

    void Update()
    {
        if (!self.IsAlive()) return;
        if (player == null)
        {
            Shoot(false);
            return;
        }
            
        if(Vector3.Distance(transform.position,player.transform.position) < attackRange)
        {
            RotateTowardsTarget();
            Shoot(true);
            return;
        }
        Shoot(false);
        
    }

    private void Shoot(bool activate)
    {
        if (!activate)
        {
            projectilePrefab.Stop();
            return;
        }
        GetComponentInParent<SoundHandler>().Fire();
        if (!projectilePrefab.isPlaying)
            projectilePrefab.Play();
    }

    private void RotateTowardsTarget()
    {

        Vector3 direction = (player.transform.position + player.transform.forward * lookAheadFactor) - transform.position ;
        transform.LookAt(player.transform.position);
    }

    //draw a sphere around each enemy to visualize attack radius
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.red;
    //}

}
