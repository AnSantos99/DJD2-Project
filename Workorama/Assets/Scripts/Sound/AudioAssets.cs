using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AudioAssets
{
    // Defines what is askde on the Inspector
    // To edit in each Audio Source
    public string name;

    public AudioClip clip;
    public AudioMixer mix;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
