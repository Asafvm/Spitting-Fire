using System;
using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Acts as health for a GameObject. 1 hit kills
/// </summary>
public class Target : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionEffect;
    
    private bool isAlive = true;

    public UnityEvent OnDeath;

    public bool IsAlive() => isAlive;

    private IEnumerator Die()
    {

        //play explosion
        ParticleSystem ps = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        ps.transform.SetParent(transform, true);
        yield return new WaitForSeconds(1f);
        //notify other scripts
        OnDeath?.Invoke();
        //notify manager
        FindObjectOfType<SessionManager>().TargetDestroyed(tag);
  

    }
    public void Hit()
    {
        if(isAlive)
        {
            isAlive = false;
            StartCoroutine(Die());
        }
        
    }
}
