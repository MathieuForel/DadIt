using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class random_generator : MonoBehaviour
{
    public GameObject[] list_enemies;
    private float cooldown = 0f;


    private void FixedUpdate()
    {
        if (cooldown > 1)
        {
            Vector3 spawn = transform.position;
            spawn.x = Random.Range(-20,20);
            spawn.z = Random.Range(-20,20);
            
            int randomEnnemy = Random.Range(0, list_enemies.Length);
            Instantiate(list_enemies[randomEnnemy], spawn, Quaternion.identity);
            cooldown = 0;
        }
        else
        {
            cooldown += Time.fixedDeltaTime;
        }
    }

}
