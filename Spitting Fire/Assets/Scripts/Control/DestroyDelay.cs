using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
