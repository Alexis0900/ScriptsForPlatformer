using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    
    public int playerType;

    public int maxHealth;
    public int currentHealth;


    public int maxMana;
    public int manaRegen;


    public int gold;
    public int bonusCrit;
    public float bonusAttackSpeed;


    public string[] weaponArray;



    public PlayerData (PlayerCombatController player)
    {
        playerType = player.playerType;

        maxHealth = player.maxHealth;
        currentHealth = player.currentHealth;

        maxMana = player.maxMana;
        manaRegen = player.manaRegen;

        gold = player.gold;
        bonusCrit = player.bonusCrit;
        bonusAttackSpeed = player.bonusAttackSpeed;

        weaponArray = new string[player.weaponList.Count];
        int weaponIndex = 0;
        foreach(Weapon i in player.weaponList){

           weaponArray[weaponIndex] = i.weaponName;
           weaponIndex++;
        }
    }


}
