using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHealthPotion : AbilityController
{   

    public GameObject player;

    void Start()
    {
        
    }

    
    public override void Ability()
    {
        player.GetComponent<PlayerCombatController>().HealPlayer(300);
    }

}

