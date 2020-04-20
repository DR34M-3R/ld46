using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;
    void Start()
    {
        _panel.SetActive(false);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Time.timeScale = 0;
        _panel.SetActive(true);
    }
}
