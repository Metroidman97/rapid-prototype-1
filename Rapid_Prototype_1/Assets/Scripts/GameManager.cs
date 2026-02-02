using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Playable area definition
    public float screenLimit;
    public float screenTop;

    // Enemy prefabs



    // Player object
    public GameObject playerPrefab;

    // UI elements
    public TextMeshProUGUI scoreText;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        // Set playable area
        screenLimit = 8f;
        screenTop = 5f;

        // Set score to 0 and prepare score UI
        score = 0;
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score + "00";
    }
}
