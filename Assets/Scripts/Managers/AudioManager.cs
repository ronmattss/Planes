using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

enum SFX
{
    MissileEngine,
    Explosion
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    void Start()
    {
        //play Theme;
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public AudioClip GetSound(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        return s.clip;
    }
}
