using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed = 5.0f; // Erhöhte Geschwindigkeit des Zombies
    private Transform player; // Referenz zum Spieler
    private bool isPlayerDead = false;

    void Start()
    {
        // Den Spieler finden
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null && !isPlayerDead)
        {
            // Bewegung in Richtung des Spielers
            Vector3 direction = Vector3.left; // Fest nach links
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Den Zombie nach links ausrichten
            transform.rotation = Quaternion.Euler(0, 270, 0); // 270 Grad um Y-Achse für nach links

            // Überprüfen, ob der Zombie hinter dem Spieler ist
            if (transform.position.x < player.position.x - 5) // 5 Einheiten hinter dem Spieler als Beispiel
            {
                Destroy(gameObject);
                Debug.Log("Zombie destroyed: " + transform.position);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Spiel beenden
            Debug.Log("Game Over! Der Zombie hat den Spieler erreicht.");
            isPlayerDead = true;
            // Füge hier deine Game Over-Logik ein
            GameOver();
        }
    }

    void GameOver()
    {
        // Beispiel-Game Over-Logik
        // Hier kannst du deinen Game Over-Bildschirm anzeigen oder den Spieler stoppen
        Time.timeScale = 0; // Spiel anhalten
        // Aktiviere Game Over-Canvas
        GameObject gameOverCanvas = GameObject.Find("GameOverCanvas");
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
}
