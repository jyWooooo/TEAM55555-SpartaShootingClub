using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBullet : MonoBehaviour
{
    int damage = 50;

    private void OnTriggerEnter(Collider collision)
    {
        TakeDamage takeDamage = collision.GetComponent<TakeDamage>();  

        if(takeDamage!= null)
        {
            takeDamage.CallDamage(damage);
        }
        //Destroy(gameObject); 
    }
}
