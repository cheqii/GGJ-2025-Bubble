using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float gravity = -9.81f;
    public float jumpForce = 5f;
    public float moveSpeed = 3f;
    private bool isGrounded;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float playerHalfHeight;
    private float boundary = 0.0f;

    void Start(){
        playerHalfHeight = spriteRenderer.bounds.extents.y; 
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && GetIsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        if (!GetIsGrounded())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition.x);
            if (mousePosition.x > transform.position.x + boundary)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else if (mousePosition.x < transform.position.x - boundary)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
    }
    private bool GetIsGrounded(){
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));
    }
}
