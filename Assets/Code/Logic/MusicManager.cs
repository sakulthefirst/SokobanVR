using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    static AudioClip[] musics;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        PlayRandom();
    }
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandom();
        }
        musics = Resources.LoadAll<AudioClip>("Music"); 
    }

    private void PlayRandom()
    {

        int index = Random.Range(0, musics.Length);
        var clip = musics[index];
        audioSource.clip = clip;
        audioSource.Play();
    }
}
