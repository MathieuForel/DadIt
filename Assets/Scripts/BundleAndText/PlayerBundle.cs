using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerBundle : MonoBehaviour
{
    public TextAsset[] BundleList;
    
    public Sentences playerJson;
    public int bundleSelected;
    public int randomJoke;

    public GameObject textProjectilePrefab;
    public GameObject textProjectile;

    public float explosionForce;
    public float explosionRadius;
    
    public float explosionForce2;
    public float explosionRadius2;

    public Vector3 offset;
    public Vector3 randomOffset;

    public int Score;
    public Text textScore;

    public List<int> bundleShotLogList;
    public float moJokeOMeter;
    public bool isMoJokeModeActivated;
    
    public void Awake()
    {
        BundleList = Resources.LoadAll<TextAsset>("PlayerBundle");
        textProjectilePrefab = Resources.Load<GameObject>("Placeholders/TextPlayerProjectile");
    }

    private void Update()
    {
        if(!isMoJokeModeActivated)
        {
            bundleSelected = (3 + bundleSelected + (int)Input.mouseScrollDelta.y) % 3;
        }
        else
        {
            bundleSelected = 3;
        }

        
        if(Input.GetKeyDown(KeyCode.Mouse1)) ShootBundleJoke();
        if(Input.GetKeyDown(KeyCode.V))
        {
            for(var i = this.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }

        if(int.Parse(textScore.text) != Score)
        {
            for(var i = 1; i <= bundleShotLogList.Count; i++)
            {
                if(bundleSelected == 3) break;
                Debug.Log(bundleShotLogList[^i] + "=?" + bundleSelected);
                if(bundleShotLogList[^i] == bundleSelected)
                {
                    if(i > 1) Score -= 5;
                }
                else break;
            }
            
            textProjectile = Instantiate(textProjectilePrefab, textScore.transform.position, Quaternion.identity);
            textProjectile.GetComponent<TextMesh>().text = "+" + (Score - int.Parse(textScore.text));
            textProjectile.GetComponent<Rigidbody>().AddExplosionForce(explosionForce2 * 0.1f, textProjectile.transform.position + randomOffset, explosionRadius2);
            textProjectile.transform.tag = "Untagged";
            textScore.text = Score + "";
        }
    }
    
    private void ShootBundleJoke()
    {
        bundleShotLogList.Add(bundleSelected);
        if(bundleShotLogList.Count > 5) bundleShotLogList.Remove(bundleShotLogList[0]);
        
        playerJson = JsonUtility.FromJson<Sentences>(BundleList[bundleSelected].text);

        randomJoke = Random.Range(0, playerJson.dataBase.Length);

        randomOffset = new Vector3(Random.Range(-2f, 3f), Random.Range(-2f, 3f), Random.Range(-2f, 3f));

        textProjectile = Instantiate(textProjectilePrefab, this.transform.position + textProjectilePrefab.transform.position, Quaternion.identity * textProjectilePrefab.transform.rotation, this.transform);
        textProjectile.GetComponent<TextMesh>().text = playerJson.dataBase[randomJoke].sentence;
        textProjectile.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
        textProjectile.GetComponent<Rigidbody>().AddExplosionForce(explosionForce2, textProjectile.transform.position - offset + randomOffset, explosionRadius2);
        textProjectile.GetComponent<ProjectileTextBundleInfo>().textBundle = bundleSelected;
    }
}
