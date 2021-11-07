using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Take apart a GameObject by putting Rigidbody on children and applying force
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Dismantle : MonoBehaviour
{
    [SerializeField] List<GameObject> rigParts;
    [SerializeField] float explosionForce = 200f;
    [SerializeField] float explosionRadius = 10f;
    private Rigidbody parentRB;

    private void Awake()
    {
        parentRB = GetComponent<Rigidbody>();
    }


    public void DismantleRig()
    {
        Debug.Log($"Dismanteling {rigParts.Count} parts on {name}");
        foreach(GameObject go in rigParts)
        {
      
            
                go.AddComponent<BoxCollider>();
                Rigidbody rb = go.AddComponent<Rigidbody>();
                rb.velocity = parentRB.velocity;
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            

        }
    }
}
