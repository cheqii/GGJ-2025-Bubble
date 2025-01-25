using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public bool onPlayer;
    public bool onGround;
    public float checkRange;
    public float groundCheckRange;

    private void Update()
    {
        onPlayer = Physics2D.Raycast(transform.position, Vector2.up, checkRange,playerLayer);
        onGround = Physics2D.Raycast(transform.position, Vector2.up, 0.1f,groundLayer);
        if (onPlayer)
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        else
        {
            if (!onGround)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector2.up * checkRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector2.up * groundCheckRange);
    }
}
