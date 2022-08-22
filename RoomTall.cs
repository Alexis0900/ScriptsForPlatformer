using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTall : MonoBehaviour
{
    public TeleportDoor topLeftDoor;
    public TeleportDoor topRightDoor;
    public TeleportDoor bottomLeftDoor;
    public TeleportDoor bottomRightDoor;

    public TeleportRoom teleporter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TeleportDoor GetTopLeftDoor()
    {
        return topLeftDoor;
    }

    public TeleportDoor GetTopRightDoor()
    {
        return topRightDoor;
    }

    public TeleportDoor GetBottomLeftDoor()
    {
        return bottomLeftDoor;
    }

    public TeleportDoor GetBottomRightDoor()
    {
        return bottomRightDoor;
    }

    public TeleportRoom GetTeleporter()
    {
        return teleporter;
    }
}
