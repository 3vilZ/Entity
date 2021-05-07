using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public int arrayNum;
    public MainMusic[] mainMusic;
    public AudioSerializable[] audioFx;

    

    AudioSource currentAudiosource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < mainMusic.Length; i++)
        {
            foreach (AudioSerializable audio in mainMusic[i].audioMusic)
            {
                audio.source = gameObject.AddComponent<AudioSource>();
                audio.source.clip = audio.clip;
                audio.source.volume = audio.volume;
            }
        }

        foreach (AudioSerializable audio in audioFx)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
        }
    }
    
    /*
    private void Start()
    {
        NextClip();
    }

    public void NextClip()
    {
        int iRandom = UnityEngine.Random.Range(0, mainMusic[arrayNum].audioMusic.Length);
        currentAudiosource = mainMusic[arrayNum].audioMusic[iRandom].source;
        currentAudiosource.Play();
    }

    private void Update()
    {
        if(!currentAudiosource.isPlaying)
        {
            NextClip();
        }
    }
    */
 
    public void PlaySound(string name)
    {
        AudioSerializable audio = Array.Find(audioFx, sound => sound.name == name);
        audio.source.Play();
    }

    public AudioSource GetSound(string name)
    {
        AudioSource audio = Array.Find(audioFx, sound => sound.name == name).source;
        return audio;
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

[System.Serializable]
public class MainMusic
{
    public AudioSerializable[] audioMusic;
}
