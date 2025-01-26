using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroObject : MonoBehaviour
{
    public Planet planet;
    public float speed;
    public int damage;

    public void InitializeAstro(Planet planet,float speed,int damage)
    {
        this.planet = planet;
        this.speed = speed;
        this.damage = damage;
    }
    private void Update()
    {
        if (GetComponent<Attractable>().currentPlanet == null)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void DeadTime()
    {
        planet.TakeDamageCost(damage);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerMain"))
        {
            //other.gameObject.GetComponent<>()
            Invoke("DeadTime",1.5f);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
