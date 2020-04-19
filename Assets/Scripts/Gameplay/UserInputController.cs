using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class UserInputController : MonoBehaviour
{
    public string leftMovementEvent;
    public string rightMovementEvent;
    public string stopMovementEvent;
    
    public List<InputParams> Keys = new List<InputParams>();
    private Dictionary<KeyCode, InputParams> KeyMap = new Dictionary<KeyCode, InputParams>();
    private EventSystem _eventSystem;

    private int _moveDirection;
    
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
        var oldMoveDirection = _moveDirection;
        _moveDirection = Mathf.RoundToInt(Input.GetAxis("Horizontal"));

        if (_moveDirection != oldMoveDirection)
        {
            if (oldMoveDirection != 0)
            {
                OnChangedDirection(0);
            }
            OnChangedDirection(_moveDirection);
        }
            
        foreach (var entry in KeyMap)
        {
             if (entry.Value.KeyDownEvent.Length > 0 && Input.GetKeyDown(entry.Key)) _eventSystem.Dispatch(entry.Value.KeyDownEvent);
             if (entry.Value.KeyUpEvent.Length > 0 && Input.GetKeyUp(entry.Key)) _eventSystem.Dispatch(entry.Value.KeyUpEvent);
        }
    }

    private void OnChangedDirection(int d)
    {
        switch (d)
        {
            case -1: _eventSystem.Dispatch(leftMovementEvent); break;
            case 0: _eventSystem.Dispatch(stopMovementEvent); break;
            case 1: _eventSystem.Dispatch(rightMovementEvent); break;
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
