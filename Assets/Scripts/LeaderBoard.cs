using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public string ID;

    public int maxScores = 6;
    public TMP_Text[] entries;

    private int timerScore;

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }

    public void LoadScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i].member_id.Length > 5)
                    {
                        entries[i].text = (scores[i].rank + ".   " + scores[i].score + " | " + scores[i].member_id.Substring(0, 8));
                    }
                    else
                    {
                        entries[i].text = (scores[i].rank + ".   " + scores[i].score + " | " + scores[i].member_id);
                    }
                }

                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        entries[i].text = (i+1) + ".   none";
                    }
                }
            }
            else
            {
                Debug.Log("Failed to login !");
            }
        });
    }

    public void SubmitScore()
    {
        timerScore = (int) PlayerPrefs.GetFloat("Score");

        LootLockerSDKManager.SubmitScore(PlayerPrefs.GetString("Name", "Steve"), timerScore, ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succeed to send score !");
            }
            else
            {
                Debug.Log("Failed to send score !");
            }
        });
    }
};