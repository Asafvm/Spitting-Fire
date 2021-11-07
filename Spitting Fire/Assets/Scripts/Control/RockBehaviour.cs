using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// change rock rotaion according to direction (help with aligning the particle effect rotation)
/// </summary>
public class RockBehaviour : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent < Rigidbody>();
    }
    void Update()
    {
        transform.LookAt(rb.velocity);
    }
}
