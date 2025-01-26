using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AstroObject))]
public class BombAstro : MonoBehaviour
{
    public GameObject miniBombAstro;
    public int bombCount;
    public bool isBomb;

    private void Update()
    {
        if (GetComponent<AstroObject>().health <= 0 && isBomb == false)
        {
            Bomb();
            isBomb = true;
        }
        
    }

    private void Bomb()
    {
        for (int a = 0; a < bombCount; a++)
        {
            GameObject miniBomb = Instantiate(miniBombAstro, transform.position, transform.rotation);
        }
    }
}
