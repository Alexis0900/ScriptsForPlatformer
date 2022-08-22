using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : MonoBehaviour
{
    public RoomSmall[] _1_Rooms;
    public RoomSmall[] _2_Rooms;
    public RoomSmall[] _3_Rooms;

    // Start is called before the first frame update
    void Start()
    {
        RoomSmall leftRoom = _1_Rooms[Random.Range(0,_1_Rooms.Length)];

        RoomSmall middleRoom = _2_Rooms[Random.Range(0,_2_Rooms.Length)];

        RoomSmall rightRoom = _3_Rooms[Random.Range(0,_3_Rooms.Length)];

        

        //Vector3 position = new Vector3(0f,0f,0f);

        RoomSmall lRoom = (RoomSmall) Instantiate(leftRoom, new Vector3(0f,0f,0f), Quaternion.Euler(0,0,0));

        RoomSmall rRoom = (RoomSmall) Instantiate(middleRoom, new Vector3(100f,0f,0f), Quaternion.Euler(0,0,0));

        RoomSmall newRoom = (RoomSmall) Instantiate(rightRoom, new Vector3(200f,0f,0f), Quaternion.Euler(0,0,0));


        TeleportDoor lDoor;
        TeleportDoor rDoor;

        lDoor = lRoom.GetRightDoor();

        rDoor = rRoom.GetLeftDoor();

        lDoor.SetDestination(rDoor);

        rDoor.SetDestination(lDoor);
    }

    void ConnectTwoRooms()
    {

    }

    void GenerateDungeonObjects()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
