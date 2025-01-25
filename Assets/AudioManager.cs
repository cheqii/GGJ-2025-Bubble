using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.isPlayed = false;
        }   
    }

    void Start(){
        Play("BGM");
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (!s.isPlayed){
            s.source.Play();
            s.isPlayed = true;
        }
    }
    public void Reset(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        s.isPlayed = false;
    }
}
