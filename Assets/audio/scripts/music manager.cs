using UnityEngine;
using System.Collections;
using System;
public class music_manager : MonoBehaviour
{
    public music_area[] area;
    private music_area a;
    private Sound songToPlay;
    public bool isplayingSong = false;
    
    private bool rand;
    void Awake()
    {
        foreach (music_area i in area){
            
            foreach (Sound s in i.music)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }
         a = area[0];
    }
    public void musicToPlay()
    {
        
        songToPlay = area[0].music[UnityEngine.Random.Range(0, a.music.Length)];
        StartCoroutine(MusicCoroutine(songToPlay));

    }

    void Update()
    {
        if (!isplayingSong)
        {
            musicToPlay();
        }
        Debug.Log(songToPlay.source.isPlaying);
    }
    public void PlayMusic ()
    {
    }
    private IEnumerator MusicCoroutine(Sound songToPlay)
    {
        isplayingSong = true;

        songToPlay.source.Play();
        yield return new WaitWhile(() => songToPlay.source.isPlaying );
        Debug.Log("songToPlay.source.isPlaying");
        isplayingSong = false;

    }
}
[System.Serializable]
public class music_area 
{
    public string name;
    public Sound[] music;
}