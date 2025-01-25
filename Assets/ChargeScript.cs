using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScript : MonoBehaviour
{
    
    public MovementController movementController;
    [SerializeField] private SpriteRenderer spriterRenderer;
    public float startJumpForce = 5f;
    public float maxJumpForce;
    private float chargeTime = 0.5f;
    [SerializeField] private float jumpCharge;
    private Vector3 scaleChange;
    

    void Awake(){
        scaleChange = new Vector3(0f, -0.01f, 0f);
    }


    public void StartCharging(){
        jumpCharge = startJumpForce;
    }

    public void Charging(){
        
        jumpCharge += Time.deltaTime / chargeTime * maxJumpForce;
        jumpCharge = Mathf.Clamp(jumpCharge, 0f, maxJumpForce);
        if (jumpCharge < maxJumpForce)
        {
            spriterRenderer.transform.localScale += scaleChange;
        }
    }

    public void Jump(){
        movementController.rb.velocity = new Vector2(movementController.rb.velocity.x, jumpCharge);
        spriterRenderer.transform.localScale = Vector3.one;
    }

}
