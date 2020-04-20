using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PianoTransfer : MonoBehaviour
{
    [SerializeField]
    private Transform _teleportPosition;
    private bool isEnter = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        isEnter = true;
    }

    private void Update()
    {
        if (!isEnter)
            return;;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<AudioSource>().Play();
            GameObject.FindWithTag("Player").transform.position = _teleportPosition.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isEnter = false;
    }
}    
