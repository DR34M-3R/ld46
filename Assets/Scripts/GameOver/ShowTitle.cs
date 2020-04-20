using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTitle : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GetComponent<Animator>().SetTrigger("Go");
    }


}
