using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private AudioSource _audioSource;

    public int indexTutorial;
    
    public List<AudioClip> Sounds = new List<AudioClip>();
    public BoxCollider2D[] _colliders;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _colliders = GetComponentsInChildren<BoxCollider2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        _audioSource.clip = Sounds[indexTutorial];
        _audioSource.Play();
        _colliders[indexTutorial].enabled = false;
        indexTutorial++;
        
    }
}
