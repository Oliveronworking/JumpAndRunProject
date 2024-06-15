using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterManager : MonoBehaviour
{
    public characterDatabase characterDB;

    private GameObject currentCharacterModel;
    private int selectedCharacter = 0;

    public GameObject mainCanvas;
    public GameObject charactersCanvas;
    public GameObject player;

    void Start()
    {
        // Lade den zuletzt ausgewählten Charakter (falls vorhanden)
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // Zeige den ausgewählten Charakter beim Start an
        LoadCharacter(selectedCharacter);
    }

    public void NextOption()
    {
        selectedCharacter++;

        if (selectedCharacter >= characterDB.characterCount)
        {
            selectedCharacter = 0;
        }

        LoadCharacter(selectedCharacter);
    }

    public void PreviousOption()
    {
        selectedCharacter--;

        if (selectedCharacter < 0)
        {
            selectedCharacter = characterDB.characterCount - 1;
        }

        LoadCharacter(selectedCharacter);
    }

    public void SelectOption()
    {
        // Speichere den ausgewählten Charakter in PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save(); // Sicherstellen, dass die Einstellungen sofort gespeichert werden

        charactersCanvas.SetActive(false);
        player.SetActive(false);
        mainCanvas.SetActive(true);
    }

    private void LoadCharacter(int index)
    {
        // Entferne das aktuelle Modell, falls vorhanden
        if (currentCharacterModel != null)
        {
            Destroy(currentCharacterModel);
        }

        // Erstelle das neue Modell
        character character = characterDB.GetCharacter(index);

        if (character == null || character.characterModel == null)
        {
            Debug.LogError("Character or character model is null at index: " + index);
            return;
        }

        currentCharacterModel = Instantiate(character.characterModel, transform.position, Quaternion.identity);

        if (currentCharacterModel == null)
        {
            Debug.LogError("Failed to instantiate character model at index: " + index);
            return;
        }

        // Rotiere das Modell um 180 Grad um die Y-Achse
        currentCharacterModel.transform.Rotate(0, 180, 0);

        // Optional: Positioniere und rotiere das Modell, wenn nötig
        currentCharacterModel.transform.SetParent(transform);

        // Rigidbody-Konfiguration
        Rigidbody rb = currentCharacterModel.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = currentCharacterModel.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // Deaktiviere die Gravitation
        rb.isKinematic = true; // Setze das Modell auf kinematisch

        // Debugging-Informationen
        Debug.Log("Rigidbody settings: useGravity=" + rb.useGravity + ", isKinematic=" + rb.isKinematic);
    }
}