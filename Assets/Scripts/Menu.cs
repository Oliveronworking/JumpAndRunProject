using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject mainCanvas;
    public GameObject charactersCanvas;
    public GameObject Player;
    public GameObject weaponCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Player.SetActive(true);
        DontDestroyOnLoad(Player);
    }

    public void OnPlayButton()
    {
        Player.SetActive(true);
        Debug.Log("Loading GameScenes...");
        SceneManager.LoadScene("GameScenes");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScenes")
        {
            Debug.Log("GameScenes loaded.");
            // Füge hier Code hinzu, um sicherzustellen, dass alle notwendigen Objekte aktiviert sind
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnOptionsButton()
    {
        optionsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        Player.SetActive(false);
    }

    public void OnCharactersButton()
    {
        charactersCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void OnWeaponSelectButtonClick()
    {
        mainCanvas.SetActive(false);
        Player.SetActive(false);
        weaponCanvas.SetActive(true);
    }
}
