using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Esc : MonoBehaviour
{
    public GameObject MenuPanel;
    public AudioSource MainTheme;

    private bool inEsc = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inEsc)
            {
                Time.timeScale = 1;
                MenuPanel.SetActive(false);
                MainTheme.Stop();
                inEsc = false;
            }
            else
            {
                Time.timeScale = 0;
                MenuPanel.SetActive(true);
                MainTheme.Play();
                inEsc = true;
            }
        } 
    }

    public void OnContinue()
    {
        MainTheme.Stop();
        inEsc = false;
    }
}
