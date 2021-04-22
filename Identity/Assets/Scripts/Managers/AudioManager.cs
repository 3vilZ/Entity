using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSerializable[] audioSerializable;

    private void Awake()
    {
        Instance = this;

        foreach (AudioSerializable audio in audioSerializable)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
        }
    }

    public void PlaySound(string name)
    {
        AudioSerializable audio = Array.Find(audioSerializable, sound => sound.name == name);
        audio.source.Play();
    }
}

[System.Serializable]
public class AudioSerializable
{
    public AudioClip clip;
    public string name;
    [Range(0, 1)] public float volume;
    [HideInInspector] public AudioSource source;
}
