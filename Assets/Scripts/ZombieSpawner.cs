using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs; // Array deiner Zombie-Prefabs
    public float spawnInterval = 2.0f; // Zeitintervall zwischen den Spawns
    public float spawnDistance = 10.0f; // Distanz vor dem Spieler, wo die Zombies spawnen
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
                Vector3 spawnPosition = new Vector3(playerTransform.position.x + spawnDistance, playerTransform.position.y, playerTransform.position.z);
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

        BoxCollider collider = zombie.AddComponent<BoxCollider>();

        zombie.AddComponent<ZombieMovement>();
    }
}
