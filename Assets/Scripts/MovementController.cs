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

    public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D collider;
    public bool isJumping = false;
    public LayerMask groundLayer;

    public UnityAction PlanetTakeDamage;
    
    public UnityAction RotatePlanet;

    void Start(){
        playerHalfHeight = collider.bounds.extents.y; 
        
    }
    
    void Update(){
        
        
        Debug.DrawRay(transform.position, Vector3.down * (playerHalfHeight + 1f), Color.cyan);
        
        switch (currentState){
            case State.Grounded:
                if (Input.GetButtonDown("Jump"))
                {
                    gameObject.GetComponent<ChargeScript>().StartCharging();
                    currentState = State.Charging;
                }
                
                break;
            case State.Jumping:
                if (GetIsGrounded())
                {
                    currentState = State.Grounded;
                    PlanetTakeDamage?.Invoke();
                }
                break;
            case State.Charging:
                // Debug.Log(currentState);
                if(Input.GetButton("Jump"))
                {
                    gameObject.GetComponent<ChargeScript>().Charging();
                }
                if (Input.GetButtonUp("Jump"))
                {
                    if(isJumping) return;
                    StartCoroutine(JumpDelay());
                }
                break;
        }
    }

    public bool GetIsGrounded()
    {
        // print("check ground = " + (bool) Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, groundLayer));
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 1f, groundLayer);
    }

    private IEnumerator JumpDelay()
    {
        gameObject.GetComponent<ChargeScript>().Jump();
        currentState = State.Jumping;
                    
        // rotate planet after jump
        RotatePlanet?.Invoke();
        isJumping = true;
        yield return new WaitForSecondsRealtime(2f);
        isJumping = false;
    }
    


}
