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
        StartCoroutine(LoadGameOver());
        Time.timeScale = 0.25f;
        

    }


    private IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(.5f);
        _gameOverPanel.SetActive(true);
    }
    

}
