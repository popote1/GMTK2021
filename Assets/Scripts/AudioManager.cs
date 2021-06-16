using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour
{
    
    
    public static GameObject AudioHolder;

    public static float MusicVolume=1;
    public static float SoundVolume=1;

    public  static AudioSource MusicPlayer = new AudioSource();
    
    
    
    void Awake()
    {
        /*if (GameObject.Find(transform.name) != null) {
            if (GameObject.Find(transform.name)!=gameObject)
                Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
        AudioHolder = gameObject;
    }

    // Update is called once per frame
    public static void PlaySound(AudioClip audioClip, float volume = 1)
    {
        AudioSource audioSource = AudioHolder.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume * SoundVolume;
        Destroy(audioSource ,audioClip.length);
        audioSource.Play();
    }
    public static void PlayMusic(AudioClip audioClip, float volume = 1)
    {
        if (MusicPlayer != null) Destroy(MusicPlayer);
        
        AudioSource audioSource   = AudioHolder.AddComponent<AudioSource>();
        MusicPlayer = audioSource;
        MusicPlayer.clip = audioClip;
        MusicPlayer.volume = MusicVolume * volume;
        MusicPlayer.loop = true;
        MusicPlayer.Play();
    }
    
    
}
