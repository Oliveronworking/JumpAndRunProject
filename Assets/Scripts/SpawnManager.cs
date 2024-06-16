using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    void Start()
    {
        // Finde das GameObject mit dem Tag "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Überprüfe, ob das GameObject gefunden wurde
        if (playerObject != null)
        {
            // Hole die PlayerController-Komponente des GameObjects
            playerControllerScript = playerObject.GetComponent<PlayerController>();

            // Starte das periodische Erzeugen von Hindernissen
            InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        }
        else
        {
            Debug.LogError("GameObject with tag 'Player' not found.");
        }
    }

    void SpawnObstacle()
    {
        // Überprüfe, ob playerControllerScript null ist, um Fehler zu vermeiden
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
