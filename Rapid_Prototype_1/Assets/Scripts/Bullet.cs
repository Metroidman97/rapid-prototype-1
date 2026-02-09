using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Declare variables
    public float speed = 8f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make the bullet move forward, regardless of direction
        transform.Translate(new Vector2(0, 1) * Time.deltaTime * speed);

        // Delete bullet when it reaches the top or bottom of the screen
        float screenTop = gameManager.screenTop;
        float screenBottom = gameManager.screenBottom;
        
        if (transform.position.y > screenTop || transform.position.y < screenBottom)
        {
            Destroy(this.gameObject);
        }
    }
}
