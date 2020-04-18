using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController_AI : MonoBehaviour
{
    void takeDamage()
    {
        
        
        Debug.Log("TODO damage");
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            takeDamage();
        }
    }
}