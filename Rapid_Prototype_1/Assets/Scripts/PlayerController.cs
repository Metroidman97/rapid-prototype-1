using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare and initialize variables
    public float speed = 6f;
    public float input;
    public int lives;

    private GameManager gameManager;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    // Control the player
    void Move()
    {
        // Read the input from the player
        input = Input.GetAxis("Horizontal");

        // Move the player
        transform.Translate(new Vector2(input, 0) * Time.deltaTime * speed);

        // Get the defined play area
        float screenLimit = gameManager.screenLimit;

        // Constrain the player to the defined play area
        if (transform.position.x > screenLimit)
        {
            transform.position = new Vector2(screenLimit, transform.position.y);
        }
        else if (transform.position.x <= -screenLimit)
        {
            transform.position = new Vector2(-screenLimit, transform.position.y);
        }
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.25f,0.5f), Quaternion.identity);
        }
    }
}
