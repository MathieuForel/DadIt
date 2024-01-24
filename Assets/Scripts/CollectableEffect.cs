using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CollectableEffect : MonoBehaviour
{
    private Animation clip;
    
    // Start is called before the first frame update
    void Awake()
    {
        clip = GetComponent<Animation>();
        StartCoroutine(StartAnime());
    }

    IEnumerator StartAnime()
    {
        clip.Stop("CollectableMove");
        yield return new WaitForSeconds(Random.Range(0, 300)/500);
        clip.Play();
    }
}
