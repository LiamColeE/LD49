using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    List<AudioClip> songs = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        source.clip = songs[Random.Range(0,songs.Count)];
        source.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = songs[Random.Range(0, songs.Count)];
            source.Play();
        }
    }
}
