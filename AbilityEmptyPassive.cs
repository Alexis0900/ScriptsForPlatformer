using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEmptyPassive : AbilityController
{
    
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    public override void Ability()
    {
        
    }
}
