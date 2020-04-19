using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

public class SuckAttackController : MonoBehaviour
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
        
        _eventSystem.AddListener(ActionEvent.ATTACK, Attack);
    }

    private void Attack(EventData e)
    {
        var eventTmp = GetEventSystem();
        
        if (eventTmp == null)
            return;
        
        if (!eventTmp.GetComponent<Stats>().isSuck)
            return;
            
        Debug.DrawRay(Transform.position, Direction * Distance, Color.red, 0.2f);
        GetEventSystem()?.Dispatch(HPEvent.DAMAGE_RECEIVED, _suckDamage);
    }

    private void Update()
    {
        Attack(null);
    }

    private EventSystem GetEventSystem()
    {

        RaycastHit2D hit = Physics2D.Raycast(Transform.position, Direction,  Distance);
        Debug.DrawRay(Transform.position, Direction * Distance, Color.green);

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
