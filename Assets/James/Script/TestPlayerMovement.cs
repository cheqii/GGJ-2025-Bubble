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
        OnPrepareJump,
        OnJump,
    }

    public enum JumpSide
    {
        Left,
        Right,
    }
    public class TestPlayerMovement : MonoBehaviour
    {
        [Header("Player Setting")]
        public PlayerState playerState;
        public JumpSide jumpSide;

        [Header("Jump Setting")] 
        public float chargeTime = 0.5f;
        public float minJumpForce = 5f;
        public float maxJumpForce = 10f;
        public Vector3 scaleChange = new Vector3(0f, -0.01f, 0f);
        public SpriteRenderer spriteRenderer;
        public LayerMask groundLayer;
        private float jumpForce;
        
        [Header("HideInspector")]
        private Rigidbody2D rb;
        private Vector3 targetDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            //PlayerJump();
        }

        private void PlayerJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerState == PlayerState.OnGround)
            {
                playerState = PlayerState.OnPrepareJump;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < transform.position.x)
                {
                    jumpSide = JumpSide.Left;
                }
                else if (mousePos.x > transform.position.x)
                {
                    jumpSide = JumpSide.Right;
                }
                jumpForce = minJumpForce;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                jumpForce += Time.deltaTime / chargeTime * maxJumpForce;;
                if (jumpForce < maxJumpForce)
                {
                    spriteRenderer.transform.localScale += scaleChange;
                }
                if (jumpForce > maxJumpForce)
                {
                    jumpForce = maxJumpForce;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && playerState == PlayerState.OnPrepareJump)
            {
                playerState = PlayerState.OnJump;
                rb.velocity = new Vector2(rb.velocity.x , jumpForce);
                jumpForce = 0;
                spriteRenderer.transform.localScale = Vector3.one;
            }

            if (playerState == PlayerState.OnJump)
            {
                if (GetIsGrounded())
                {
                    
                }
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,transform.right * 1);
            Gizmos.DrawRay(transform.position,-transform.right * 1);
        }

        public bool GetIsGrounded()
        {
            // print("check ground = " + (bool) Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, groundLayer));
            return Physics2D.Raycast(transform.position, Vector2.down, GetComponent<Collider2D>().bounds.extents.y + 1f, groundLayer);
        }
        
    }
}

