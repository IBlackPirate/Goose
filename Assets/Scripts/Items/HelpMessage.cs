using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HelpMessage : MonoBehaviour
{
    private static HelpMessage instance;
    public string HelpHeader = "Подсказка";
    public GameObject MessagePrefab;
    public float TimeToDestroy = 5;

    private void Awake()
    {
        instance = this;
    }

    public static void Show(string text, Transform position = null)
    {
        instance.ShowMessage(text, position);
    }

    public void ShowMessage(string text, Transform position = null, bool NeedDestroy = true)
    {
        if (position == null)
            position = transform;
        var message = Instantiate(MessagePrefab, position);
        message.GetComponentsInChildren<Text>().Where(x => x.text != HelpHeader).First().text = text;

        if (NeedDestroy)
            Destroy(message, TimeToDestroy);
    }
}

