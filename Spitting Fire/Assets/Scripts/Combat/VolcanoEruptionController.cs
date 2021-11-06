using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VolcanoEruptionController : MonoBehaviour
{
    [SerializeField] VisualEffect lavaEffect;
    [SerializeField] float eriptionDelay = 1f;
    [SerializeField] float rockShootAngle = 60f;
    [SerializeField] float rockShootForce = 300f;
    [SerializeField] GameObject magmaRock;
    [SerializeField] Transform spawnPoint;

    private GameObject playerRef;
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        InvokeRepeating(nameof(Eruption), 10, 5);
    }

    private void Eruption()
    {
        StartCoroutine(Erupt());
    }

    private IEnumerator Erupt()
    {
        //save default
        float particles = lavaEffect.GetFloat("_nParticles");
        Vector3 gravity = lavaEffect.GetVector3("_gParticles");
        //show effect
        lavaEffect.SetFloat("_nParticles", 20000);
        lavaEffect.SetVector3("_gParticles", Vector3.up * 10);
        //spawn rocks
        ShootRock();
        yield return new WaitForSeconds(eriptionDelay);
        //restore effect after short delay
        lavaEffect.SetFloat("_nParticles", particles);
        lavaEffect.SetVector3("_gParticles", gravity);
    }

    private void ShootRock()
    {
        //get start and target points
        GameObject rock = Instantiate(magmaRock, spawnPoint.position, Quaternion.identity);
        Vector3 playerDirection = (playerRef.transform.position - transform.position).normalized;
        //calculate forces directions
        float yForce = Mathf.Abs(Mathf.Sin(rockShootAngle));
        Vector2 xzForce = new Vector2(playerDirection.x*Mathf.Cos(rockShootAngle), playerDirection.z * Mathf.Cos(rockShootAngle));
        //add force to direction
        Vector3 shootForce = new Vector3(xzForce.x, yForce, xzForce.y) * rockShootForce;
        //shoot
        rock.GetComponent<Rigidbody>().AddForce(shootForce * rockShootForce);
    }
}
