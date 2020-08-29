using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticHelpMessage : MonoBehaviour
{
    
    [TextArea]
    public string Message;

    private bool isShowed;

    private void ShowHelp()
    {
        HelpMessage.Show(Message);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isShowed && other.tag == "Player")
        {
            ShowHelp();
            isShowed = true;
        }
    }
}
