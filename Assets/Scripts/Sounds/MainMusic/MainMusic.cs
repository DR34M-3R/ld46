using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    public List<AudioClip> rndSounds = new List<AudioClip>();

    public AudioClip MainSound;

    private AudioSource _source;

    private AudioClip previousClip;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = MainSound;
        _source.Play();
    }

    void Update()
    {
        if (_source.isPlaying)
            return;;
        do
        {
            _source.clip = rndSounds[Random.Range(0, rndSounds.Count)];
        } while (_source.clip == previousClip);
        _source.Play();
        
    }
}
