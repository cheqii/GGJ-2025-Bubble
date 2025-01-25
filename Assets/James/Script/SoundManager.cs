using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public SoundName soundName;
    public AudioClip clip;
}

public enum SoundName
{
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
