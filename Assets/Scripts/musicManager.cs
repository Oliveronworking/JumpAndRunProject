using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{
    public AudioSource audioSource;
    private float musicVolume = 1f;
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        //audioSource.Play();
        musicVolume = PlayerPrefs.GetFloat("volume");
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }
    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }
}
