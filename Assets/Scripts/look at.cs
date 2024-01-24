using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    GameObject target;

    bool is_in_collider;
    private void OnTriggerEnter(Collider other)
    {
        is_in_collider = true;
        target = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        is_in_collider = false;
    }
    private void Update()
    {
        if (is_in_collider)
        {
            target.transform.LookAt(gameObject.transform.position, Vector3.up);
        }
    }
}
