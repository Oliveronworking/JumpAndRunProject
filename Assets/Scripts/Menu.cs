using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject optionsCanvas;
    public GameObject mainCanvas;
    public GameObject characktersCanvas;
    public GameObject Player;
    // Start is called before the first frame update

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

    public void OnCharacktersButton()
    {
        characktersCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    void Start()
    {
        Player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
