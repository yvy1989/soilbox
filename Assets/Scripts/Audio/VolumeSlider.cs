using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.changeMasterVolume(slider.value);
        slider.onValueChanged.AddListener(val => AudioController.Instance.changeMasterVolume(val));// conforma mexe o slider troca o valor de val no change master volume
    }

}
