using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableController : MonoBehaviour
{
    public MainMusic _Main;
    public AudioClip finalClip;
    void Start()
    {
        _Main = FindObjectOfType<MainMusic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;

        other.gameObject.GetComponent<PlayerDeathController>().enabled = false;
        _Main.GetComponent<AudioSource>().clip = finalClip;
        _Main.GetComponent<AudioSource>().volume = .85f;
        _Main.GetComponent<AudioSource>().Play();
    }
}
