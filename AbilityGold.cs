using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGold : AbilityController
{
    public GameObject player;

    void Start()
    {
        
    }

    
    public override void Ability()
    {   
        PlayerCombatController playerController = player.GetComponent<PlayerCombatController>();

        if(playerController.currentHealth > 50){
            playerController.TakeDamage(50);
            playerController.GainGold(50);
        }
    }
}
