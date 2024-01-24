using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class EnnemyBundle : MonoBehaviour
{
    public TextAsset[] topicList;
    
    public Sentences pnjJson;
    public int pnjSeed;

    public void Awake()
    {
        topicList = Resources.LoadAll<TextAsset>("SimplePnjDialogue");
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) PnjRandomization();
    }

    private void PnjRandomization()
    {
        pnjSeed = Random.Range(0, 3);

        pnjJson = JsonUtility.FromJson<Sentences>(topicList[pnjSeed].text);

        pnjSeed = Random.Range(0, pnjJson.dataBase.Length);
        
        Debug.Log(pnjJson.dataBase[pnjSeed].sentence);
    }
}
