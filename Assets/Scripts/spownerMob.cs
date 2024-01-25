using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.Search;
using UnityEngine;

public class random_generator : MonoBehaviour
{
    public GameObject[] list_enemies;
    public int number_enemies = 10;
    public int temp;
    private float cooldown = 0f;
    public List<Vector3> zoneSpownList = new List<Vector3> ();




    private void FixedUpdate()
    {
        if (temp < number_enemies)
        {
                Vector3 spawn = transform.position;
                int spown_chosen_index = Random.Range(0, zoneSpownList.Count);
                Vector3 chosen = zoneSpownList[spown_chosen_index];
                spawn.x = chosen.x;
                spawn.z = chosen.z;
                int randomEnnemy = Random.Range(0, list_enemies.Length);
                Instantiate(list_enemies[randomEnnemy], spawn, Quaternion.identity);
                cooldown = 0;
                temp += 1;

        }

    }
}
    


