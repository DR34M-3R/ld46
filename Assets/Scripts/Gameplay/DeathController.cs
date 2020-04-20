using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
=======
>>>>>>> master
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
<<<<<<< HEAD
  //      _gameOverPanel.SetActive(true);
      //  Time.timeScale = 0;
    //  gameObject.GetComponent<BoxCollider2D>().enabled = false;
    //  gameObject.GetComponent<CircleCollider2D>().enabled = false;

    var col = GetComponents<Collider2D>();
    foreach (var item in col)
    {
        item.enabled = false;
    }
    
    

    if (gameObject.CompareTag("Guard"))
    {
        var rigidbody = GetComponentsInChildren<Rigidbody2D>().Skip(1);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        foreach (var i in rigidbody)
        {
            i.transform.localRotation = Random.rotation;
            i.simulated = true;
            i.GetComponent<SpriteRenderer>().enabled = true;
            i.AddForce(Random.insideUnitCircle * Random.Range(50, 150));
            Destroy(gameObject, 10);
        }
    }
    else
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Animator>().SetTrigger("Die");
        Destroy(gameObject, 10);

    }
     


    }

=======
        Debug.LogWarning("feewfwefwefwefwefwefwfew");
  //      _gameOverPanel.SetActive(true);
      //  Time.timeScale = 0;
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      gameObject.GetComponent<BoxCollider2D>().enabled = false;
      gameObject.GetComponent<CircleCollider2D>().enabled = false;
      Destroy(gameObject, 2);

    }
>>>>>>> master

}
