using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    public float groundCheckRange = 1;
    public bool onGround = false;
    [Header("Gravity Check")]
    public PlanetGravity currentPlanet;
    public bool rotateToCenter = true;
    
    [Header("Ref")]
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        onGround = Physics2D.Raycast(groundCheckPoint.position, Vector2.down,groundCheckRange, groundLayer);
        if (currentPlanet != null)
        {
            if (!currentPlanet.attractedObject.Contains(_collider2D))
            {
                currentPlanet = null;
            }

            if (rotateToCenter)
            {
                RotateToCenter();
            }

            if (onGround)
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
            
            
        }
    }

    private void RotateToCenter()
    {
        Vector2 distanceVector = (Vector2)currentPlanet.planetTransform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
 
    public void Attract(PlanetGravity planetGravity)
    {
        Vector2 attractDir = (Vector2)planetGravity.planetTransform.position - _rigidbody2D.position;
        _rigidbody2D.AddForce(attractDir.normalized * -planetGravity.gravitySize * 100 * Time.fixedDeltaTime);
        
        if (currentPlanet ==null)
        {
            currentPlanet = planetGravity;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(groundCheckPoint.position,Vector2.down * groundCheckRange);
    }
    
}