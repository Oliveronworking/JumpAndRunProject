using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createweapon : MonoBehaviour
{
    public GameObject[] weapons; // Array mit den verfügbaren Waffen
    private GameObject currentWeapon;

    public Transform weaponSpawnPoint; // Punkt, an dem die Waffen erstellt werden

    public void createWeapon()
    {
        Debug.Log("Weapon creation method executed!");

        // Hier eine Beispiel-Waffe auswählen und anzeigen
        if (weapons.Length > 0)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            int weaponIndex = 0; // Beispiel: immer die erste Waffe auswählen
            currentWeapon = Instantiate(weapons[weaponIndex], weaponSpawnPoint.position, weaponSpawnPoint.rotation);
            currentWeapon.transform.SetParent(weaponSpawnPoint, false);

            // Setze die lokale Position und Rotation der Waffe, damit sie korrekt angezeigt wird
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;

            Debug.Log("Weapon instantiated at local position: " + currentWeapon.transform.localPosition);
            Debug.Log("Weapon instantiated with local rotation: " + currentWeapon.transform.localRotation);
        }
        else
        {
            Debug.LogError("No weapons assigned to the weapons array.");
        }
    }
}
