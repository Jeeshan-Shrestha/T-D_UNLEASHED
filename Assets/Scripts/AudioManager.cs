using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    private AudioSource[] sources;
    AudioSource bgMusic;
    void Start()
    {
        sources = GetComponents<AudioSource>();
        bgMusic = sources[0];
    }

    public void PlayBgMusic()
    {
        bgMusic.Play();
    }

    public void PlayPlayer1FinishingMoveSong()
    {
        sources[1].Play();
    }

}