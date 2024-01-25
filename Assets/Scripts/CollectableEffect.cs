using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CollectableEffect : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(Random.Range(0f, 100f)/100f);
        anim.SetBool("startAnim", true);
    }
}