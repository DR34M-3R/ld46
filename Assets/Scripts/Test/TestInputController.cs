using System;
using core;
using Events;
using UnityEngine;

namespace Test
{
    
    
    public class TestInputController : MonoBehaviour
    {
        private EventSystem _eventSystem;
        // Start is called before the first frame update
        void Start()
        {
            _eventSystem = GetComponent<EventSystem>();
        }

        private void Update()
        {
            if (Input.GetKeyUp("space")) _eventSystem.Dispatch(HPEvent.DAMAGE_RECEIVED, 50f);
            if (Input.GetKeyUp("s")) _eventSystem.Dispatch(HPEvent.HEALED, 50f);
        }
    }
}