using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Playable area definition
    public float screenLimit;
    public float screenTop;
    public float screenBottom;

    // Enemy prefabs
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public GameObject enemy5Prefab;
    public GameObject enemy6Prefab;

    // Enemy spawn grid array
    private float[][] spawnGrid = new float[3][];

    // Player object
    public GameObject playerPrefab;

    // UI elements
    public TextMeshProUGUI scoreText;

    public int score;

    // Current level
    public string level;

    // Number of currently active enemies
    private int remainingEnemies = 0;

    // Number of enemies in their final position
    //private int inPosition = 0;

    // Enemy list
    private List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Set playable area
        screenLimit = 6f;
        screenTop = 12f;
        screenBottom = -6f;

        // Set score to 0 and prepare score UI
        score = 0;
        UpdateScoreText();

        // Get the current level
        level = SceneManager.GetActiveScene().name;

        // Spawn player
        Instantiate(playerPrefab, new Vector2(0, -4f), Quaternion.identity);

        // Set up enemy spawn grid and spawn enemies
        SpawnEnemies();

        // Wait until all enemies are in position, then randomly call an enemy from the list to divebomb the player
        //InvokeRepeating(nameof(SelectEnemy), 15f, 5f);

        // Make the enemies shoot
        InvokeRepeating(nameof(EnemyShoot), 15f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Close game instantly by pressing escape
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Check when all enemies are dead to advance to the next level
        CheckEnemiesDead();
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
            // Enemy formation for level 1
            case "Level1":
                spawnGrid[0] = new float[3] { -2f, 0f, 2f };  // Each row is a row of enemies, and each number is the X coordinate of their formation position
                spawnGrid[1] = new float[5] { -4f, -2f, 0f, 2f, 4f };
                spawnGrid[2] = new float[7] { -6f, -4f, -2f, 0f, 2f, 4f, 6f };
                break;

            // Enemy formation for level 2
            case "Level2":
                spawnGrid[0] = new float[4] { -6f, -4f, 4f, 6f };
                spawnGrid[1] = new float[5] { -4f, -2f, 0f, 2f, 4f };
                spawnGrid[2] = new float[3] { -2f, 0f, 2f};
                break;

        }
        

        for (int i = 0; i < spawnGrid.Length; i++)
        {
            // The Y coordinate for each enemy row
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
                switch (i)
                {
                    case 0:
                        if (level == "Level1")
                        {
                            enemyList.Add(Instantiate(enemy3Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity));    // Spawn different enemies in different rows
                            remainingEnemies++;     // Increment the enemy counter with each spawn
                        }
                        else if (level == "Level2")
                        {
                            enemyList.Add(Instantiate(enemy6Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity));    // Spawn different enemies based on the current level
                            remainingEnemies++;
                        }
                        break;

                    case 1:
                        if (level == "Level1")
                        {
                            enemyList.Add(Instantiate(enemy2Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity));    
                            remainingEnemies++;     
                        }
                        else if (level == "Level2")
                        {
                            enemyList.Add(Instantiate(enemy5Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity));    
                            remainingEnemies++;
                        }
                        break;

                    case 2:
                        if (level == "Level1")
                        {
                            enemyList.Add(Instantiate(enemy1Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity));
                            remainingEnemies++;
                        }
                        else if (level == "Level2")
                        {
                            enemyList.Add(Instantiate(enemy4Prefab, new Vector2(spawnGrid[i][j], Yposition), Quaternion.identity));    
                            remainingEnemies++;
                        }
                        break;
                }
                

                
            }
        }
    }

    public void DecrimentEnemies()
    {
        // Decriment the remaining enemy count every time one is destroyed
        remainingEnemies--;
    }

    void CheckEnemiesDead()
    {
        // Check if every enemy has been killed
        if (remainingEnemies == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            // Code to advance to the next scene goes here
            Debug.Log("Next Level");
        }
    }

    /*
    void SelectEnemy()
    {
        StartCoroutine(nameof(MoveEnemy));
    }
    */

    void EnemyShoot()
    {
        GameObject enemy = EnemyListSelect();   // Select an enemy from the enemy list
        enemy.GetComponent<Enemy>().Shoot();    // Enemy fires their weapon
    }

    /*
    IEnumerator MoveEnemy()
    {
        GameObject selectedEnemy = EnemyListSelect();

        
        while (selectedEnemy.GetComponent<Enemy>().isMoving == true)
        {
            selectedEnemy = EnemyListSelect();
            yield return null;
        }
        

        StartCoroutine(selectedEnemy.GetComponent<Enemy>().Move());
        yield return null;
        
    }
    */

    GameObject EnemyListSelect()
    {
        int listIndex;

        GameObject selectedEnemy;

        listIndex = Random.Range(0, enemyList.Count);

        selectedEnemy = enemyList[listIndex];

        return selectedEnemy;
    }
}
