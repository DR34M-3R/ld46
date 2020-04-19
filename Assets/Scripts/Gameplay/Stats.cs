using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class Stats : MonoBehaviour
    {
        public int hp = 100;
        
        [Range(0f, 1f)]
        public float movementSmoothing = 0.1f;        
        
        [Range(0f, 100f)]
        public float movementSpeed = 100f;
        
        [Range(100f, 700f)]
        public float jumpForce = 100f;
    }
}