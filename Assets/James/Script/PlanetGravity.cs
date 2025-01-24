using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using James;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public LayerMask attractLayer;
    public float gravitySize = -10f;
    public float gravityRadius = 10;
    public List<Collider2D> attractedObject = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;
    public bool onPlayer;

    private void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        SetAttractedObject();
        if (!onPlayer)
        {
            transform.Rotate(0, 0, 10 * Time.deltaTime);
        }
        
    }

    private void FixedUpdate()
    {
        AttractObjects();
    }

    private void SetAttractedObject()
    {
        attractedObject = Physics2D.OverlapCircleAll(planetTransform.position, gravityRadius, attractLayer).ToList();
    }

    private void AttractObjects()
    {
        foreach (Collider2D attractable in attractedObject)
        {
            if (attractable.GetComponent<TestPlayerMovement>().playerState == PlayerState.OnJump)
            {
                return;
            }
            attractable.GetComponent<Attractable>().Attract(this);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,gravityRadius);
        Gizmos.DrawRay(transform.position,Vector3.up * 5);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayer = false;
        }
    }
}
