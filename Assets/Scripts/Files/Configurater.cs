using UnityEngine;

class Configurater:MonoBehaviour
{
    private void Awake()
    {
        CurrentConfig.Awake();
        if (!CurrentConfig.IsInitialize)
        {
            CurrentConfig.Level = 1;
            CurrentConfig.IsFullScreen = false;
            CurrentConfig.Quality = 3;
            CurrentConfig.MenuVolume = 1;
            CurrentConfig.GameVolume = 1;
            CurrentConfig.Resolution = Screen.resolutions.Length - 1;
        }
    }

}

