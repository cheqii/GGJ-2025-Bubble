using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroPooling : MonoBehaviour
{
    public static AstroPooling astroPooling;
   
    [Header("Astro Normal Pool")]
    public List<GameObject> astroNormalObjectPool;
    public GameObject astroNormalPrefab;
    public int amountToNormalPool;
    [Header("Astro Bomb Pool")]
    public List<GameObject> astroBombObjectPool;
    public GameObject astroBombPrefab;
    public int amountToBombPool;
    [Header("Astro Big Pool")]
    public List<GameObject> astroBigObjectPool;
    public GameObject astroBigPrefab;
    public int amountToBigPool;
    void Awake()
    {
        if (astroPooling == null)
        {
            astroPooling = this;
        }
    }

    void Start()
    {
        CreateAstroPool(astroNormalObjectPool, astroNormalPrefab, amountToNormalPool);
        CreateAstroPool(astroBombObjectPool,astroBombPrefab,amountToBombPool);
        CreateAstroPool(astroBigObjectPool,astroBigPrefab,amountToBigPool);
    }

    private void CreateAstroPool(List<GameObject> astroList,GameObject poolObject,int poolAmount)
    {
        GameObject tmp;
        for(int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(poolObject,transform);
            tmp.SetActive(false);
            astroList.Add(tmp);
        }
    }
    
    
    public GameObject GetNormalPooledObject()
    {
        foreach (GameObject objectPool in astroNormalObjectPool)
        {
            if (!objectPool.activeInHierarchy)
            {
                return objectPool;
            }
        }
        return null;
    }
    
    public GameObject GetBombPooledObject()
    {
        foreach (GameObject objectPool in astroBombObjectPool)
        {
            if (!objectPool.activeInHierarchy)
            {
                return objectPool;
            }
        }
        return null;
    }

    public GameObject GetBigPooledObject()
    {
        foreach (GameObject objectPool in astroBigObjectPool)
        {
            if (!objectPool.activeInHierarchy)
            {
                return objectPool;
            }
        }
        return null;
    }
    
}
