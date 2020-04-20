using System;
using core;
using Events;
using UnityEngine;

namespace Gameplay
{
    public class HPController : MonoBehaviour
    {
        private float _currentHP;
        private Stats _stats;

        private EventSystem _eventSystem;

        void Start()
        {
            _stats = GetComponent<Stats>();
            _eventSystem = GetComponent<EventSystem>();

            _eventSystem.AddListener(HPEvent.DAMAGE_RECEIVED, OnDamageReceived);
            _eventSystem.AddListener(HPEvent.HEALED, OnHealed);
            _currentHP = _stats.hp;
        }

        void OnHealed(EventData e)
        {
            float hpCount = Convert.ToSingle(e.Data);
            _currentHP += hpCount;
            
            if (_currentHP > _stats.hp)
            {
                _currentHP = _stats.hp;
            }
            _eventSystem.Dispatch(HPEvent.CHANGED, _currentHP);

        }

        void OnDamageReceived(EventData e)
        {
                Debug.LogWarning(gameObject.name + "Damaged");
            if (_currentHP > 0)
            {
                float hpCount = Convert.ToSingle(e.Data);
                _currentHP -= hpCount;
            
                if (_currentHP <= 0)
                {
                    _currentHP = 0;
                    _eventSystem.Dispatch(HPEvent.DIED);
                }
                _eventSystem.Dispatch(HPEvent.CHANGED, _currentHP);
            }
        }


    }
}