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
    public int bundleNum;

    public int[] objectNum = new int[]{ 0, 0, 0};
    
    // Start is called before the first frame update
    void Start()
    {
        propsNum = Random.Range(4, 6);
        bundleNum = Random.Range(2, 5);
        
        //Instantiate Beers
        for (int i = 0; i < propsNum; i++)
        {
            spawnPoint = GetRandomPosition();
            
            var spawned = Instantiate(props, spawnPoint, Quaternion.identity);
        }
        
        //Instantiate Bundles
        for (int i = 0; i < bundleNum*3; i++)
        {
            spawnPoint = GetRandomPosition();
            
            var spawned = Instantiate(bundles[Random.Range(0, 3)], spawnPoint, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        int random = Random.Range(0, spawnArea.Length);
        if (Mathf.Min(objectNum) + 1 < Mathf.Max(objectNum))
        {
            random = Mathf.Min(objectNum);
        }
        
        objectNum[random]++;
        Collider randomArea = spawnArea[random];
        
        float x = Random.Range(randomArea.bounds.min.x, randomArea.bounds.max.x);
        float z = Random.Range(randomArea.bounds.min.x, randomArea.bounds.max.x);
            
        return new Vector3(x, randomArea.bounds.max.y, z);
    }
}