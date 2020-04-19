using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

public class AttackActionController : MonoBehaviour
{
    public List<Transform> Positions = new List<Transform>();

    public Vector2 Direction;

    public float Distance;
    
    private Stats _stats;
    private EventSystem _eventSystem;

    void Start()
    {
        _stats = GetComponent<Stats>();
        _eventSystem = GetComponent<EventSystem>();
        
        _eventSystem.AddListener(ActionEvent.ATTACK, Attack);
    }

    

    private void Attack(EventData e)
    {
        GetEventSystem()?.Dispatch(HPEvent.DAMAGE_RECEIVED, _stats.Damage);
    }

    private EventSystem GetEventSystem()
    {
        foreach (var item in Positions)
        {
            RaycastHit2D hit = Physics2D.Raycast(item.position, Direction,  Distance);
            Debug.DrawRay(Positions[0].position, Direction * Distance, Color.green);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out EventSystem eventSystem))
                {
                    Debug.DrawRay(Positions[0].position, Direction * Distance, Color.red);
                    return eventSystem;
                }
            }
        }

        return null;
    }
    
    
}
