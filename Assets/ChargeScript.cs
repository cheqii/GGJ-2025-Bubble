using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScript : MonoBehaviour
{
    
    public MovementController movementController;
    [SerializeField] private SpriteRenderer spriterRenderer;
    public float jumpForce = 5f;
    public float maxJumpForce;
    private float chargeTime = 0.5f;
    [SerializeField] private float jumpCharge;
    private Vector3 scaleChange;
    

    void Awake(){
        scaleChange = new Vector3(0f, -0.01f, 0f);
    }


    public void StartCharging(){
        jumpCharge = 0f;
    }

    public void Charging(){
        spriterRenderer.transform.localScale += scaleChange;
        jumpCharge += Time.deltaTime / chargeTime * (maxJumpForce - jumpForce);
        jumpCharge = Mathf.Clamp(jumpCharge, 0f, maxJumpForce - jumpForce);
    }

    public void Jump(){
        movementController.rb.velocity = new Vector2(movementController.rb.velocity.x, jumpForce + jumpCharge);
        spriterRenderer.transform.localScale = Vector3.one;
    }

}
