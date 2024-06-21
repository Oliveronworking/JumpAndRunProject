using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponDatabase weaponDB;

    private GameObject currentWeaponModel;
    private int selectedWeapon = 0;

    public GameObject mainCanvas;
    public GameObject weaponsCanvas;
    private GameObject weaponParent; // Das leere GameObject "Weapon"

    void Start()
    {
        // Finde das Weapon-Parent-GameObject zur Laufzeit
        weaponParent = GameObject.Find("Weapon");
        if (weaponParent == null)
        {
            Debug.LogError("Weapon-Parent-GameObject mit dem Namen 'Weapon' nicht gefunden!");
            return;
        }

        // Prüfe, ob weaponDB zugewiesen ist
        if (weaponDB == null)
        {
            Debug.LogError("WeaponDatabase ist nicht zugewiesen!");
            return;
        }

        // Lade die zuletzt ausgewählte Waffe (falls vorhanden)
        selectedWeapon = PlayerPrefs.GetInt("SelectedWeapon", 0);

        // Zeige die ausgewählte Waffe beim Start an
        LoadWeapon(selectedWeapon);
    }

    public void NextOption()
    {
        selectedWeapon++;

        if (selectedWeapon >= weaponDB.weaponCount)
        {
            selectedWeapon = 0;
        }

        LoadWeapon(selectedWeapon);
    }

    public void PreviousOption()
    {
        selectedWeapon--;

        if (selectedWeapon < 0)
        {
            selectedWeapon = weaponDB.weaponCount - 1;
        }

        LoadWeapon(selectedWeapon);
    }

    public void SelectOption()
    {
        // Speichere die ausgewählte Waffe in PlayerPrefs
        PlayerPrefs.SetInt("SelectedWeapon", selectedWeapon);
        PlayerPrefs.Save(); // Sicherstellen, dass die Einstellungen sofort gespeichert werden

        weaponsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    private void LoadWeapon(int index)
    {
        // Entferne das aktuelle Modell, falls vorhanden
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }

        // Prüfe, ob der Index gültig ist
        if (index < 0 || index >= weaponDB.weaponCount)
        {
            Debug.LogError("Ungültiger Waffenindex: " + index);
            return;
        }

        // Erstelle das neue Modell
        Weapon weapon = weaponDB.GetWeapon(index);

        if (weapon == null || weapon.weaponModel == null)
        {
            Debug.LogError("Waffe oder Waffenmodell ist null bei Index: " + index);
            return;
        }

        // Instanziiere das Waffenmodell unter dem Weapon-Parent-GameObject
        currentWeaponModel = Instantiate(weapon.weaponModel, weaponParent.transform);

        // Die Waffe Rotieren
        currentWeaponModel.transform.localPosition = Vector3.zero;
        currentWeaponModel.transform.localRotation = Quaternion.identity;
        currentWeaponModel.transform.Rotate(-90, 90, 0);

        // Rigidbody-Konfiguration
        Rigidbody rb = currentWeaponModel.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = currentWeaponModel.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; 
        rb.isKinematic = true; 
        rb.mass = 60.0f;

        Debug.Log("Waffe erfolgreich geladen: " + weapon.weaponModel.name);
    }
}
