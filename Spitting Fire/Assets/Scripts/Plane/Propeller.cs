using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent < Animator>();
    }



}
