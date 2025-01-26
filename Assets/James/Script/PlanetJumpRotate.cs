using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetJumpRotate : MonoBehaviour
{
    public MovementController chargeScript;

    private void Update()
    {
        if (chargeScript.GetComponent<Rigidbody2D>().velocity.y > 1 || chargeScript.GetComponent<Rigidbody2D>().velocity.y < -1)
        { 
            transform.Rotate(0,0,Time.deltaTime * 10);
        }
    }
}
