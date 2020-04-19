using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class AttackActionController : MonoBehaviour
{
    public List<Vector3> Positions = new List<Vector3>();

    [SerializeField]
    private Stats _stats;

    public void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);

        if (hit.collider != null)
        {

        }
    }


 //   private EventSystem GetCollision()
   // {
        
    //}
    
    
}
