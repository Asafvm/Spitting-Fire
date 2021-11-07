using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If bullet hits a Target component, active Hit()
/// otherwise, play hit effect and disappear
/// </summary>
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    private List<ParticleCollisionEvent> events;
    private ParticleSystem bullet;
    private void Start()
    {
        events = new List<ParticleCollisionEvent>();
        bullet = GetComponent<ParticleSystem>();
    }


    private void OnParticleCollision(GameObject other)
    {
        if (bullet == null) return;
        int numOfColisions = bullet.GetCollisionEvents(other, events);

        if (other.TryGetComponent(out Target target))
            target.Hit();

        else
        {
            foreach(ParticleCollisionEvent point in events)
                Destroy(Instantiate(hitEffect, point.intersection, hitEffect.transform.rotation),.2f);
        }

        
    }

    
}
