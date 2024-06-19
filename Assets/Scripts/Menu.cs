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
    public GameObject weaponCreateObject; // Referenz zum GameObject, das das createweapon-Skript enthält

    public Button weaponSelectButton; // Referenz zum neuen Button

    // Start is called before the first frame update
    void Start()
    {
        Player.SetActive(true);

        // Initialisiere den WeaponSelectButton
        if (weaponSelectButton != null)
        {
            weaponSelectButton.onClick.AddListener(OnWeaponSelectButtonClick);
        }
        else
        {
            Debug.LogError("Weapon Select Button is not assigned.");
        }
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

    public void OnCharacktersButton()
    {
        charactersCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    void OnWeaponSelectButtonClick()
    {
        if (weaponCreateObject != null)
        {
            // Deaktiviere die Charakterauswahl und andere Canvas
            charactersCanvas.SetActive(false);
            mainCanvas.SetActive(false);

            // Zeige den Spieler und die Waffe an
            Player.SetActive(true);

            createweapon weaponCreateComponent = weaponCreateObject.GetComponent<createweapon>();

            if (weaponCreateComponent != null)
            {
                weaponCreateComponent.createWeapon();
            }
            else
            {
                Debug.LogError("createweapon component not found on the assigned GameObject.");
            }
        }
        else
        {
            Debug.LogError("weaponCreateObject is not assigned.");
        }
    }
}
