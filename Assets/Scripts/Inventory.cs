using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int animals;
    public int sports;
    public int peoples;

    public TMP_Text animal;
    public TMP_Text sport;
    public TMP_Text people;
    
    void UpdateUI()
    {
        animal.text = "Animals : " + animals;
        sport.text = "Sports : " + sports;
        people.text = "Peoples : " + peoples;
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

        UpdateUI();
    }
}
