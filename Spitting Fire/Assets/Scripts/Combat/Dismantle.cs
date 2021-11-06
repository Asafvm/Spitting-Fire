using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismantle : MonoBehaviour
{
    [SerializeField] List<GameObject> rigParts;
    [SerializeField] float explosionForce = 200f;
    [SerializeField] float explosionRadius = 10f;



    public void DismantleRig()
    {
        Debug.Log($"Dismanteling {rigParts.Count} parts on {name}");
        foreach(GameObject go in rigParts)
        {
            if(TryGetComponent(out Rigidbody rb))
            {
                go.AddComponent<BoxCollider>();
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            else
            {
                go.AddComponent<BoxCollider>();
                rb = go.AddComponent<Rigidbody>();
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

        }
    }
}
