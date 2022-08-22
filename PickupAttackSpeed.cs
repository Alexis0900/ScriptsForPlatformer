using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAttackSpeed : MonoBehaviour
{
    public float attackSpeed = 0;
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerCombatController>().GainBonusAttackSpeed(attackSpeed);

            Destroy(gameObject);
        }
    }
}
