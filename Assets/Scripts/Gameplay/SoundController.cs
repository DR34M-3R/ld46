using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public List<SoundByEvent> SoundList = new List<SoundByEvent>();

    private AudioSource _audioSource;
    private SoundManager _soundManager;
    private EventSystem _eventSystem;

    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        if (GetComponent<AudioSource>() == null) //check
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        _eventSystem = GetComponent<EventSystem>();
        _eventSystem.AddListener(EventSystem.BROADCAST, TryPlaySound);
    }

    private void TryPlaySound(EventData e)
    {
        var sound = SoundList.FirstOrDefault(soundData => soundData.EventType == e.Type);

        if (sound != null)
        {
            var clip = _soundManager.GetSound(sound.SoundName);
            _audioSource.clip = clip;
            _audioSource.PlayOneShot(clip);
        }
    }
}