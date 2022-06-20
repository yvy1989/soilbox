using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxSounds : MonoBehaviour
{
    public GameObject checkEfects;
    public GameObject checkMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkEfects.SetActive (!AudioController.Instance.effectSource.mute);
        checkMusic.SetActive (!AudioController.Instance.musicSource.mute);
    }
}
