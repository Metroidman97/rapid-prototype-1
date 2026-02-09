using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float duration = 0.25f;

    [SerializeField] private AudioClip explosionSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position, 1f);
        Destroy(this.gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
