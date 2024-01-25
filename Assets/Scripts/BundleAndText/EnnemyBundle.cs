using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class EnnemyBundle : MonoBehaviour
{
    public TextAsset[] topicList;
    public TextAsset complexResponse;
    
    public Sentences pnjJson;
    public Sentences pnjResponse;
    public int pnjBundleSelected;
    public int sentenceSelected;
    
    public PlayerBundle playerBundle;
    public ProjectileTextBundleInfo projectileTextBundleInfo;
    public int playerChosenBundle;
    public TextMesh playerText;
    public float deSpawnTimer;

    public bool hasTalked;
    public bool isComplex;
    
    public void Awake()
    {
        topicList = Resources.LoadAll<TextAsset>("SimplePnjDialogue");
        if(isComplex) complexResponse = Resources.Load<TextAsset>("ComplexPnjDialogue/ComplexPeopleDataBase");
        playerBundle = GameObject.FindWithTag("MainCamera").GetComponent<PlayerBundle>();
    }

    public void Start()
    {
        hasTalked = false;
        PnjRandomization();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) PnjRandomization();
    }

    private void PnjRandomization()
    {
        pnjBundleSelected = Random.Range(0, 3);

        pnjJson = JsonUtility.FromJson<Sentences>(topicList[pnjBundleSelected].text);
        if(isComplex) pnjResponse = JsonUtility.FromJson<Sentences>(complexResponse.text);

        sentenceSelected = Random.Range(0, pnjJson.dataBase.Length);

        this.transform.GetChild(1).GetComponent<TextMesh>().text = pnjJson.dataBase[sentenceSelected].sentence;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("PlayerTextBundle") & !hasTalked)
        {
            collision.transform.SetParent(this.transform);
            collision.transform.rotation = Quaternion.Euler(this.transform.rotation.x * 360, 
                                                            this.transform.rotation.y * 360, 
                                                            collision.transform.rotation.z * 120 - 20);
            collision.transform.position = new Vector3(this.transform.position.x + Random.Range(-2,3),
                                                        this.transform.position.y + Random.Range(-2,3),
                                                        this.transform.position.z - 1);
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.GetComponent<BoxCollider>().isTrigger = true;
            
            playerText = collision.transform.GetComponent<TextMesh>();
            
            projectileTextBundleInfo = collision.transform.GetComponent<ProjectileTextBundleInfo>();
            projectileTextBundleInfo.isFading = false;
            playerChosenBundle = projectileTextBundleInfo.textBundle;
            
            hasTalked = true;
            BundleScore();
            StartCoroutine(DeSpawnPnj());
        }
    }

    public void BundleScore()
    {
        Debug.Log(playerChosenBundle + " =? " + pnjBundleSelected);
        Debug.Log(pnjBundleSelected == playerChosenBundle);

        if(playerChosenBundle == 3)
        {
            playerBundle.Score += 100;
            if(isComplex) playerBundle.Score += 100;
            playerText.color = Color.magenta;
            this.transform.GetChild(1).GetComponent<TextMesh>().text = "xD";
            return;
        }
        
        if(pnjBundleSelected == playerChosenBundle)
        {
            playerBundle.Score += 100;
            if(isComplex) playerBundle.Score += 100;
            playerText.color = Color.green;
            this.transform.GetChild(1).GetComponent<TextMesh>().text = ":D";
            return;
        }

        if((pnjBundleSelected + 4) % 3 == playerChosenBundle)
        {
            playerBundle.Score += 50;

            if(isComplex)
            {
                playerBundle.Score -= 100;
                this.transform.GetChild(1).GetComponent<TextMesh>().text = pnjResponse.dataBase[sentenceSelected].sentence;
            }
            else
            {
                this.transform.GetChild(1).GetComponent<TextMesh>().text = ":)";
            }
            playerText.color = Color.yellow;
            return;
        }
        
        if((pnjBundleSelected + 2) % 3 == playerChosenBundle)
        {
            playerBundle.Score += 20;
            
            if(isComplex)
            {
                playerBundle.Score -= 150;
                this.transform.GetChild(1).GetComponent<TextMesh>().text = pnjResponse.dataBase[sentenceSelected].sentence;
            }
            else
            {
                this.transform.GetChild(1).GetComponent<TextMesh>().text = ":|";
            }
            playerText.color = Color.red;
            return;
        }
    }

    public IEnumerator DeSpawnPnj()
    {
        while(true)
        {
            deSpawnTimer -= 0.1f;

            if (deSpawnTimer <= 0)
            {
                //to be removed
                if (!isComplex)
                {
                    Instantiate(Resources.Load<GameObject>("Pnjs/SimplePnj"));
                }
                else
                {
                    Instantiate(Resources.Load<GameObject>("Pnjs/ComplexPnj"));
                }

                StopAllCoroutines();
                Destroy(this.gameObject);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
