using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public enum State{
        Grounded,
        Jumping,
        Charging,
        Attacking
    }

    public State currentState;
    [SerializeField] private float playerHalfHeight;
    public LayerMask groundLayer;
    
    
    [Header("๋Jump Charge")]
    [SerializeField] private SpriteRenderer spriterRenderer;
    public float startJumpForce = 5f;
    public float maxJumpForce = 7f;
    public float chargeTime = 0.5f;
    [SerializeField] private float jumpCharge;
    public float JumpCharge => jumpCharge;
    public UnityAction PlanetTakeDamage;
    public UnityAction RotatePlanet;
    
    private Vector3 scaleChange;
    private Collider2D collider;
    private Rigidbody2D rb;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        scaleChange = new Vector3(0f, -0.01f, 0f);
    }

    void Start(){
        playerHalfHeight = collider.bounds.extents.y; 
        
    }
    
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * (playerHalfHeight + 1f), Color.cyan);
        PlayerJump();
    }

    public bool GetIsGrounded()
    {
        // print("check ground = " + (bool) Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, groundLayer));
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 1f, groundLayer);
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
    


}
