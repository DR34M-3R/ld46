﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using Events;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpView : MonoBehaviour
{
    [SerializeField]
    private HPController _player;

    private float _maxPlayerHealth;
    

    [SerializeField]
    private Slider _healthSlider;


    
    void Start()
    {
        _player.GetComponent<EventSystem>().AddListener(HPEvent.CHANGED, OnHealthChanged);
        _maxPlayerHealth = _player.GetComponent<Stats>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnHealthChanged(EventData e)
    {
        var value = Convert.ToSingle(e.Data) / _maxPlayerHealth;
        _healthSlider.value = value;
    }
    
}
