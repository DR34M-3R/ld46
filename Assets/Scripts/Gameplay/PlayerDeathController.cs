using System.Collections;
using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private HPController _player;

    void Start()
    {
        _player.GetComponent<EventSystem>().AddListener(HPEvent.DIED, OnPlayerDied);
        
    }
    

    private void OnPlayerDied(EventData e)
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }
    

}
