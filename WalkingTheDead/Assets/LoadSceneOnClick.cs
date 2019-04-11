using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void BackToMainMenu()
    {
        //StartCoroutine(Fading("MainMenu"));
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadFirst()
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
