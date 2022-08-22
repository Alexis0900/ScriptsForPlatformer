using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRoom : MonoBehaviour
{
    public RoomController roomSpawner;
   
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            roomSpawner.ActivateRoom();
        }
    }

}
