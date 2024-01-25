using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public int range;
    public int timeOut;
    private bool canShoot;

    public Camera cam;
    public PlayerCamera pc;
    
    public Animator animator;

    private void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Aiming();
    }
    
    private IEnumerator ResetTrue()
    {
        yield return new WaitForSeconds(timeOut);
        canShoot = true;
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                if (hit.collider.CompareTag("Damageable"))
                {
                    Debug.Log("Hit");
                }

                canShoot = false;
                StartCoroutine(ResetTrue());
            }
        }
    }

    void Aiming()
    {
        animator.SetBool("isAiming", Input.GetMouseButton(1));
        pc.playerSens = Input.GetMouseButton(1) ? pc.maxSens / 5f : pc.maxSens;
    }
}
