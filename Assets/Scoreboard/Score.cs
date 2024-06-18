using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // für das Text-Rendering

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;

    public bool scoreErweiterung;

    private PlayerController playerControllerScript;

    void Start()
    {
        // Finde das GameObject mit dem Namen "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Überprüfe, ob das GameObject gefunden wurde
        if (playerObject != null)
        {
            // Hole die PlayerController-Komponente des GameObjects
            playerControllerScript = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("GameObject with name 'Player' not found.");
        }
    }

    void Update()
    {
        // Überprüfe, ob playerControllerScript null ist, um Fehler zu vermeiden
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.Round(scoreCount);
        }
    }
}
