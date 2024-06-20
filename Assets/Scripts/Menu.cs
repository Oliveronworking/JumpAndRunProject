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

    // Start is called before the first frame update
    void Start()
    {
        Player.SetActive(true);
    }

    public void OnPlayButton()
    {
        Player.SetActive(true);
        SceneManager.LoadScene(1);
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
    }
}
