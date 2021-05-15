using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public int arrayNum;
    public MainMusic[] mainMusic;
    public AudioSerializable[] audioFx;
    public AudioSerializable[] mechFx;

    AudioSource currentAudiosource;
    bool bMusic = true;
    float fcurrentvolume;

    //AS Específicos
    [HideInInspector] public AudioSource asCollectableLoop;
    [HideInInspector] public AudioSource asFireLoop;
    [HideInInspector] public AudioSource asAbility;

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0 || level == 1 || level == 2 || level == 3)
        {
            arrayNum = 0;
        }
        else if (level == 4)
        {
            arrayNum = 1;
        }
        else if (level == 5)
        {
            arrayNum = 2;
        }
        else
        {
            arrayNum = 0;
        }
    }

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
                audio.source.volume = 0.1f;
            }
        }

        foreach (AudioSerializable audio in audioFx)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
        }

        foreach (AudioSerializable audio in mechFx)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
        }

        //GetSources
        asCollectableLoop = GetMechFx("CollectableLoop");
        asFireLoop = GetMechFx("Fire");
        asAbility = GetMechFx("Ability");
    }

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
        if(!currentAudiosource.isPlaying && bMusic)
        {
            NextClip();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FadeOut();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            FadeIn();
        }
    }

    #region SoundCalls

    public void PlaySound(string name)
    {
        AudioSerializable audio = Array.Find(audioFx, sound => sound.name == name);
        audio.source.Play();
    }

    public void PlayMechFx(string name)
    {
        AudioSerializable audio = Array.Find(mechFx, sound => sound.name == name);
        audio.source.Play();
    }

    public AudioSource GetSound(string name)
    {
        AudioSource audio = Array.Find(audioFx, sound => sound.name == name).source;
        return audio;
    }

    public AudioSource GetMechFx(string name)
    {
        AudioSource audio = Array.Find(mechFx, sound => sound.name == name).source;
        return audio;
    }

    #endregion

    public void FadeOut()
    {
        bMusic = false;
        fcurrentvolume = currentAudiosource.volume;
        StartCoroutine(FadeSound(currentAudiosource, 2, 0));
    }

    public void FadeIn()
    {
        currentAudiosource.volume = fcurrentvolume;
        int iRandom = UnityEngine.Random.Range(0, mainMusic[arrayNum].audioMusic.Length);
        currentAudiosource = mainMusic[arrayNum].audioMusic[iRandom].source;
        currentAudiosource.volume = 0;
        currentAudiosource.Play();
        StartCoroutine(FadeSound(currentAudiosource, 2, fcurrentvolume));
        bMusic = true;

    }

    public IEnumerator FadeSound(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    public IEnumerator FadeSound(AudioSource audioSource, float duration, float targetVolume, float realVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        if (audioSource == asAbility)
        {
            asAbility.Stop();
            asAbility.volume = realVolume;
        }

        yield break;
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
