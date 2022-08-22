using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // stats
    public int damage;
    public int manaCost;
    public int crit;
    public float attackSpeed;
    public int projectileSpeed;

    // projectile and sprite
    public GameObject projectile;
    public Sprite weaponSprite;

    // name
    public string weaponName;

}
