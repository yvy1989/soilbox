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
}
