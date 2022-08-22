using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{

    // room arrays
	public RoomTall[] Spawns;
    public RoomTall[] _1_Rooms;
    public RoomTall[] _2_Rooms;
    public RoomTall[] _3_Rooms;

    public RoomTall[] _5_Rooms;
    public RoomTall[] _6_Rooms;
    public RoomTall[] _7_Rooms;
    public RoomTall[] _8_Rooms;
    public RoomTall[] _9_Rooms;
    public RoomTall[] _10_Rooms;
    public RoomTall[] _11_Rooms;

    public RoomTall[] _12_Rooms;
    public RoomTall[] _13_Rooms;



    // used for the teleport interface
    public GameObject[] InterfaceMapButtons;
    public GameObject InterfaceMapColumn;
    


    // making the 2d array
    private int[,] codeMap;
    private List<int[]> openDoors;
    private int roomCount;
    private bool insertFirst = false;

    private int baseRoomNumber = 17;
    public int extraRoomNumber = 0;
    
    public int exits = 1;
    private int exitIndex = 0;
    public string[] nextScenes;
    
    // making the 3d one with objects
    private RoomTall[,] objectRoomMap;

    private GameObject [,] maptime; // ????????????????????

    
    private GameObject bigMap;




    // Start is called before the first frame update
    void Start()
    {
        codeMap = new int[10,20];

        objectRoomMap = new RoomTall[10,20];

        openDoors = new List<int[]>();

        roomCount = 0;

        GenerateLevel();

        bigMap = GameObject.FindWithTag("BigMap");
        bigMap.GetComponent<BigMap>().HideMap();



    }


    public void GenerateLevel()
    {
        clearMap();
        
        createDungeonArray();

        GenerateDungeonObjects();

        GenerateBigMap();

        printMap();   
    }

    void clearMap()
    {
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 20; j++){
                codeMap[i,j] = 0;
            }
        }
    }

    void printMap()
    {
        string row = "";

        for(int i = 0; i < 10; i++){

            row += "\n";

            for(int j = 0; j < 20; j++){
                row += codeMap[i,j].ToString() + " ";
            }

        }
            
        Debug.Log(row);
    }

    void createDungeonArray()
    {
        int spawnX = Random.Range(2,5);
        int spawnY = Random.Range(3,7);

        codeMap[spawnX,spawnY] = 41;
        codeMap[spawnX + 1,spawnY] = 42;

        openDoors.Insert(0, new int[2] {spawnX, spawnY-1});
        openDoors.Insert(0, new int[2] {spawnX, spawnY+1});
        openDoors.Insert(0, new int[2] {spawnX+1, spawnY-1});
        openDoors.Insert(0, new int[2] {spawnX+1, spawnY+1});

        // make a nice diagonal
        insertFirst = true;
        for(int i=0; i<7; i++){
            addRoom(openDoors[0][0], openDoors[0][1], 0);
        }


        insertFirst = false;
        while(roomCount < baseRoomNumber-7 + extraRoomNumber && openDoors.Count > 0){
            
            int index = Random.Range(0,openDoors.Count);
            addRoom(openDoors[index][0], openDoors[index][1], index);
        }

        addExits();

        closeAllDoors();
    }

    void addRoom(int x, int y, int index)
    {

        openDoors.RemoveAt(index);

        if(codeMap[x,y]!=0){
            Debug.Log("Houston, we have a problem " + codeMap[x,y]);
            return;
        }

        roomCount++;
        
        int roomType = 0;

        // spawn new room at x,y

        // need % and switches in a bit

        // remember to check for out of bounds pls
        if(y==0){
            codeMap[x,y] = 1;
            roomType = 1;
        }
        else if(y==19){
            codeMap[x,y] = 3;
            roomType = 3;
        }
        else if(x<=1){
            codeMap[x,y] = 2;
            roomType = 2;
        }
        else if(x>=8){
            codeMap[x,y] = 2;
            roomType = 2;
        }
        // no adjacent tall rooms
        else if(codeMap[x,y-1]>3 || codeMap[x,y+1]>3 ||
                codeMap[x+1,y-1]>3 || codeMap[x+1,y+1]>3 ||
                codeMap[x-1,y-1]>3 || codeMap[x-1,y+1]>3){

                roomType = 2;
        }
        // no more than 2 small rooms in a row
        else if((codeMap[x,y-1] == 2 && codeMap[x,y-2] == 2) ||
                (codeMap[x,y+1] == 2 && codeMap[x,y+2] == 2)){

                //roomType = 5;

                roomType = Random.Range(5,12);

        }
            else{
                // 65% chance for a small room
                int aux = Random.Range(1,100);
                if(aux <=50){
                    roomType = 2;
                    
                }
                else if(aux > 50){
                    
                    if(insertFirst == true){
                        roomType = 5;
                    }
                    else{
                        roomType = Random.Range(5,12);
                    }

                    
                }
            }
        

        // else we add the doors to our list
        
        

        // failsafe - if we can't fit a tall room, we make it into a 2
        switch(roomType)
        {
            case 2:
                    makeSmallRoom(x,y);

                break;
            
            case 5:
                    // check if we can connect to the neighbours
                    // going down
                    if(HasRightWall(x,y-1) == false && HasLeftWall(x,y+1) == false &&
                       HasRightWall(x+1,y-1) == false && HasLeftWall(x+1,y+1) == false &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] = 51;
                        codeMap[x+1,y] = 52;

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if(HasRightWall(x-1,y-1) == false && HasLeftWall(x-1,y+1) == false &&
                            HasRightWall(x,y-1) == false && HasLeftWall(x,y+1) == false &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 51;
                        codeMap[x,y] = 52;

                        if(codeMap[x-1,y-1] == 0){
                            insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }

                break;



            case 6:
                    // check if we can connect to the neighbours
                    // going down
                    if((HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0) && HasLeftWall(x,y+1) == false &&
                       HasRightWall(x+1,y-1) == false && HasLeftWall(x+1,y+1) == false  &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] = 61;
                        codeMap[x+1,y] = 62;

                        if(codeMap[x,y-1] == 0){
                            //insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if((HasRightWall(x-1,y-1) == true || codeMap[x-1,y-1] == 0) && HasLeftWall(x-1,y+1) == false &&
                            HasRightWall(x,y-1) == false && HasLeftWall(x,y+1) == false &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 61;
                        codeMap[x,y] = 62;

                        if(codeMap[x-1,y-1] == 0){
                            //insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }

                break;



            case 7:
                    // check if we can connect to the neighbours
                    // going down
                    if(HasRightWall(x,y-1) == false && HasLeftWall(x,y+1) == false &&
                       HasRightWall(x+1,y-1) == false && (HasLeftWall(x+1,y+1) == true || codeMap[x+1,y+1] == 0)  &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] = 71;
                        codeMap[x+1,y] = 72;

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            //insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if(HasRightWall(x-1,y-1) == false && HasLeftWall(x-1,y+1) == false &&
                            HasRightWall(x,y-1) == false && (HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0) &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 71;
                        codeMap[x,y] = 72;

                        if(codeMap[x-1,y-1] == 0){
                            insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            //insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }


                break;


            
            case 8:
                    // check if we can connect to the neighbours
                    // going down
                    if(HasRightWall(x,y-1) == false && (HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0) &&
                       (HasRightWall(x+1,y-1) == true || codeMap[x+1,y-1] == 0) && HasLeftWall(x+1,y+1) == false  &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] = 81;
                        codeMap[x+1,y] = 82;

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            //insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            //insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if(HasRightWall(x-1,y-1) == false && (HasLeftWall(x-1,y+1) == true || codeMap[x-1,y+1] == 0) &&
                            (HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0) && HasLeftWall(x,y+1) == false &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 81;
                        codeMap[x,y] = 82;

                        if(codeMap[x-1,y-1] == 0){
                            insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            //insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            //insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }
                    

                break;

            

            case 9:
                    // check if we can connect to the neighbours
                    // going down
                    if((HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0) && HasLeftWall(x,y+1) == false &&
                       HasRightWall(x+1,y-1) == false && (HasLeftWall(x+1,y+1) == true || codeMap[x+1,y+1] == 0)  &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] = 91;
                        codeMap[x+1,y] = 92;

                        if(codeMap[x,y-1] == 0){
                            //insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            //insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if((HasRightWall(x-1,y-1) == true || codeMap[x-1,y-1] == 0) && HasLeftWall(x-1,y+1) == false &&
                            HasRightWall(x,y-1) == false && (HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0) &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 91;
                        codeMap[x,y] = 92;

                        if(codeMap[x-1,y-1] == 0){
                            //insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            //insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }
                    

                break;



            case 10:
                    // check if we can connect to the neighbours
                    // going down
                    if(HasRightWall(x,y-1) == false && (HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0) &&
                       HasRightWall(x+1,y-1) == false && (HasLeftWall(x+1,y+1) == true || codeMap[x+1,y+1] == 0)  &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] =101;
                        codeMap[x+1,y] = 102;

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            //insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            //insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if(HasRightWall(x-1,y-1) == false && (HasLeftWall(x-1,y+1) == true || codeMap[x-1,y+1] == 0) &&
                            HasRightWall(x,y-1) == false && (HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0) &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 101;
                        codeMap[x,y] = 102;

                        if(codeMap[x-1,y-1] == 0){
                            insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            //insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            //insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }


                break;

            

            case 11:
                    // check if we can connect to the neighbours
                    // going down
                    if((HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0) && HasLeftWall(x,y+1) == false &&
                       (HasRightWall(x+1,y-1) == true || codeMap[x+1,y-1] == 0) && HasLeftWall(x+1,y+1) == false  &&
                       codeMap[x,y] == 0 && codeMap[x+1,y] == 0){

                        codeMap[x,y] = 111;
                        codeMap[x+1,y] = 112;

                        if(codeMap[x,y-1] == 0){
                            //insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }

                        if(codeMap[x+1,y-1] == 0){
                            //insertNewDoor(x+1,y-1);
                        }

                        if(codeMap[x+1,y+1] == 0){
                            insertNewDoor(x+1,y+1);
                        }

                    }
                    // going up
                    else if((HasRightWall(x-1,y-1) == true || codeMap[x-1,y-1] == 0) && HasLeftWall(x-1,y+1) == false &&
                            (HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0) && HasLeftWall(x,y+1) == false &&
                            codeMap[x-1,y] == 0 && codeMap[x,y] == 0){

                        codeMap[x-1,y] = 111;
                        codeMap[x,y] = 112;

                        if(codeMap[x-1,y-1] == 0){
                            //insertNewDoor(x-1,y-1);
                        }

                        if(codeMap[x-1,y+1] == 0){
                            insertNewDoor(x-1,y+1);
                        }

                        if(codeMap[x,y-1] == 0){
                            //insertNewDoor(x,y-1);
                        }

                        if(codeMap[x,y+1] == 0){
                            insertNewDoor(x,y+1);
                        }
                    }
                    // unlucky but we close the room
                    else {
                        makeSmallRoom(x,y);
                    }
                    

                break;
            
            
        
        }


        

        // connect doors from us to x,y and from x,y to us

    }

    bool HasRightWall (int x, int y)
    {
        int[] walls = new int[] { 3, 13, 72, 81, 92, 101, 102 };
    
        foreach (int i in walls){
            if(i.Equals(codeMap[x,y])){
                return true;
            }
        }

        return false;
    }

    bool HasLeftWall (int x, int y)
    {
        int[] walls = new int[] { 1, 12, 61, 82, 91, 111, 112 };
    
        foreach (int i in walls){
            if(i.Equals(codeMap[x,y])){
                return true;
            }
        }

        return false;
    }

    void insertNewDoor(int x, int y)
    {   
        if(insertFirst == true){
            openDoors.Insert(0, new int[2] {x, y});
        }
        else{
            openDoors.Add(new int[2] {x, y});
        }
        
    }

    void makeSmallRoom(int x, int y)
    {
        // check if we can connect to the neighbours and make a 1 or 3
        if(HasRightWall(x,y-1) == true){
            codeMap[x,y] = 1;
        }
        else if(HasLeftWall(x,y+1) == true){
            codeMap[x,y] = 3;
        }
        // if there no walls we make a 2 and add the doors
        else {
            
            codeMap[x,y] = 2;

            if(codeMap[x,y-1]==0){
                insertNewDoor(x,y-1);
            }

            if(codeMap[x,y+1]==0){
                insertNewDoor(x,y+1);
            }
        }
    }

    void closeAllDoors(){

        foreach(int[] i in openDoors){
               
            int x = i[0];
            int y = i[1];

            if(codeMap[x,y]==0){
                
                // remember to check for out of bounds pls
                if(y==0){
                    codeMap[x,y] = 1;
                }
                else if(y==19){
                    codeMap[x,y] = 3;
                }
                // check if we can connect to the neighbours and make a 1 or 3
                else if(HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0){
                    codeMap[x,y] = 1;
                }
                else if(HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0){
                    codeMap[x,y] = 3;
                }
                // if there no walls we make a 2 
                else {
                    codeMap[x,y] = 2;
                }     
            }
        }
    }

    void addExits(){

        for(int i=0; i<exits; i++){

            int index = Random.Range(0,openDoors.Count);

            int x = openDoors[index][0];
            int y = openDoors[index][1];

            if(codeMap[x,y]==0){
                
                // remember to check for out of bounds pls
                if(y==0){
                    codeMap[x,y] = 12;
                }
                else if(y==19){
                    codeMap[x,y] = 13;
                }
                // check if we can connect to the neighbours and make a 1 or 3
                else if(HasRightWall(x,y-1) == true || codeMap[x,y-1] == 0){
                    codeMap[x,y] = 12;
                }
                else if(HasLeftWall(x,y+1) == true || codeMap[x,y+1] == 0){
                    codeMap[x,y] = 13;
                }
                // if there no walls we make a 2 
                else {
                    codeMap[x,y] = 2;
                    i--;
                }     
            }

            openDoors.RemoveAt(index);
        }

    }








    RoomTall GenerateNewRoom(int x, int y)
    {

        int roomValue = codeMap[x,y];

        RoomTall[] fromArray = Spawns;

        // switch for the type of room
        switch(roomValue)
        {
            case 1:
                fromArray = _1_Rooms;
                break;
            
            case 2:
                fromArray = _2_Rooms;
                break;
            
            case 3:
                fromArray = _3_Rooms;
                break;
            
            case 41:
                fromArray = Spawns;
                break;

            case 51:
                fromArray = _5_Rooms;
                break;

            case 61:
                fromArray = _6_Rooms;
                break;

            case 71:
                fromArray = _7_Rooms;
                break;

            case 81:
                fromArray = _8_Rooms;
                break;

            case 91:
                fromArray = _9_Rooms;
                break;

            case 101:
                fromArray = _10_Rooms;
                break;

            case 111:
                fromArray = _11_Rooms;
                break;

            case 12:
                fromArray = _12_Rooms;
                break;
            
            case 13:
                fromArray = _13_Rooms;
                break;

        }

        // get random room from array
        RoomTall newRoomType = fromArray[Random.Range(0,fromArray.Length)];
        
        // instantiate and return it
        RoomTall newRoom = (RoomTall) Instantiate(newRoomType, new Vector3(100f*y, 100f*x, 0f), Quaternion.Euler(0,0,0));


        // assign exit text
        if(roomValue == 12 || roomValue == 13){
            newRoom.GetComponentInChildren<SceneChanger>().SetNextScene(nextScenes[exitIndex]);
            exitIndex++;
        }

        return newRoom;
    }

    void ConnectTwoRooms(int x, int y){

        RoomTall leftRoom = objectRoomMap[x,y];
        RoomTall rightRoom = objectRoomMap[x,y+1];

        TeleportDoor leftDoor;
        TeleportDoor rightDoor;


        if(codeMap[x,y] > 13 && codeMap[x,y]%10 == 2){
            leftDoor = leftRoom.GetBottomRightDoor();
        }
        else{
            leftDoor = leftRoom.GetTopRightDoor();
        }

        if(codeMap[x,y+1] > 13 && codeMap[x,y+1]%10 == 2){
            rightDoor = rightRoom.GetBottomLeftDoor();
        }
        else{
            rightDoor = rightRoom.GetTopLeftDoor();
        }
        

        // check for nulls
        if(leftDoor != null && rightDoor != null){
            leftDoor.SetDestination(rightDoor);
            rightDoor.SetDestination(leftDoor);
        }
     
        
    }

    void GenerateDungeonObjects()
    {
        //RoomTall leftRoom;
        //RoomTall rightRoom;

        RoomTall[] roomType = Spawns;

        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 19; j++){
                
                // see if can assign LEFT room
                if(codeMap[i,j] != 0){

                    // if we have no room object, we make instantiate one
                    if(objectRoomMap[i,j] == null){

                        objectRoomMap[i,j] = GenerateNewRoom(i, j);

                        // in case we spawn a tall room, get the value bellow it
                        if(codeMap[i,j] > 13){
                            objectRoomMap[i+1,j] = objectRoomMap[i,j];
                        }
                    }

                    //leftRoom = objectRoomMap[i,j];
                }

                // see if we can assign RIGHT room
                if(codeMap[i,j+1] != 0){

                    // if we have no room object, we make instantiate one
                    if(objectRoomMap[i,j+1] == null){

                        objectRoomMap[i,j+1] = GenerateNewRoom(i, j+1);

                        // in case we spawn a tall room, get the value bellow it
                        if(codeMap[i,j+1] > 13){
                            objectRoomMap[i+1,j+1] = objectRoomMap[i,j+1];
                        }
                    }

                    //rightRoom = objectRoomMap[i,j+1];
                }


                // we connect the doors here and bless rng
                if(codeMap[i,j] != 0 && codeMap[i,j+1] != 0){
                    ConnectTwoRooms(i,j);
                }

            }

        }
    }

    void GenerateBigMap()
    {
        GameObject bigParent = GameObject.FindWithTag("BigMapInterface");;

        GameObject columnParent;

        // reverse traverse because we need columns first
        for(int j = 0; j < 20; j++){
            
            // make a new column and fill it with goodies

            columnParent = Instantiate(InterfaceMapColumn) as GameObject;
            columnParent.transform.SetParent(bigParent.transform, false);
            columnParent.SetActive(true);
            
            for(int i = 0; i < 10; i++){
                
                GameObject newIcon;

                int iconIndex = -1;

                switch(codeMap[i,j])
                {
                    case 0:
                        iconIndex = 0;
                        break;

                    case 1:
                        iconIndex = 1;
                        break;
                    
                    case 2:
                        iconIndex = 2;
                        break;
                    
                    case 3:
                        iconIndex = 3;
                        break;
                    
                    case 41:
                        iconIndex = 4;
                        break;

                    case 51:
                        iconIndex = 5;
                        break;

                    case 61:
                        iconIndex = 6;
                        break;

                    case 71:
                        iconIndex = 7;
                        break;

                    case 81:
                        iconIndex = 8;
                        break;

                    case 91:
                        iconIndex = 9;
                        break;

                    case 101:
                        iconIndex = 10;
                        break;

                    case 111:
                        iconIndex = 11;
                        break;
                    
                    case 12:
                        iconIndex = 12;
                        break;
                    
                    case 13:
                        iconIndex = 13;
                        break;
                }

                if(iconIndex != -1){
                    newIcon = Instantiate(InterfaceMapButtons[iconIndex]) as GameObject;
                    newIcon.transform.SetParent(columnParent.transform, false);
                    newIcon.SetActive(true);


                    if(iconIndex > 3 && iconIndex <= 11){
                        TeleportRoom teleporter = objectRoomMap[i,j].GetTeleporter();

                        newIcon.GetComponent<Button>().onClick.AddListener(teleporter.TeleportHere);
                    }

                }
                
            }
        }
    }
}
