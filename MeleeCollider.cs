using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {   
            // damage player
            //other.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage);

            other.gameObject.GetComponent<PlayerCombatController>().TakeDamage(damage);
            gameObject.SetActive(false);

            //Debug.Log("Damaged player! - " + damage);
            
        }
    }
}
