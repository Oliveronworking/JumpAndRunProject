using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // f�r das Text-Rendering

public class Score : MonoBehaviour
{
    // Variablen
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    // Z�hler des Counters
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

        scoreText.text = "Score: " + Mathf.Round (scoreCount); // Z�hlt den Score
    }
}
