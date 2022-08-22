using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbilityController : MonoBehaviour
{
    
    public Sprite abilitySprite;
    public string abilityName;
    public int abilityCooldown = 5;

    abstract public void Ability();

}
