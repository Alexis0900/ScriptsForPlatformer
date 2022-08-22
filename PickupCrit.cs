using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCrit : MonoBehaviour
{
   public int crit = 5;
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerCombatController>().GainBonusCrit(crit);

            Destroy(gameObject);
        }
    }
}

