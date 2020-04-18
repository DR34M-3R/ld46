using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private PauseView _pauseView;
    private void Start()
    {
        _pauseView = GetComponent<PauseView>();
        _pauseView.IsActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            _pauseView.IsActive = !_pauseView.IsActive;
        }
    }
}
