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
        public float moveSpeed;
        public float jumpForce;
        public float jumpRange;
        public float jumpTime;
        
        [Header("HideInspector")]
        private Attractable attractable;
        private Rigidbody2D rb;
        private Vector3 targetDirection;
        private float jumpTimeCounter;

        private void Awake()
        {
            attractable = GetComponent<Attractable>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            PlayerMove();
            PlayerJump();
        }

        private void PlayerMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");
            
            Vector2 movement = new Vector2(horizontal, 0);
            
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }

        private void PlayerJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && attractable.onGround)
            {
                attractable.onGround = false;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < transform.position.x)
                {
                    rb.velocity = new Vector2(rb.velocity.x , jumpForce);
                    //rb.velocity = new Vector2(jumpRange,rb.velocity.y) * transform.right;
                }
                else if (mousePos.x > transform.position.x)
                {
                    rb.velocity = new Vector2(rb.velocity.x , jumpForce);
                    //rb.velocity = new Vector2(jumpRange,rb.velocity.y) * transform.right;
                }

                jumpTimeCounter = 0;
                playerState = PlayerState.OnJump;
            }

            if (playerState == PlayerState.OnJump)
            {
                jumpTimeCounter += Time.deltaTime;
                if (jumpTimeCounter >= jumpTime)
                {
                    playerState = PlayerState.OnGround;
                    jumpTimeCounter = 0;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,transform.right * 1);
            Gizmos.DrawRay(transform.position,-transform.right * 1);
        }
    }
}

