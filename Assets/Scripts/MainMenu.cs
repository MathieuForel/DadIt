using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool panelOpened = false;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPannel(GameObject pannel)
    {
        pannel.SetActive(!panelOpened);
        panelOpened = !panelOpened;
    }

    public void SetPlayerName()
    {
        PlayerPrefs.SetString("Name", "Steve");
    }
}