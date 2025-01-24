using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace James
{
    public enum PlayerState
    {
        OnGround,
        OnJump,
    }
    [RequireComponent(typeof(Attractable))]
    public class TestPlayerMovement : MonoBehaviour
    {
        [Header("Player Setting")]
        public PlayerState playerState;
        public float moveSpeed;
        public float jumpTime;
        public Transform leftJumpPoint;
        public Transform rightJumpPoint;
        
        [Header("Trajectory Setting")]
        public float trajectoryMaxRelativeHeight;
        public Vector3 trajectoryStartPoint;
        public AnimationCurve projectileCurve;
        public AnimationCurve axisCorrectionProjectileCurve;
        public AnimationCurve speedProjectileCurve;
        
        [Header("HideInspector")]
        private Attractable attractable;
        private Vector3 targetDirection;
        private float jumpTimeCounter;

        private void Awake()
        {
            attractable = GetComponent<Attractable>();
        }

        private void Update()
        {
            PlayerMove();
            PlayerJump();
        }

        private void PlayerMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector2 movement = new Vector2(horizontal, vertical);
            
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
                    if (mousePos.y > transform.position.y)
                    {
                        targetDirection = (rightJumpPoint.position - transform.position).normalized;
                    }
                    else
                    {
                        targetDirection = (leftJumpPoint.position - transform.position).normalized;
                    }
                }
                else if (mousePos.x > transform.position.x)
                {
                    if (mousePos.y > transform.position.y)
                    {
                        targetDirection = (leftJumpPoint.position - transform.position).normalized;
                    }
                    else
                    {
                        targetDirection = (rightJumpPoint.position - transform.position).normalized;
                    }
                    
                }
                
                trajectoryStartPoint = transform.position;
                jumpTimeCounter = 0;
                playerState = PlayerState.OnJump;
            }

            if (playerState == PlayerState.OnJump)
            {
                jumpTimeCounter += Time.deltaTime;
                if (jumpTimeCounter >= jumpTime)
                {
                    playerState = PlayerState.OnGround;
                }
                UpdateProjectileCurve();
                //transform.position += targetDirection * moveSpeed * Time.deltaTime;
            }
        }
         
        private void UpdateProjectileCurve()
        {
            Vector3 trajectoryRange = targetDirection - trajectoryStartPoint;

            if (trajectoryRange.x < 0)
            {
                moveSpeed = -moveSpeed;
            }
        
            float nextPositionX = transform.position.x + moveSpeed  * Time.deltaTime;
            float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;
        
            float nextPositionYNormalized = projectileCurve.Evaluate(nextPositionXNormalized);
        
            float nextPositionYCorrectionNormalize = axisCorrectionProjectileCurve.Evaluate(nextPositionXNormalized);
            float nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalize * trajectoryRange.y;
        
            float nextPositionY = trajectoryStartPoint.y + nextPositionYNormalized * trajectoryMaxRelativeHeight + nextPositionYCorrectionAbsolute;
        
            Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);
        
            CalculateNextProjectileSpeed(nextPositionXNormalized);
        
            transform.position = newPosition;
        }

        private void CalculateNextProjectileSpeed(float nextPositionXNormalized) 
        {
            float nextMoveSpeedNormalized = speedProjectileCurve.Evaluate(nextPositionXNormalized);
        
            moveSpeed = nextMoveSpeedNormalized * moveSpeed;
        }
    }
}

