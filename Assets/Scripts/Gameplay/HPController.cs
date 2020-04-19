using System;
using core;
using Events;
using UnityEngine;

namespace Gameplay
{
    public class HPController : MonoBehaviour
    {
        public float CurrentHP;
        private Stats _stats;

        private EventSystem _eventSystem;

        void Start()
        {
            _stats = GetComponent<Stats>();
            _eventSystem = GetComponent<EventSystem>();

            _eventSystem.AddListener(HPEvent.DAMAGE_RECEIVED, OnDamageReceived);
            _eventSystem.AddListener(HPEvent.HEALED, OnHealed);
            
        }

        void OnHealed(EventData e)
        {

            float hpCount = Convert.ToSingle(e.Data);
            CurrentHP += hpCount;
            
            if (CurrentHP > _stats.hp)
            {
                CurrentHP = _stats.hp;
            }
            
            _eventSystem.Dispatch(HPEvent.CHANGED, CurrentHP);
        }

        void OnDamageReceived(EventData e)
        {
            if (CurrentHP > 0)
            {
                float hpCount = Convert.ToSingle(e.Data);
                CurrentHP -= hpCount;
            
                if (CurrentHP <= 0)
                {
                    CurrentHP = 0;
                    _eventSystem.Dispatch(HPEvent.DIED);
                }
                _eventSystem.Dispatch(HPEvent.CHANGED, CurrentHP);
            }
        }


    }
}