using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionsMenu : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject mainCanvas;
    // Start is called before the first frame update
    public void OnBackButton()
    {
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
