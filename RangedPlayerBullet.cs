using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPlayerBullet : MonoBehaviour
{
    public int damage;
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
    
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Enemy"){
            
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }


    /// maybe a self destruct
}
