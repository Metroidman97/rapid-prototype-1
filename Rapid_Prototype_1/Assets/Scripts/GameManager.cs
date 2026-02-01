using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Playable area definition
    public float screenLimit;
    public float screenTop;

    // Enemy prefabs



    // Player object
    public GameObject playerPrefab;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        // Set playable area
        screenLimit = 8f;
        screenTop = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
