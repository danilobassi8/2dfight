﻿using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManagerScript : MonoBehaviour
{

    public Sound[] sounds;

    // Use this for initialization
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        
        s.source.Play();



    }
}

