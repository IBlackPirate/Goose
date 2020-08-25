using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixerGroup Mixer;

    public void MenuVolumePressed(float volume)
    {
        CurrentConfig.MenuVolume = volume;
        Mixer.audioMixer.SetFloat("MenuVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void GameVolumePressed(float volume)
    {
        CurrentConfig.GameVolume = volume;
        Mixer.audioMixer.SetFloat("GameVolume", Mathf.Lerp(-80, 0, volume));
    }
}
