using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroObject : MonoBehaviour
{
    public float speed;
    
    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
