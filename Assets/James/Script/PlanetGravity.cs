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

    private void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        SetAttractedObject();
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
    }
}
