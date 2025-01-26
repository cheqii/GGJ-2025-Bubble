using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Physics2D = UnityEngine.Physics2D;

public class MovementController : MonoBehaviour
{
    public enum State{
        Grounded,
        Jumping,
        Charging,
        Attacking
    }

    public State currentState;
    public LayerMask groundLayer;
    public float checkRange = 0.7f;
    [Header("Jump Charge")]
    [SerializeField] private SpriteRenderer spriterRenderer;
    public float startJumpForce = 5f;
    public float maxJumpForce = 7f;
    public float chargeTime = 0.5f;
    [SerializeField] private float jumpCharge;
    
    public float JumpCharge => jumpCharge;
    public UnityAction PlanetTakeDamage;
    public UnityAction RotatePlanet;
    
    private Vector3 scaleChange;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scaleChange = new Vector3(0f, -0.01f, 0f);
    }

    public bool GetIsGrounded()
    {
        if (Physics2D.OverlapCircle(transform.position, checkRange, groundLayer))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        return Physics2D.OverlapCircle(transform.position, checkRange, groundLayer);
    }

    private void PlayerJump()
    {
        switch (currentState){
            case State.Grounded:
                if (Input.GetMouseButtonDown(0))
                {
                    currentState = State.Charging;
                    jumpCharge = startJumpForce;
                }
                break;
            case State.Charging:
                jumpCharge += Time.deltaTime / chargeTime * maxJumpForce;
                jumpCharge = Mathf.Clamp(jumpCharge, 0f, maxJumpForce);
                if (jumpCharge < maxJumpForce)
                {
                    spriterRenderer.transform.localScale += scaleChange;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Jump();
                    currentState = State.Jumping;
                    RotatePlanet?.Invoke();
                }
                break;
            case State.Jumping:
                if (GetIsGrounded())
                {
                    currentState = State.Grounded;
                    PlanetTakeDamage?.Invoke();
                }
                break;
        }
    }

    public void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpCharge);
        spriterRenderer.transform.localScale = Vector3.one;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRange);
    }
}
