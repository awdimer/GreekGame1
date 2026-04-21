using UnityEngine;

public class music : MonoBehaviour
{
    public Sound[] Music;
    private bool rand;
    void Awake()
    {
        
        foreach (Sound s in Music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }
    //    StartCoroutine(MusicCoroutine());
    }

    void Update()
    {
        PlayMusic();
    }
    public void PlayMusic ()
    {
    }
 //   private IEnumerator MusicCoroutine(string name)
 //   {
  //      Sound s = Array.Find(music,sound => sound.name == name);

   //     yield return new WaitWhile(() => s.isPlaying );
    

   // }
}
