using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseSoundController : MonoBehaviour
{
    private AudioSource[] audioList;

    void Start()
    {
        audioList = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var rand = Random.Range(0, audioList.Length);
            audioList[rand].Play();
        }
    }
}
