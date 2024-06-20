using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // Array deiner Zombie-Prefabs
    public float spawnInterval = 2.0f; // Zeitintervall zwischen den Spawns
    public float spawnDistance = 10.0f; // Distanz vor dem Spieler, wo die Zombies spawnen
    public float minSpawnDistance = 2.0f; // Mindestabstand vom Spieler
    private Transform playerTransform;

    void Start()
    {
        Debug.Log("ZombieSpawner started in scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (playerTransform != null)
            {
                Vector3 spawnPosition;
                do
                {
                    // Berechne eine zufällige Position innerhalb des Spawn-Bereichs
                    float randomX = playerTransform.position.x + Random.Range(spawnDistance, spawnDistance + 5);
                    float randomY = playerTransform.position.y;
                    float randomZ = playerTransform.position.z;
                    spawnPosition = new Vector3(randomX, randomY, randomZ);
                }
                while (Vector3.Distance(spawnPosition, playerTransform.position) < minSpawnDistance); // Überprüfe den Mindestabstand

                int zombieIndex = Random.Range(0, zombiePrefabs.Length);
                GameObject zombiePrefab = zombiePrefabs[zombieIndex];

                GameObject zombieInstance = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                AddComponents(zombieInstance);

                Debug.Log("Zombie spawned at: " + spawnPosition);
            }
            else
            {
                Debug.LogError("Player Transform not found.");
            }
        }
    }

    void AddComponents(GameObject zombie)
    {
        Rigidbody rb = zombie.AddComponent<Rigidbody>();
        rb.useGravity = true; // Schwerkraft aktivieren, damit der Zombie auf den Boden fällt
        rb.isKinematic = false; // Sicherstellen, dass der Rigidbody nicht kinematisch ist

        BoxCollider collider = zombie.AddComponent<BoxCollider>();
        collider.center = new Vector3(0, 0.5f, 0); // BoxCollider-Einstellungen anpassen
        collider.size = new Vector3(1, 1, 1); // Kleineren BoxCollider verwenden

        zombie.AddComponent<ZombieMovement>();
    }
}
