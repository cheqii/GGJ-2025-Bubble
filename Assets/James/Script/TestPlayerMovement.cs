using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace James
{
    public class TestPlayerMovement : MonoBehaviour
    {
        public float moveSpeed;

        private void Update()
        {
            PlayerMove();
        }

        private void PlayerMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector2 movement = new Vector2(horizontal, vertical);
            
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}

