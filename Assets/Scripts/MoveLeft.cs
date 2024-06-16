using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    void Start()
    {
        // Finde das GameObject mit dem Tag "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Überprüfe, ob das GameObject gefunden wurde
        if (playerObject != null)
        {
            // Hole die PlayerController-Komponente des GameObjects
            playerControllerScript = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("GameObject with tag 'Player' not found.");
        }
    }

    void Update()
    {
        // Überprüfe, ob playerControllerScript null ist, um Fehler zu vermeiden
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
