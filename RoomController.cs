using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public void ActivateRoom()
    {   
        foreach(Transform child in transform){
            
            Debug.Log("room activated");

            if(child.GetComponent<EnableRoom>() != null){
                child.gameObject.SetActive(false);
            }
            else if(child.GetComponent<Spawner>() != null){
                child.GetComponent<Spawner>().Spawn();
            }
        }
    }
}

