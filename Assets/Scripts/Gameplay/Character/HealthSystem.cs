using System.Collections;
using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private EventSystem _eventSystem;
    private Stats _stats;
    void Awake()
    {
        _stats = GetComponent<Stats>();

        _eventSystem = GetComponent<EventSystem>();

        _eventSystem.AddListener(MovementEvent.JUMP, OnJump);
    //   _eventSystem.AddListener(MovementEvent.GROUNDED, OnStopped);
        _eventSystem.AddListener(MovementEvent.MOVING_LEFT_STARTED, SubtractHp);
        _eventSystem.AddListener(MovementEvent.MOVING_STOPPED, OnStopped);
        _eventSystem.AddListener(MovementEvent.MOVING_RIGHT_STARTED, SubtractHp); 
    }


    private void OnStopped(EventData e)
    {
        StopAllCoroutines();
    }

    private void SubtractHp (EventData e)
    {
        StartCoroutine(Subtracting());
    }

    private IEnumerator Subtracting()
    {
        while (true)
        {
            yield return  new WaitForSeconds(0.2f);
            _eventSystem.Dispatch(HPEvent.DAMAGE_RECEIVED, 1);
        }
    }


    private void OnJump(EventData e)
    {
        _eventSystem.Dispatch(HPEvent.DAMAGE_RECEIVED, 2);
    }
    
}
