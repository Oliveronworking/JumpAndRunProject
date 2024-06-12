using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // für das Text-Rendering

public class Score : MonoBehaviour
{
    // Variablen
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    // Zähler des Counters
    public float scoreCount;
    // Highscore
    public float highScoreCount;

    public float pointsPerSecond;

    public bool scoreErweiterung;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoreCount += pointsPerSecond * Time.deltaTime;

        scoreText.text = "Score: " + Mathf.Round (scoreCount); // Zählt den Score
    }
}
