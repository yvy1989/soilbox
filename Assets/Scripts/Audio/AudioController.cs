using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;

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
    
    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}
