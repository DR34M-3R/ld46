using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<Sound> Sounds = new List<Sound>();
    
    [SerializeField]
    public List<RandomSounds> RandomSounds = new List<RandomSounds>();

    public AudioClip GetSound(string key)
    {
       var result =  Sounds.FirstOrDefault(sound => sound.Name == key);

       return result != null ? result.Clip : GetRandomSound(key);

    }


    private AudioClip GetRandomSound(string key)
    {
        var result = RandomSounds.FirstOrDefault(sound => sound.Name == key);

        return result?.Sounds[Random.Range(0, result.Sounds.Count)].Clip;
    }
}
