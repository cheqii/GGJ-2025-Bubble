using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundPlay
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
    public List<SoundPlay> sounds = new List<SoundPlay>();
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
