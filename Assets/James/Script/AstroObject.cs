using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum AstroObjectType
{
    None,
    MiniAstro,
    BombAstro,
    BigAstro
}

public class AstroObject : MonoBehaviour
{
    [SerializeField] private AstroObjectType astroType = AstroObjectType.None;
    
    public PlanetGravity planet;
    public int maxHealth;
    public int health;
    public int damage;
    public Transform vfx;

    private void OnEnable()
    {
        health = maxHealth;
    }

    public void InitializeAstro(PlanetGravity planet)
    {
        this.planet = planet;
    }

    private void DeadTime()
    {
        //Transform projectile = Instantiate(vfx, transform.position, Quaternion.identity);
        //Destroy(projectile, 3f);
        // planet.TakeDamage(damage);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerMain"))
        {
            if (astroType == AstroObjectType.BigAstro)
            {
                Camera.main.DOShakePosition(0.5f, 5f);
            }
            var _planet = other.gameObject.transform.parent.GetComponent<Planet>();
            _planet.TakeDamage(damage);
            Invoke("DeadTime",0.5f);
            FindObjectOfType<AudioManager>().Play("Asteroid Drop");
        }
    }

    /*private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }*/
    
}
