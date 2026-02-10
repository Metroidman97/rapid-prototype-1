using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Prefabs
    public GameObject explosionPrefab;
    public GameObject enemyBulletPrefab;

    private GameManager gameManager;

    // The score value awarded upon death
    private int scoreValue;

    // Offscreen starting position
    private Vector2 rowSpawn;

    // Position in the formation
    private Vector2 formationPosition;

    // Row number in the formation
    private int rowNum;

    //public bool isMoving = true;

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

        if ((transform.position.x == formationPosition.x) && (transform.position.y == formationPosition.y))
        {
            //isMoving = false;
            StopCoroutine(nameof(MoveToPosition));
        }
    }

    // When hit by player bullet
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
        if (gameObject.name == "BO_Bug_Enemy(Clone)" || gameObject.name == "CWF_Bug_Enemy(Clone)")
        {
            rowSpawn = GameObject.Find("Row3Spawn").transform.position;
            rowNum = 3;
        }
        else if (gameObject.name == "MW_Bug_Enemy(Clone)" || gameObject.name == "CitrusMealybug1(Clone)")
        {
            rowSpawn = GameObject.Find("Row2Spawn").transform.position;
            rowNum = 2;
        }
        else if (gameObject.name == "Cat_Bug_Enemy(Clone)" || gameObject.name == "Bee_Bug_Enemy(Clone)")
        {
            rowSpawn = GameObject.Find("Row1Spawn").transform.position;
            rowNum = 1;
        }

        gameObject.transform.position = rowSpawn;
    }

    // Move into formation position from offscreen
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
    }

    public void Shoot()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));

        //Debug.Log("Pow");
    }

    /*
    public IEnumerator Move()
    {
        //isMoving = true;

        //Vector2 newPosition = new Vector2(0f, -6f);
        Vector2 newPosition = gameManager.playerPrefab.transform.position;

        yield return new WaitForEndOfFrame();
        transform.position = Vector2.MoveTowards(transform.position, newPosition, Time.deltaTime * 2f);

        if ((transform.position.x == newPosition.x) && (transform.position.y == newPosition.y))
        {
            //transform.position = formationPosition;
        }

        //Debug.Log(newPosition);

        //yield return null;
    }
    */
}