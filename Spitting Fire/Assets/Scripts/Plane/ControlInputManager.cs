using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Read input from user defined keys (Project Settings)
/// </summary>
public class ControlInputManager : MonoBehaviour
{
    
    public float pitch, roll, yaw, thrust;

    // Update is called once per frame
    void Update()
    {
        pitch = Input.GetAxis("Pitch");
        roll = Input.GetAxis("Roll");
        yaw = Input.GetAxis("Yaw");
        thrust = Input.GetAxis("Thrust");

        //Debug.Log($"Pitch = {pitch}, Roll = {roll}, Yaw = {yaw}, Thrust = {thrust}");
    }
}
