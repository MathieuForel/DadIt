using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RSpawnBounds : MonoBehaviour
{
    private Vector3 spawnPoint;
    public Collider spawnArea;
    
    [Space]
    
    public GameObject props;
    [Range(1, 5)]
    public int propsNum;
    
    [Space]
    
    public GameObject[] bundles;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Props
        for (int i = 0; i < propsNum; i++)
        {
            spawnPoint = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), spawnArea.bounds.max.y,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            var spawned = Instantiate(props, spawnPoint, Quaternion.identity);
        }
        
        //Instantiate Bundles
        for (int i = 0; i < propsNum*2; i++)
        {
            spawnPoint = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), spawnArea.bounds.max.y,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            var spawned = Instantiate(bundles[Random.Range(0, 3)], spawnPoint, Quaternion.identity);
        }
    }
}
