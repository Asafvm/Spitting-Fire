using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Control the player physical movement and rotation
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private ControlInputManager control;
    private AircraftRigController rigController;
    private Target self;
    private Terrain groundRef;
    private float gravityModifier = 2.4f;
    private bool isGrounded = true;
    private bool isBoosting = false;
    private float timeSinceStartedBoosting = float.MaxValue;
    

    [Header("Flight Speed")]
    [SerializeField] float thrustAcceleration = 1f;
    [SerializeField] float maxThrust = 80;
    public float thrustValue = 0;
    [SerializeField] float boostTime = 1f;
    [SerializeField] float timeBetweenBoosts = 10f;
    [Header("Turning Speed")]
    [SerializeField] float maxRollSpeed;
    [SerializeField] float maxYawSpeed;
    [SerializeField] float maxPitchSpeed;
    private float rollSpeed = 0;
    private float yawSpeed = 0;
    private float pitchSpeed = 0;
    [Header("Weapon Locations")]
    [SerializeField] ParticleSystem[] weapons;
    [Header("HUD Displays")]
    [SerializeField] Image thrustDisplay;
    [SerializeField] Image boostDisplay;
    [Header("Effects")]
    [SerializeField] TrailRenderer[] boostEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        control = GetComponent<ControlInputManager>();
        rigController = GetComponent<AircraftRigController>();
        self = GetComponent<Target>();
        groundRef = Terrain.activeTerrain;

    }
    private void FixedUpdate()
    {
        if (!self.IsAlive()) return;
        CalculateThrust();  // Control Thrust
        CalculateRotation();  // Move Elevator

        HandleBoost();
        HandleRigidbodyMovement();

    }
    private void Update()
    {
        if (!self.IsAlive()) return;
        CheckIfGrounded();
        HandleLandingGear();
        HandleFire();
        UpdateHudDisplay();
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timeSinceStartedBoosting += Time.deltaTime;
    }

    private void UpdateHudDisplay()
    {
        UpdateThrustDisplay();
        UpdateBoostDisplay();
    }

    private void UpdateBoostDisplay()
    {
        if (timeSinceStartedBoosting > timeBetweenBoosts)   //Green when boost is available
            boostDisplay.color = Color.green;

        else
        { 
            if (timeSinceStartedBoosting > boostTime)
            {
                boostDisplay.fillAmount = (timeSinceStartedBoosting-boostTime)/(timeBetweenBoosts-boostTime);
                boostDisplay.color = Color.red;
            }
            else
            {
                boostDisplay.fillAmount = Mathf.Clamp01(1 - timeSinceStartedBoosting / boostTime);
                boostDisplay.color = Color.green;
            }
                
        }
        
    }

    private void UpdateThrustDisplay()
    {
        float thrustPercent = thrustValue / maxThrust;
        thrustDisplay.fillAmount = thrustPercent;
        if (thrustPercent < .3f)
            thrustDisplay.color = Color.red;
        else
            thrustDisplay.color = Color.green;
    }

    private void HandleBoost()
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (timeSinceStartedBoosting < timeBetweenBoosts) return;
            isBoosting = true;
            timeSinceStartedBoosting = 0;
            StartCoroutine(Boost());
        }
        foreach (TrailRenderer effect in boostEffect)
            effect.emitting = isBoosting;
    }

    private IEnumerator Boost()
    {

        while(timeSinceStartedBoosting < boostTime)
        {
            Debug.Log($"Boosting for {timeSinceStartedBoosting}s");
            rb.AddForce(transform.forward * 30f);
            yield return null;
        }
        isBoosting = false;

        yield return new WaitForSeconds(timeBetweenBoosts - boostTime);


    }

    private void CheckIfGrounded()
    {
        isGrounded = transform.position.y - groundRef.SampleHeight(transform.position) < 2f;
    }

    private void HandleRigidbodyMovement()
    {
        float thrustPercent = thrustValue / maxThrust;
        rb.useGravity = thrustValue < maxThrust / 3f;   //turn off gravity when thrust at a decent speed
        transform.Rotate(pitchSpeed, yawSpeed * thrustPercent, -rollSpeed); //rotate the plane
        rb.AddForce(transform.forward * maxThrust * thrustPercent); //add thrust force
        rb.AddForce(-rb.velocity/2 + Physics.gravity/gravityModifier);    //add drag and gravity force
        
    }

    private void HandleLandingGear()
    {
        if(Input.GetKeyUp(KeyCode.L))
            rigController.TriggerLandingGear();
    }

    private void HandleFire()
    {
        bool isFiring = Input.GetButton("Fire1");


        foreach (ParticleSystem weapon in weapons)
        {
            ParticleSystem.EmissionModule emissionModule = weapon.emission;
            emissionModule.enabled = isFiring;
        }
        if (isFiring)
            GetComponent<SoundHandler>().Fire();
        else
            GetComponent<SoundHandler>().Stop();

    }



    private void CalculateRotation()
    {
        yawSpeed = control.yaw * maxYawSpeed * Time.deltaTime;  //allow rotating on ground (if plane is speeding)

        if (rb.useGravity && isGrounded) return;    //ignore other rotations on ground

        pitchSpeed = control.pitch * maxPitchSpeed * Time.deltaTime;    
        rollSpeed = control.roll * maxRollSpeed * Time.deltaTime;
        

    }

    private void CalculateThrust()
    {
        if (control.thrust != 0)
        {
            thrustValue = Mathf.MoveTowards(thrustValue, control.thrust * maxThrust, thrustAcceleration * Time.deltaTime);
            thrustValue = Mathf.Clamp(thrustValue, 0, maxThrust);
        }
        rigController.Propeller(thrustValue/maxThrust);
        
    }

    //if player collided with boxcolliders, destroy player
    //if player landed with wheels on the ground, ignore
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        foreach (ContactPoint contact in contacts)
            if (contact.thisCollider.name.Equals("Player"))
            {
                self.Hit();
                break;
            }
    }
}
