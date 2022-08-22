using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{
    
    private int value;

    void Start()
    {
        int x = Random.Range(0,transform.childCount);
        transform.GetChild(x).gameObject.SetActive(true);

        switch(x)
        {
            case 0:
                    value = 1;
                break;
            
            case 1:
                    value = 5;
                break;

            case 2:
                    value = 10;
                break;

            case 3:
                    value = 5;
                break;

            case 4:
                    value = 15;
                break;

            case 5:
                    value = 30;
                break;

            default:
                    value = 1;
                break;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerCombatController>().GainGold(value);

            Destroy(gameObject);
        }
    }
}
