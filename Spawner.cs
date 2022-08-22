using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(0,100)]
    public int chanceToSpawn = 80;


    public void Spawn()
    {   
        int chance = Random.Range(0,101);

        if(chance <= chanceToSpawn){

            int x = Random.Range(0,transform.childCount);
            transform.GetChild(x).gameObject.SetActive(true);
        }
    }
}
