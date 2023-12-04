using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip gameSong;
    Slider mainMenuVolumeSlider;
    static public float volume = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        mainMenuVolumeSlider = transform.Find("Slider").GetComponent<Slider>();

        mainMenuVolumeSlider.onValueChanged.AddListener(ChangeVolume);

        //set volume at beginning of 0.5
        mainMenuVolumeSlider.value = volume;
    }
     public void ChangeVolume(float value)
    {
        AudioListener.volume = mainMenuVolumeSlider.value;
        volume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
