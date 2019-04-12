using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    // Make it a singleton
    private void Awake()
    {
        int numberOfAudioPlayers = FindObjectsOfType<LoadSceneOnClick>().Length;

        if (numberOfAudioPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void LoadMainMenu()
    {
        //StartCoroutine(Fading("MainMenu"));
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel1()
    {
        //StartCoroutine(Fading("UpdatedFirstLevel"));
        SceneManager.LoadScene("Level_1");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
