using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public State currentState; 
    public enum State{
        Grounded,
        Jumping,
        Charging,
        Attacking
    }
    
    [SerializeField] private float playerHalfHeight;

    public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D collider;
    public bool onGround;
    public LayerMask groundLayer;

    public UnityAction PlanetTakeDamage;

    void Start(){
        playerHalfHeight = collider.bounds.extents.y; 
        
    }
    
    void Update(){
        switch (currentState){
            case State.Grounded:
                if (Input.GetButtonDown("Jump")){
                    gameObject.GetComponent<ChargeScript>().StartCharging();
                    currentState = State.Charging;
                }
                
                break;
            case State.Jumping:
                // Debug.Log(currentState);
                if (GetIsGrounded())
                {
                    PlanetTakeDamage?.Invoke();
                    currentState = State.Grounded;
                }
                break;
            case State.Charging:
                // Debug.Log(currentState);
                if (Input.GetButton("Jump"))
                {
                    gameObject.GetComponent<ChargeScript>().Charging();
                }
                if (Input.GetButtonUp("Jump")){
                    gameObject.GetComponent<ChargeScript>().Jump();
                    currentState = State.Jumping;
                    
                }
                break;
        }
    }

    private bool GetIsGrounded(){
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, groundLayer);
    }
    


}
