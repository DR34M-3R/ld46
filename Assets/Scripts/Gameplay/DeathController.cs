﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            GetComponent<MovementController>().enabled = false;
            foreach (var i in rigidbody)
            {
                i.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                i.simulated = true;
                i.AddForce(Random.insideUnitCircle * Random.Range(50, 150));
                Destroy(gameObject, 10);
                i.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Animator>().SetTrigger("Die");
            Destroy(gameObject, 10);
        }
    }
}