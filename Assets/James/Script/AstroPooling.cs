using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroPooling : MonoBehaviour
{
    public static AstroPooling astroPooling;
    public List<GameObject> astroObjectPool;
    public GameObject astroPrefab;
    public int amountToPool;

    void Awake()
    {
        if (astroPooling == null)
        {
            astroPooling = this;
        }
    }

    void Start()
    {
        astroObjectPool = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(astroPrefab);
            tmp.SetActive(false);
            astroObjectPool.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject()
    {
        foreach (GameObject objectPool in astroObjectPool)
        {
            if (!objectPool.activeInHierarchy)
            {
                return objectPool;
            }
        }
        return null;
    }
}
