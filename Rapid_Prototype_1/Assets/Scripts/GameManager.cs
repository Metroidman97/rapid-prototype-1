using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Playable area definition
    public float screenLimit;
    public float screenTop;

    // Enemy prefabs
    public GameObject enemy1Prefab;

    // Enemy spawn grid array
    private float[][] spawnGrid = new float[3][];

    // Player object
    public GameObject playerPrefab;

    // UI elements
    public TextMeshProUGUI scoreText;

    public int score;

    // Current level
    public string level;


    // Start is called before the first frame update
    void Start()
    {
        // Set playable area
        screenLimit = 6f;
        screenTop = 12f;

        // Set score to 0 and prepare score UI
        score = 0;
        UpdateScoreText();

        // Get the current level
        level = SceneManager.GetActiveScene().name;

        // Spawn player
        Instantiate(playerPrefab, new Vector2(0, -4f), Quaternion.identity);

        // Set up enemy spawn grid and spawn enemies
        SpawnEnemies();
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

    void SpawnEnemies()
    {
        // Select formation based on current level number
        switch(level)
        {
            case "Level1":
                spawnGrid[0] = new float[3] { -2f, 0f, 2f };
                spawnGrid[1] = new float[5] { -4f, -2f, 0f, 2f, 4f };
                spawnGrid[2] = new float[7] { -6f, -4f, -2f, 0f, 2f, 4f, 6f };
                break;

            case "Level2":
                spawnGrid[0] = new float[4] { -6f, -4f, 4f, 6f };
                spawnGrid[1] = new float[5] { -4f, -2f, 0f, 2f, 4f };
                spawnGrid[2] = new float[3] { -2f, 0f, 2f};
                break;

        }
        

        for (int i = 0; i < spawnGrid.Length; i++)
        {
            float Yposition = 10f; // Match the current sub array with the formation row

            switch (i)
            {
                case 0:
                    Yposition = 10f;
                    break;

                case 1:
                    Yposition = 8f;
                    break;

                case 2:
                    Yposition = 6f;
                    break;
            }

            for (int j = 0; j < spawnGrid[i].Length; j++)
            {
                Instantiate(enemy1Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity);
            }
        }
    }
}
