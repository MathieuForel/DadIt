using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RSpawnBounds : MonoBehaviour
{
    private Vector3 spawnPoint;
    public Collider[] spawnArea;
    
    [Space]
    
    public GameObject props;
    public int propsNum;
    
    [Space]
    
    public GameObject[] bundles;

    public int[] bundleNum;
    
    // Start is called before the first frame update
    void Start()
    {
        propsNum = 1;
        for (int i = 0; i < 8; i++)
        {
            bundleNum[i] = 2;
        }

        for (int i = 0; i < 8; i++) // 3 zones
        {
            //Instantiate Beers
            for (int x = 0; x < propsNum; x++)
            {
                spawnPoint = GetRandomPosition(i);
                
                var spawned = Instantiate(props, spawnPoint, Quaternion.identity);
            }
            
            //Instantiate Bundles
            for (int z = 0; z < bundleNum[i]; z++)
            {
                spawnPoint = GetRandomPosition(i);
                
                var spawned = Instantiate(bundles[z], spawnPoint, Quaternion.identity);
                spawned.transform.position = spawnPoint;
            }
        }
    }

    Vector3 GetRandomPosition(int index)
    {
        Collider randomArea = spawnArea[index];
        
        float x = Random.Range(randomArea.bounds.min.x, randomArea.bounds.max.x);
        float z = Random.Range(randomArea.bounds.min.z, randomArea.bounds.max.z);
        
        return new Vector3(x, randomArea.bounds.max.y +1f, z);
    }
}