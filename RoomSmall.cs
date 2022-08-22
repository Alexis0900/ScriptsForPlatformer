using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSmall : MonoBehaviour
{
    public TeleportDoor leftDoor;
    public TeleportDoor rightDoor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TeleportDoor GetLeftDoor()
    {
        return leftDoor;
    }

    public TeleportDoor GetRightDoor()
    {
        return rightDoor;
    }

}
