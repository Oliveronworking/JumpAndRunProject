using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject[] zombiePrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;
    private bool spawnObstacleNext = true; // True = Spawn Obstacle, False = Spawn Zombie

    void Start()
    {
        // Finde das GameObject mit dem Tag "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Überprüfe, ob das GameObject gefunden wurde
        if (playerObject != null)
        {
            // Hole die PlayerController-Komponente des GameObjects
            playerControllerScript = playerObject.GetComponent<PlayerController>();

            // Starte das periodische Erzeugen von Hindernissen oder Zombies
            InvokeRepeating("SpawnEntity", startDelay, repeatRate);
        }
        else
        {
            Debug.LogError("GameObject with tag 'Player' not found.");
        }
    }

    void SpawnEntity()
    {
        // Überprüfe, ob playerControllerScript null ist, um Fehler zu vermeiden
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            if (spawnObstacleNext)
            {
                SpawnObstacle();
            }
            else
            {
                SpawnZombie();
            }

            // Wechsel zwischen Obstacle und Zombie
            spawnObstacleNext = !spawnObstacleNext;
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacleInstance = Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        Rigidbody rb = obstacleInstance.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obstacleInstance.AddComponent<Rigidbody>();
        }
        rb.mass = 50; // Erhöhe die Masse der Boxen
    }

    void SpawnZombie()
    {
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject zombiePrefab = zombiePrefabs[zombieIndex];
        GameObject zombieInstance = Instantiate(zombiePrefab, spawnPos, zombiePrefab.transform.rotation);
        AddComponents(zombieInstance);
    }

    void AddComponents(GameObject zombie)
    {
        Rigidbody rb = zombie.AddComponent<Rigidbody>();
        rb.useGravity = true; // Schwerkraft aktivieren, damit der Zombie auf den Boden fällt
        rb.isKinematic = false; // Sicherstellen, dass der Rigidbody nicht kinematisch ist
        rb.mass = 1; // Setze eine niedrigere Masse für den Zombie

        BoxCollider collider = zombie.AddComponent<BoxCollider>();
        collider.center = new Vector3(0, 0.5f, 0); // BoxCollider-Einstellungen anpassen
        collider.size = new Vector3(1, 1, 1); // Kleineren BoxCollider verwenden

        zombie.AddComponent<ZombieMovement>();
    }
}
