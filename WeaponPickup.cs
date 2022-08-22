using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{   
    public Weapon weapon;
    public GameObject icon;
    private GameObject player;
    private bool inside;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        icon.GetComponent<UIIcon>().SetSprite(weapon.weaponSprite);
        icon.GetComponent<UIIcon>().SetText(weapon.weaponName);
    }

    // Update is called once per frame
    void Update()
    {
        if(inside == true && Input.GetKeyDown("e")==true){
            Debug.Log("Pick up weapon");
            // call player switch weapon

            player.GetComponent<PlayerCombatController>().AddWeapon(weapon);
            
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            inside = true;
            icon.SetActive(true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            inside = false;
            icon.SetActive(false);
        }
    }



}
