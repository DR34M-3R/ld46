using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

public class SuckActionController : MonoBehaviour
{
    public Transform Transform;
    
    [SerializeField]
    private float _suckDamage;
    
    public Vector2 Direction;
    
    public float Distance;

    

    private EventSystem _eventSystem;

    private void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
        
        _eventSystem.AddListener(ActionEvent.SUCK, Attack);
    }

    private void Attack(EventData e)
    {

        var eventTmp = GetEventSystem();

        if (eventTmp == null)
        {
            Debug.LogWarning("Object does not have Event System");
            return;
        }

        if (!eventTmp.GetComponent<Stats>().isSuck)
        {
            Debug.LogWarning("Object does not have suck priority");
            return;
        }
        Debug.DrawRay(Transform.position, new Vector2(transform.localScale.x, 0) * Distance, Color.red, 0.2f);
        GetEventSystem()?.Dispatch(HPEvent.DAMAGE_RECEIVED, _suckDamage);
        Destroy(eventTmp.gameObject , 1);
        GetComponent<EventSystem>()?.Dispatch(HPEvent.HEALED, 50);
    }

    private void Update()
    {
    }

    private EventSystem GetEventSystem()
    {

        RaycastHit2D hit = Physics2D.Raycast(Transform.position, new Vector2(transform.localScale.x, 0),  Distance);
        Debug.DrawRay(Transform.position, new Vector2(transform.localScale.x, 0) * Distance, Color.green);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out EventSystem eventSystem))
            {
                return eventSystem;
            }
        }
       
        return null;
    }

}
