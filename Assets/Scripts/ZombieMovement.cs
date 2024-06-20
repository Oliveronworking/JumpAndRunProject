using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 2.0f; // Geschwindigkeit des Zombies
    private Transform player; // Referenz zum Spieler

    void Start()
    {
        // Den Spieler finden
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Bewegung in Richtung des Spielers
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Den Zombie nach links ausrichten
            if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0); // 270 Grad um Y-Achse für nach links
            }
            else if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0); // 90 Grad um Y-Achse für nach rechts
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Spiel beenden oder Schaden zufügen
            Debug.Log("Game Over! Der Zombie hat den Spieler erreicht.");
        }
    }
}
