using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator _animator;
    private AudioSource _audio;

    private bool isOpen;

    private void Start()
    {
        isOpen = false;
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen)
            return;
        _animator.SetTrigger("Open");
        _audio.Play();
        isOpen = true;
        var col = GetComponents<BoxCollider2D>();
        foreach (var item in col)
        {
            item.enabled = false;
        }
    }
}
