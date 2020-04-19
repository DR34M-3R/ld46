using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class UserInputController : MonoBehaviour
{
    public List<InputParams> Keys = new List<InputParams>();
    private Dictionary<KeyCode, InputParams> KeyMap = new Dictionary<KeyCode, InputParams>();
    private EventSystem _eventSystem;
    
    void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
            
        foreach (var keyParam in Keys)
        {
            KeyMap[keyParam.KeyCode] = keyParam;
        }

        Keys = null;
    }
    
    void Update()
    {
        // Input.GetAxis("Horizontal") TODO implement it better
        foreach (var entry in KeyMap)
        {
             if (entry.Value.KeyDownEvent.Length > 0 && Input.GetKeyDown(entry.Key)) _eventSystem.Dispatch(entry.Value.KeyDownEvent);
             if (entry.Value.KeyUpEvent.Length > 0 && Input.GetKeyUp(entry.Key)) _eventSystem.Dispatch(entry.Value.KeyUpEvent);
        }
    }
}

[Serializable]
public class InputParams
{
    public KeyCode KeyCode;
    public string KeyDownEvent;
    public string KeyUpEvent;
}