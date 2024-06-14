using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public GameObject GameOverCanvas;
    // Start is called before the first frame update

    public void mainMenuButton()
    {
        GameOverCanvas.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void restartButton()
    {
        GameOverCanvas.SetActive(false);
        /*
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        */
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
