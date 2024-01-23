using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour

{
    // Start is called before the first frame update

    public TMP_Text timerText;
    private float timeLeft;
    void Start()
    {
        timeLeft = Time.time + 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            string mins = ((int)timeLeft / 60).ToString();
            string secs = ((int)timeLeft % 60).ToString();

            timerText.text = mins + ":" + secs;
        }
    }
}
