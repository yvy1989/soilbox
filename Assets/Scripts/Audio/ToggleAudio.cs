using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{

    public void toggleMusic()
    {
        AudioController.Instance.ToggleMusic();
    }

    public void toggleEffects() 
    {
        AudioController.Instance.ToggleEffects();
    }
}
