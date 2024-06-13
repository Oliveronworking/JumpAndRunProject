using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{

    public Slider volumeSlider;
    public GameObject ObjectMusic;

    //slider level to audio volume
    private float musicVolume = 0f;
    private AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        ObjectMusic = GameObject.FindWithTag("GameMusic");
        AudioSource=ObjectMusic.GetComponent<AudioSource>(); //

        //set Volume
        musicVolume = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = musicVolume; //
        PlayerPrefs.SetFloat("volume", musicVolume);
    }
    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }
}
