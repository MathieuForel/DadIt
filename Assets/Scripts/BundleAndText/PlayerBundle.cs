using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerBundle : MonoBehaviour
{
    public TextAsset[] BundleList;
    
    public Sentences playerJson;
    public int bundleSelected;
    public int randomJoke;

    public void Awake()
    {
        BundleList = Resources.LoadAll<TextAsset>("PlayerBundle");
    }

    private void Update()
    {
        bundleSelected = (3 + bundleSelected + (int)Input.mouseScrollDelta.y) % 3;
        
        if(Input.GetKeyDown(KeyCode.Mouse1)) ShootBundleJoke();
    }
    
    private void ShootBundleJoke()
    {
        playerJson = JsonUtility.FromJson<Sentences>(BundleList[bundleSelected].text);

        randomJoke = Random.Range(0, playerJson.dataBase.Length);
        
        Debug.Log(playerJson.dataBase[randomJoke].sentence);
    }
}
