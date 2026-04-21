using UnityEngine;
using System.Collections;
using System;
public class music_manager : MonoBehaviour
{
    public music_area[] area;
    private music_area a;
    private Sound songToPlay;
    private bool isplayingSong = false;
    
    private bool rand;
    void Awake()
    {
        music_area a = area[0];
        foreach (music_area i in area){
            
            foreach (Sound s in a.music)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }
    }
    public void musicToPlay()
    {
        
        songToPlay = a.music[UnityEngine.Random.Range(0, a.music.Length)];
;
        StartCoroutine(MusicCoroutine(songToPlay));

    }

    void Update()
    {
        if (!isplayingSong)
        {
            musicToPlay();
        }
    }
    public void PlayMusic ()
    {
    }
    private IEnumerator MusicCoroutine(Sound songToPlay)
    {
        Sound s = songToPlay;
        isplayingSong = true;
        s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
        s.source.Play();
        yield return new WaitWhile(() => s.isPlaying );
        isplayingSong = false;

    }
}
[System.Serializable]
public class music_area 
{
    public string name;
    public Sound[] music;
}