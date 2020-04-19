using UnityEngine;

namespace Gameplay
{
    public class Stats : MonoBehaviour
    {
        public int hp = 100;
        
        public int Damage = 100;

        public bool isSuck;
        
        [Range(0f, 1f)]
        public float movementSmoothing = 0.1f;        
        
        [Range(0f, 100f)]
        public float movementSpeed = 100f;
        
        [Range(0, 1000F)]
        public float jumpForce = 100f;
    }
}