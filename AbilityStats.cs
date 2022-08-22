using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStats : AbilityController
{
    
    public GameObject player;

    void Start()
    {
    }

    
    public override void Ability()
    {   
        PlayerCombatController playerController = player.GetComponent<PlayerCombatController>();

        if(playerController.GetGold() > 30 && playerController.currentMana > 100){
            
            playerController.SpendGold(30);
            playerController.SpendMana(100);

            playerController.GainBonusCrit(10);
            playerController.GainBonusAttackSpeed(0.2f);
        }
    }

}
