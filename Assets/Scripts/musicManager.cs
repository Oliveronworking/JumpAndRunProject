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
        if (ObjectMusic == null)
        {
            Debug.LogError("GameObject with tag 'GameMusic' not found.");
            return;
        }

        AudioSource = ObjectMusic.GetComponent<AudioSource>();
        if (AudioSource == null)
        {
            Debug.LogError("AudioSource component not found on GameObject with tag 'GameMusic'.");
            return;
        }

        //set Volume
        musicVolume = PlayerPrefs.GetFloat("volume", 0.5f); 
        AudioSource.volume = musicVolume;
        if (volumeSlider != null)
        {
            volumeSlider.value = musicVolume;
        }
        else
        {
            Debug.LogError("Volume slider not assigned in the inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioSource != null)
        {
            AudioSource.volume = musicVolume;
            PlayerPrefs.SetFloat("volume", musicVolume);
        }
    }

    public void UpdateVolume(float volume)
    {
        musicVolume = volume;
    }
}
