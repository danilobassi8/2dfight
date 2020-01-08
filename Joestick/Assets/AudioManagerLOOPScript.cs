using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManagerLOOPScript : MonoBehaviour
{

    public bool EnLoop = true;

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
            s.source.loop = EnLoop;

        }
    }

    void Start()
    {
        Play("viento");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        s.source.Play();
    }
}
