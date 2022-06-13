using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;

    public AudioClip [] EffectsClips;
    public AudioClip [] MusicsClips;

    public AudioSource musicSource, effectSource;
    // Start is called before the first frame update

    private void Start()
    {
        musicSource.clip = MusicsClips[0];
        musicSource.Play();
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayEfect(int efectIndex)
    {
        effectSource.PlayOneShot(EffectsClips[efectIndex]); 
    }

    public void PlayMusic(int musicIndex)
    {
        musicSource.PlayOneShot(MusicsClips[musicIndex]);
    }

    public void changeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void changeMusicToGame()
    {
        musicSource.Stop();
        musicSource.clip = MusicsClips[1];
        musicSource.Play();
    }

    public void changeMusicToMenu()
    {
        musicSource.Stop();
        //musicSource.PlayOneShot(MusicsClips[1]);
        musicSource.clip = MusicsClips[0];
        musicSource.Play();
    }
}
