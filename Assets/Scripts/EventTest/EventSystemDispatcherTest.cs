using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemDispatcherTest : MonoBehaviour
{
    private int _count = 0;
    private EventSystem _eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_count < 100)
        {
            _eventSystem.Dispatch("ping", _count);
            _count++;
        }
    }
}
