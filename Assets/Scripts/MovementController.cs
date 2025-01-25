using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public State currentState; 
    public enum State{
        Grounded,
        Jumping,
        Charging,
        Attacking
    }
    
    private float playerHalfHeight;

    public Rigidbody2D rb;
    [SerializeField] private CircleCollider2D collider;
    public GameObject attackRange;

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
                else if (Input.GetButtonDown("Fire1")){
                    currentState = State.Attacking;
                }
                FindObjectOfType<AudioManager>().Play("Drop");
                FindObjectOfType<AudioManager>().Reset("Jump");
                
                break;
            case State.Jumping:
                FindObjectOfType<AudioManager>().Reset("Drop");
                if (GetIsGrounded()){
                    currentState = State.Grounded;
                }
                break;
            case State.Charging:
                if (Input.GetButton("Jump")){
                    FindObjectOfType<AudioManager>().Play("Charge");
                    gameObject.GetComponent<ChargeScript>().Charging();
                }
                if (Input.GetButtonUp("Jump")){
                    FindObjectOfType<AudioManager>().Reset("Charge");
                    FindObjectOfType<AudioManager>().Play("Jump");
                    gameObject.GetComponent<ChargeScript>().Jump();
                    currentState = State.Jumping;
                }
                break;
            case State.Attacking:
                FindObjectOfType<AudioManager>().Play("Hit");
                this.gameObject.transform.GetChild(0).GetComponent<AttackingScript>().Attacking();
                currentState = State.Grounded;
                break;
        }
    }

    private bool GetIsGrounded(){
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Attractable"));
    }


}
