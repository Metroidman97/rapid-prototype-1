using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource SFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameObject
        AudioSource audioSource = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioClip
        audioSource.clip = audioClip;

        //assign volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get length of SFX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}
