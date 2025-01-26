using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime), Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime), Space.Self);
        }
        else
        {
            transform.Translate(Vector2.zero,Space.Self);
        }
    }
}
