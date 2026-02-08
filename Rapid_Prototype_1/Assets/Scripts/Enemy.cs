using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject enemyBulletPrefab;

    private GameManager gameManager;

    private int scoreValue;

    private Vector2 rowSpawn;

    private Vector2 formationPosition;

    private int rowNum;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreValue = 1;                                                                 // Set the score value
        formationPosition = new Vector2(transform.position.x, transform.position.y);    // Set the position of the enemy when they first spawn

        // Move the enemy to the sides of the screen
        GetRowSpawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy to its position in the formation
        StartCoroutine(MoveToPosition());
        
        /*
        if (gameManager.EveryoneInPosition())
        {
            Debug.Log("Everyone is in position");
        }
        */
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
            rowNum = 3;
        }
        else if (gameObject.name == "Enemy2(Clone)" || gameObject.name == "Enemy5(Clone)")
        {
            rowSpawn = GameObject.Find("Row2Spawn").transform.position;
            rowNum = 2;
        }
        else if (gameObject.name == "Enemy3(Clone)" || gameObject.name == "Enemy6(Clone)")
        {
            rowSpawn = GameObject.Find("Row1Spawn").transform.position;
            rowNum = 1;
        }

        gameObject.transform.position = rowSpawn;
    }

    IEnumerator MoveToPosition()
    {
        float waitTime = 0f;
        switch(rowNum)
        {
            case 1:
                waitTime = 9f;
                break;
            case 2:
                waitTime = 6f;
                break;
            case 3:
                waitTime = 3f;
                break;
        }

        yield return new WaitForSeconds(waitTime);
        transform.position = Vector2.MoveTowards(transform.position, formationPosition, Time.deltaTime * 6f);
        /*
        if ((transform.position.x == formationPosition.x) && (transform.position.y == formationPosition.y))
        {
            gameManager.EnemyInPosition();
        }
        */
    }

    void Shoot()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 180, 0));
    }
}