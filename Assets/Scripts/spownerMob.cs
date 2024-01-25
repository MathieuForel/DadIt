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
    public Vector3[] zoneSpown;

    private void Start()
    {
        zoneSpown = new Vector3[20];
        zoneSpown[0] = new Vector3(0, 0, 0);
        zoneSpown[1] = new Vector3(1, 0, 0);
        zoneSpown[2] = new Vector3(2, 0, 0);
        zoneSpown[3] = new Vector3(3, 0, 0);
        zoneSpown[4] = new Vector3(4, 0, 0);
        zoneSpown[5] = new Vector3(5, 0, 0);
        zoneSpown[6] = new Vector3(6, 0, 0);
        zoneSpown[7] = new Vector3(7, 0, 0);
        zoneSpown[8] = new Vector3(8, 0, 0);
        zoneSpown[9] = new Vector3(9, 0, 0);
        zoneSpown[10] = new Vector3(0, 0, 1);
        zoneSpown[11] = new Vector3(0, 0, 2);
        zoneSpown[12] = new Vector3(0, 0, 3);
        zoneSpown[13] = new Vector3(0, 0, 4);
        zoneSpown[14] = new Vector3(0, 0, 5);
        zoneSpown[15] = new Vector3(1, 0, 6);
        zoneSpown[16] = new Vector3(2, 0, 7);
        zoneSpown[17] = new Vector3(3, 0, 8);
        zoneSpown[18] = new Vector3(4, 0, 9);
        zoneSpown[19] = new Vector3(5, 0, 10);
    }


    private void FixedUpdate()
    {
        if (temp < number_enemies)
        {
            if (cooldown > 1)
            {
                Vector3 spawn = transform.position;
                int spown_chosen_index = Random.Range(0, zoneSpown.Length);
                Vector3 chosen = zoneSpown[spown_chosen_index];
                spawn.x = chosen.x;
                spawn.z = chosen.z;
                int randomEnnemy = Random.Range(0, list_enemies.Length);
                Instantiate(list_enemies[randomEnnemy], spawn, Quaternion.identity);
                cooldown = 0;
                temp += 1;

            }
            else
            {
                cooldown += Time.fixedDeltaTime;

            }

        }

    }
}
    


