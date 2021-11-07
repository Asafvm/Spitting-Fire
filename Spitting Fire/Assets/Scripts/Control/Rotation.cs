using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Rotate GameObject around Vector3.up with a random factor for variation
/// </summary>
public class Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private float randomSpeed;
    private float randomDirection;

    private void Start()
    {
        randomSpeed = Random.Range(.5f , 2f);
        randomDirection = Mathf.RoundToInt( Random.Range(-1f, 1f));

    }
    // Update is called once per frame
    void Update()
    {
            transform.Rotate(Vector3.up, rotationSpeed* randomDirection * randomSpeed* Time.deltaTime);
    }
}
