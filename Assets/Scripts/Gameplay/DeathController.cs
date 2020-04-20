using System.Collections;
using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

public class DeathController : MonoBehaviour
{


    void Start()
    {
        GetComponent<EventSystem>().AddListener(HPEvent.DIED, OnDied);
        
    }
    

    private void OnDied(EventData e)
    {
        Debug.LogWarning("feewfwefwefwefwefwefwfew");
  //      _gameOverPanel.SetActive(true);
      //  Time.timeScale = 0;
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponent<BoxCollider2D>().enabled = false;
      gameObject.GetComponent<CircleCollider2D>().enabled = false;
      Destroy(gameObject, 2);

    }

}
