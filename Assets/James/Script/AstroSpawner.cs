using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnState
{
    OnPrepare,
    OnSpawn,
}

public enum AstroName
{
     AstroNormal,
     AstroBomb,
     AstroBig,
}

public enum SpawnSide
{
    LeftSide,
    RightSide,
}
[Serializable]
public class AstroType
{
    public AstroName astroName;
    public int cost;
}

[CreateAssetMenu(fileName = "AstroType", menuName = "Astro/Astro Type")]
public class AstroList : ScriptableObject
{
    public List<AstroType> astroList;
}
[RequireComponent(typeof(AstroPooling))]
public class AstroSpawner : MonoBehaviour
{
    [Header("Astro Ref")]
    public PlanetGravity planet;
    public Animator animator;
    public SpawnState state = SpawnState.OnPrepare;
    public AstroList astroList;
    public List<AstroType> currentAstroList;
    [Header("Astro Setting")]
    public int spawnCost = 5;
    public float spawnDelay = 1.5f;
    private float spawnDelayTime;
    [Header("RandomPoint")]
    public SpawnSide spawnSide = SpawnSide.LeftSide;
    public Vector3 spawnPoint;
    public Vector3 leftPoint;
    public Vector3 rightPoint;

    
    
    [ContextMenu("Test Spawn Astro")]
    private void SpawnAstro()
    {
        state = SpawnState.OnSpawn;
        float randomX = Random.Range(0f, 1f);
        if (randomX < 0.5f)
        {
            spawnSide = SpawnSide.LeftSide;
            animator.SetTrigger("AleartLeft");
        }
        else
        {
            spawnSide = SpawnSide.RightSide;
            animator.SetTrigger("AleartRight");
        }
        
    }

    private void Update()
    {
        switch (state)
        {
            case SpawnState.OnPrepare:
                break;
            case SpawnState.OnSpawn:
                if (spawnCost > 0)
                {
                    spawnDelayTime += Time.deltaTime;
                    if (spawnDelayTime >= spawnDelay)
                    {
                        PrepareAstro(spawnCost);
                        spawnDelayTime = 0;
                    }
                }
                else
                {
                    state = SpawnState.OnPrepare;
                }
                break;
        }
    }
    private void PrepareAstro(int cost)
    {
        foreach (AstroType astroType in astroList.astroList)
        {
            if (astroType.cost <= cost)
            {
                currentAstroList.Add(astroType);
            }
        }

        if (spawnSide == SpawnSide.LeftSide)
        {
            spawnPoint = new Vector2(leftPoint.x, Random.Range(leftPoint.y, leftPoint.z));
        }
        else
        {
            spawnPoint = new Vector2(rightPoint.x, Random.Range(rightPoint.y, rightPoint.z));
        }
        int randomAstroNumber = Random.Range(0, currentAstroList.Count - 1);
        AstroName astroName = currentAstroList[randomAstroNumber].astroName;
        switch (astroName)
        {
            case AstroName.AstroNormal:
            {
                GameObject bullet = GetComponent<AstroPooling>().GetNormalPooledObject(); 
                if (bullet != null) {
                    bullet.transform.position = spawnPoint;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetActive(true);
                }
                AstroObject astroObject =bullet.GetComponent<AstroObject>();
                astroObject.InitializeAstro(planet);
                break;
            }
            case AstroName.AstroBomb:
            {
                GameObject bullet = GetComponent<AstroPooling>().GetBombPooledObject(); 
                if (bullet != null) {
                    bullet.transform.position = spawnPoint;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetActive(true);
                }
        
                AstroObject astroObject =bullet.GetComponent<AstroObject>();
                astroObject.InitializeAstro(planet);
                break;
            }
            case AstroName.AstroBig:
            {
                GameObject bullet = GetComponent<AstroPooling>().GetBigPooledObject(); 
                if (bullet != null) {
                    bullet.transform.position = spawnPoint;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetActive(true);
                }
        
                AstroObject astroObject =bullet.GetComponent<AstroObject>();
                astroObject.InitializeAstro(planet);
                break;
            }
        }
        
        spawnCost -= currentAstroList[randomAstroNumber].cost;
        currentAstroList.Clear();
    }
}
