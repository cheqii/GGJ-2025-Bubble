using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public State currentState; 
    public enum State{
        Grounded,
        Jumping,
        Charging
    }
    
    private float playerHalfHeight;

    public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D collider;
    public bool onGround;

    void Start(){
        playerHalfHeight = collider.bounds.extents.y; 
    }
    
    void Update(){
        switch (currentState){
            case State.Grounded:
                if (gameObject.GetComponent<JumpScript>().isJumping()){
                    gameObject.GetComponent<ChargeScript>().StartCharging();
                    currentState = State.Charging;
                }
                break;
            case State.Jumping:
                Debug.Log(currentState);
                if (GetIsGrounded()){
                    currentState = State.Grounded;
                }
                break;
            case State.Charging:
                Debug.Log(currentState);
                if (Input.GetButton("Jump")){
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
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            onGround = true;
        }
    }


}
