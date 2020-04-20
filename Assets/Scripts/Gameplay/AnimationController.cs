using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationController : MonoBehaviour
{
    public List<AnimationByEvent> animationList = new List<AnimationByEvent>();

    private EventSystem _eventSystem;
    private Animator _animator;
    private string _animation;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _eventSystem = GetComponent<EventSystem>();
        _eventSystem.AddListener(EventSystem.BROADCAST, TryPlayAnimation);
    }

    private void TryPlayAnimation(EventData e)
    {
        var anim = animationList.FirstOrDefault(soundData => soundData.eventType == e.Type);
        if (anim != null)
        {
            _animation = anim.animationName;
        }
    }

    private void FixedUpdate()
    {
        if (_animation != null)
        {
            _animator.Play(_animation);
            _animation = null;
        }
    }
}

[System.Serializable]
public class AnimationByEvent
{
    public string eventType;
    public string animationName;
}