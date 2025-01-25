using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    
    public MovementController movementController;
    public float gravity = -9.81f;
    public float jumpForce = 5f;
    private float playerHalfHeight;

    public bool isJumping()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            return true;
        }

        return false;

    }

}
