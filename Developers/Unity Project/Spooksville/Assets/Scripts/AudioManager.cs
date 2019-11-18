using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {   
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(VolumeFade(s, s.source, 0f, .4f));
    }

    public void Stop(string name, float fadeLength)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(VolumeFade(s, s.source, 0f, fadeLength));
    }

    private IEnumerator VolumeFade(Sound sound, AudioSource audioSource, float endVolume, float fadeLength)
    {
        var startVolume = audioSource.volume;
        var startTime = Time.time;

        while (Time.time < startTime + fadeLength)
        {
            audioSource.volume = startVolume + ((endVolume - startVolume) * ((Time.time - startTime) / fadeLength));
            yield return null;
        }

        if (endVolume == 0) audioSource.Stop();

        sound.source.volume = sound.volume;
    }

    public Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }
}
