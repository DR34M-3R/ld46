using System.Collections;
using System.Collections.Generic;
using core;
using Events;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class EventSystem : MonoBehaviour
{
    public const string BROADCAST = "_broadcast";
    
    Dictionary<string, List<HandlerData>> _listeners = new Dictionary<string, List<HandlerData>>();

    public void AddListener(string eventName, HandlerFunction handler, bool once = false)
    {
        if (!_listeners.ContainsKey(eventName))
        {
            _listeners[eventName] = new List<HandlerData>();
        }

        _listeners[eventName].Add(new HandlerData(handler, once));
    }

    public void RemoveListener(string eventName, HandlerFunction handler)
    {
        if (_listeners.ContainsKey(eventName))
        {
            foreach (var listener in _listeners[eventName])
            {
                if (listener.Handler.Equals(handler))
                {
                    _listeners[eventName].Remove(listener);
                }
            }
        }
    }
    
    public void Dispatch(string eventType, object eventData = null)
    {
        var e = new EventData(eventType, eventData);
        DispatchEvent(eventType, e);
        DispatchEvent(BROADCAST, e);
    }

    private void DispatchEvent(string eventType, EventData eventData)
    {
        if (_listeners.ContainsKey(eventType))
        {
            foreach (var listener in _listeners[eventType])
            {
                listener.Handler(eventData);
                
                if (listener.Once)
                {
                    _listeners[eventType].Remove(listener);
                }
            }
        }
    }
}