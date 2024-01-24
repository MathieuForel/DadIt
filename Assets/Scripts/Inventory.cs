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

    public TMP_Text animal;
    public TMP_Text sport;
    public TMP_Text people;

    public Image fillAmount;
    public PlayerMovement pm;
    private int beerNum;

    void UpdateUI()
    {
        animal.text = "Animals : " + animals;
        sport.text = "Sports : " + sports;
        people.text = "Peoples : " + peoples;

        fillAmount.fillAmount = 0.25f * beerNum;
    }

    void Update()
    {
        if (pm.isMojo)
        {
            
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bundle1"))
        {
            Destroy(col.gameObject);
            animals++;
        }
        if (col.gameObject.CompareTag("Bundle2"))
        {
            Destroy(col.gameObject);
            sports++;
        }
        if (col.gameObject.CompareTag("Bundle3"))
        {
            Destroy(col.gameObject);
            peoples++;
        }

        if (!pm.isMojo && col.gameObject.CompareTag("Beer"))
        {
            Destroy(col.gameObject);
            
            beerNum++;
            if (beerNum == 4)
            {
                pm.isMojo = true;
            }
        }

        UpdateUI();
    }
}
