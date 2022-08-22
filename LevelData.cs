using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    
    public string currentScene;
    public int playerIndex;
    // int array


    public LevelData (LevelManager level)
    {
        currentScene = level.currentScene;

        playerIndex = level.playerIndex;
    }

}
