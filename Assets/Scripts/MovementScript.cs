using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public MovementController movementController;
    public float moveSpeed = 3f;
    public void Moving()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x > transform.position.x)
        {
            movementController.rb.velocity = new Vector2(moveSpeed, movementController.rb.velocity.y);
        }
        else if (mousePosition.x < transform.position.x)
        {
            movementController.rb.velocity = new Vector2(-moveSpeed, movementController.rb.velocity.y);
        }
    }

}
