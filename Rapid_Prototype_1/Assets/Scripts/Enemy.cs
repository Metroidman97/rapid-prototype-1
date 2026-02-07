using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;

    private GameManager gameManager;

    private int scoreValue;

    private Vector2 rowSpawn;

    private Vector2 formationPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreValue = 1;                                                                 // Set the score value
        formationPosition = new Vector2(transform.position.x, transform.position.y);    // Set the position of the enemy when they first spawn

        // Move the enemy to the sides of the screen
        GetRowSpawn();

        // Move the enemy to its position in the formation
        //Invoke(nameof(MoveToPosition), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(scoreValue);
            gameManager.DecrimentEnemies();
            Destroy(this.gameObject);
        }
    }

    void GetRowSpawn()
    {
        if (gameObject.name == "Enemy1(Clone)" || gameObject.name == "Enemy4(Clone)")
        {
            rowSpawn = GameObject.Find("Row3Spawn").transform.position;
        }
        else if (gameObject.name == "Enemy2(Clone)" || gameObject.name == "Enemy5(Clone)")
        {
            rowSpawn = GameObject.Find("Row2Spawn").transform.position;
        }
        else if (gameObject.name == "Enemy3(Clone)" || gameObject.name == "Enemy6(Clone)")
        {
            rowSpawn = GameObject.Find("Row1Spawn").transform.position;
        }

        gameObject.transform.position = rowSpawn;
    }

    void MoveToPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, formationPosition, Time.deltaTime * 6f);
    }
}