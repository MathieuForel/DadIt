using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    private int animals;
    private int sports;
    private int peoples;

    public AudioSource[] sounds;

    public TMP_Text animal;
    public TMP_Text sport;
    public TMP_Text people;

    public Image fillAmount;
    public PlayerMovement pm;
    private int beerNum;
    public float mojoTimer;

    public Vector3[] pos;
    public int currentIndex;
    public RectTransform select;

    void UpdateUI()
    {
        animal.text = animals.ToString();
        sport.text = sports.ToString();
        people.text = peoples.ToString();
    }

    void Update()
    {
        if (pm.isMojo && mojoTimer > 0f)
        {
            fillAmount.fillAmount -= Time.deltaTime/15f;
            mojoTimer -= Time.deltaTime;
        }
        else if (mojoTimer <= 0f)
        {
            beerNum = 0;
            mojoTimer = 15f;
            pm.isMojo = false;
        }

        PlayerInput();
    }

    void PlayerInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentIndex++;
            if (currentIndex > 2) currentIndex = 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = 2;
        }

        select.anchoredPosition = pos[currentIndex];
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bundle1"))
        {
            Destroy(col.gameObject);
            sports++;
            sounds[0].Play();
        }
        if (col.gameObject.CompareTag("Bundle2"))
        {
            Destroy(col.gameObject);
            peoples++;
            sounds[1].Play();
        }
        if (col.gameObject.CompareTag("Bundle3"))
        {
            Destroy(col.gameObject);
            animals++;
            sounds[2].Play();
        }

        if (beerNum < 4 && col.gameObject.CompareTag("Beer"))
        {
            Destroy(col.gameObject);
            
            fillAmount.fillAmount += 0.25f;
            beerNum++;
            
            
            if (beerNum == 4)
            {
                pm.isMojo = true;
                sounds[4].Play();
            }
            else
            {
                sounds[3].Play();
            }
        }

        UpdateUI();
    }
}
