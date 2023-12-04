using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip gameSong;
    Slider MainMenuVolumeSlider;
    
    static public float volume = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        MainMenuVolumeSlider = transform.Find("Slider").GetComponent<Slider>();

        MainMenuVolumeSlider.onValueChanged.AddListener(ChangeVolume);

        //set volume at beginning of 0.5
        MainMenuVolumeSlider.value = volume;
    }
     public void ChangeVolume(float value)
    {
        AudioListener.volume = MainMenuVolumeSlider.value;
        volume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
