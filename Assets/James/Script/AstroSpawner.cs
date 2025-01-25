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
[RequireComponent(typeof(AstroPooling))]
public class AstroSpawner : MonoBehaviour
{
    public SpawnState state = SpawnState.OnPrepare;
    public int spawnCount;
    public float spawnDelay = 1.5f;
    private float spawnDelayTime;
    [Header("RandomPoint")]
    public Vector3 startSpawnPoint;
    public Vector3 endSpawnPoint;

    public void SpawnAstro(int count)
    {
        spawnCount = count;
        state = SpawnState.OnSpawn;
    }
    
    [ContextMenu("Test Spawn Astro")]
    private void SpawnAstro()
    {
        spawnCount = 5;
        state = SpawnState.OnSpawn;
    }

    private void Update()
    {
        switch (state)
        {
            case SpawnState.OnPrepare:
                break;
            case SpawnState.OnSpawn:
                if (spawnCount > 0)
                {
                    spawnDelayTime += Time.deltaTime;
                    if (spawnDelayTime >= spawnDelay)
                    {
                        Vector3 spawnPoint = new Vector3(Random.Range(startSpawnPoint.x, endSpawnPoint.x), Random.Range(startSpawnPoint.y, endSpawnPoint.y));
                        GameObject bullet = GetComponent<AstroPooling>().GetPooledObject(); 
                        if (bullet != null) {
                            bullet.transform.position = spawnPoint;
                            bullet.transform.rotation = Quaternion.identity;
                            bullet.SetActive(true);
                        }
                        bullet.GetComponent<AstroObject>().speed = Random.Range(0.8f, 1.2f);
                        spawnCount -= 1;
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
}
