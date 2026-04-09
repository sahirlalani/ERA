using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    
    public Sound[] Musicsounds, SFXsounds;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in Musicsounds)
        {
            s.Source = audioSource;
            s.Source.clip = s.clip;
            s.Source.loop = true;
            s.Source.volume = s.volume;
        }
        
        foreach (Sound s in SFXsounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;
            s.Source.loop = false;
            s.Source.volume = s.volume;
        }
        
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(Musicsounds, sound => sound.name == name);
        s.Source.loop = true;
        s.Source.Play();
        //Debug.LogWarning(s.name);
        //Debug.LogWarning(s.clip);
        //Debug.LogWarning(s.volume);
        //Debug.LogWarning(s.Source);
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFXsounds, sound => sound.name == name);
        s.Source.PlayOneShot(s.clip, s.volume);
    }

    /*public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    */
}
