using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy GameObject after a delay
/// </summary>
public class DestroyDelay : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
