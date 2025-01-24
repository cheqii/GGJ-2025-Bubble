using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace James
{
    public enum PlayerState
    {
        OnGround,
        OnJump,
    }

    public enum JumpSide
    {
        Left,
        Right,
    }
    [RequireComponent(typeof(Attractable))]
    public class TestPlayerMovement : MonoBehaviour
    {
        [Header("Player Setting")]
        public PlayerState playerState;
        public JumpSide jumpSide;
        public float jumpForce;
        
        [Header("HideInspector")]
        private Rigidbody2D rb;
        private Vector3 targetDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            PlayerJump();
        }

        private void PlayerJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerState == PlayerState.OnGround)
            {
                playerState = PlayerState.OnJump;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < transform.position.x)
                {
                    jumpSide = JumpSide.Left;
                    rb.velocity = new Vector2(rb.velocity.x , jumpForce);
                }
                else if (mousePos.x > transform.position.x)
                {
                    jumpSide = JumpSide.Right;
                    rb.velocity = new Vector2(rb.velocity.x , jumpForce);
                }
                
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,transform.right * 1);
            Gizmos.DrawRay(transform.position,-transform.right * 1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                playerState = PlayerState.OnGround;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                
            }
        }
    }
}

