using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class EventSystemReceiverTest : MonoBehaviour
{
    private EventSystem _eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
        _eventSystem.AddListener("ping", OnPing);
    }

    void OnPing(EventData e)
    {
        Debug.Log(e);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
