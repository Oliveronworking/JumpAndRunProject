using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject optionsCanvas;
    public GameObject mainCanvas;
    // Start is called before the first frame update
    public void OnPlayButton()
    {
        Debug.Log("Play");
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
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
