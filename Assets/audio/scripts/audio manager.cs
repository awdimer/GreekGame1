using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private bool rand;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            rand = s.ran;
            
        }
    }

    void Update()
    {
        
    }
    public void Play (string name)
    {
        Sound s = Array.Find(sounds,sound => sound.name == name);
        if(rand){
            s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
            Debug.Log(UnityEngine.Random.Range(0, s.clip.Length));
        }
        s.source.Play();
    }
}
