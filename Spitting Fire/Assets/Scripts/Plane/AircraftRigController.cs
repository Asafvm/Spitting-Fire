using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class AircraftRigController : MonoBehaviour
{
    [Header("Rudder")]
    [SerializeField] Transform rudder;
    [SerializeField] float rudderMaxAngle;
    [Header("Propeller")]
    [SerializeField] Transform propeller;
    [SerializeField] AudioSource propellerAudio;
    [Header("Ailerons")]
    [SerializeField] Transform aileronLeft;
    [SerializeField] Transform aileronRight;
    [SerializeField] float aileronMaxAngle;
    [Header("Flaps")]
    [SerializeField] Transform flapLeft;
    [SerializeField] Transform flapRight;
    [SerializeField] float flapMaxAngle;
    [Header("Elevators")]
    [SerializeField] Transform elevatorLeft;
    [SerializeField] Transform elevatorRight;
    [SerializeField] float elevatorMaxAngle;
    [Header("Wheels")]
    [SerializeField] Transform wheelLeft;
    [SerializeField] Transform wheelRight;
    [SerializeField] float wheelMaxAngle;

    private ControlInputManager control;
    private bool landingGearDeployed = true;
    private Coroutine landingGearCoroutine;
    private List<Transform> planeParts;

    private void Awake()
    {
        control = GetComponent<ControlInputManager>();
        
    }
    void Update()
    {
        Rudder();
        Flaps();
        Ailerons();
        Elevators();
    }

    #region Landing Gear

    public void TriggerLandingGear()
    {
        DeployLandingGear(!landingGearDeployed);
    }

    private void DeployLandingGear(bool deploy)
    {

        if (!(landingGearDeployed ^ deploy)) return;
        landingGearDeployed = deploy;

        if (landingGearCoroutine != null)
            StopCoroutine(landingGearCoroutine);
        if (deploy)
            LowerLandingGear();
        else
            RaiseLandingGear();

    }
    
    private void LowerLandingGear()
    {
        UpdateWheelsColiders(true);

        landingGearCoroutine = StartCoroutine(LandingGearStatus(wheelMaxAngle,0));

    }

    private void RaiseLandingGear()
    {
        UpdateWheelsColiders(false);

        landingGearCoroutine = StartCoroutine(LandingGearStatus(0,wheelMaxAngle));

    }

    private IEnumerator LandingGearStatus(float origin,float target)
    {
        float leftZRotation = origin; 
        float rightZRotation = -origin;

        while (!Mathf.Approximately(leftZRotation,target)&& !Mathf.Approximately(rightZRotation, -target))
        {
            leftZRotation = Mathf.MoveTowards(leftZRotation, target, 50 * Time.deltaTime);
            rightZRotation = Mathf.MoveTowards(rightZRotation, -target, 50 * Time.deltaTime);
            wheelLeft.localRotation = Quaternion.Euler(wheelLeft.localRotation.x, 180 + wheelLeft.localRotation.y, leftZRotation);
            wheelRight.localRotation = Quaternion.Euler(wheelRight.localRotation.x, 180 + wheelRight.localRotation.y, rightZRotation);
            yield return null;
        }
    }

    private void UpdateWheelsColiders(bool wheelsDown)
    {
        wheelLeft.GetComponent<SphereCollider>().enabled = wheelsDown;
        wheelRight.GetComponent<SphereCollider>().enabled = wheelsDown;
    }

    #endregion





    public void Propeller(float thrustPercent)
    {
        Animator propellerAnimator = GetComponentInChildren<Animator>();
        propellerAnimator.SetBool("Moving", thrustPercent > 0);
        propellerAnimator.speed =thrustPercent;
        propellerAudio.pitch = thrustPercent*1.8f;
        
    }

    private void Elevators()
    {
        float xRotation = -control.pitch * elevatorMaxAngle;
        elevatorLeft.localRotation = Quaternion.Euler(xRotation, 180 - elevatorLeft.localRotation.y, elevatorLeft.localRotation.z);
        elevatorRight.localRotation = Quaternion.Euler(xRotation, 180 - elevatorRight.localRotation.y, elevatorRight.localRotation.z);
    }

    private void Rudder()
    {
        float yRotation = 180 - control.yaw * rudderMaxAngle;
        rudder.localRotation = Quaternion.Euler(rudder.localRotation.x, yRotation, rudder.localRotation.z);
    }

    private void Flaps()
    {
        float xRotation = Mathf.Clamp(control.thrust * flapMaxAngle, -90, 0);
        if (control.thrust < 0)
        {
            flapLeft.localRotation = Quaternion.Euler(180 + xRotation, 10 + flapLeft.localRotation.y, 186 + flapLeft.localRotation.z);
            flapRight.localRotation = Quaternion.Euler(61 + xRotation, 5 + flapRight.localRotation.y, 193 + flapRight.localRotation.z);
        }
    }

    private void Ailerons()
    {
        float xRotation = control.roll * aileronMaxAngle;
        aileronLeft.localRotation = Quaternion.Euler(xRotation, 195 + aileronLeft.localRotation.y, 5 + aileronLeft.localRotation.z);
        aileronRight.localRotation = Quaternion.Euler(155 + xRotation, -17 + aileronRight.localRotation.y, -6 + aileronRight.localRotation.z);
    }

}
