using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas Canvas;
    public Esc esc;

    public void StartPressed()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void ContinuePressed()
    {
        esc.OnContinue();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void QuitPressed()
    {
        CurrentConfig.OnQuit();
        Application.Quit();
    }
}
