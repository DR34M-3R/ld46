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

        var eventTmp = GetEventSystem();
        Debug.LogWarning(eventTmp);
        if (eventTmp == null)
            return;
        if (eventTmp.gameObject.CompareTag("Guard"))
        {
            eventTmp.GetComponent<Stats>().hp -= _stats.Damage;
            if (eventTmp.GetComponent<Stats>().hp <= 0)
            {
                eventTmp.Dispatch(HPEvent.DIED);
                //Destroy(eventTmp.gameObject,);
            }
        }
        
        eventTmp.Dispatch(HPEvent.DAMAGE_RECEIVED, _stats.Damage);
        Debug.DrawRay(Positions[0].position, new Vector2(transform.localScale.x, 0)  * Distance, Color.red, 0.3f);
    }

    private EventSystem GetEventSystem()
    {
        foreach (var item in Positions)
        {
            RaycastHit2D hit = Physics2D.Raycast(item.position, new Vector2(transform.localScale.x, 0),  Distance);
            Debug.DrawRay(Positions[0].position, new Vector2(transform.localScale.x, 0) * Distance, Color.green, 0.2f);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out EventSystem eventSystem))
                {
                    Debug.DrawRay(Positions[0].position, new Vector2(transform.localScale.x, 0)  * Distance, Color.red, 0.3f);
                    return eventSystem;
                }
            }
        }

        return null;
    }
    
    
}
