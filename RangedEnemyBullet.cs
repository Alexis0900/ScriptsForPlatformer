using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBullet : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter (Collider other)
    {
        //Debug.Log("hit - " + other.gameObject.name);
        if(other.gameObject.tag == "Player"){
            
            other.gameObject.GetComponent<PlayerCombatController>().TakeDamage(damage);
            //Debug.Log("hit enemy");
        }
    }
}
