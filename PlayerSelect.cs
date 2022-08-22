using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    
    private GameObject[] charArray;

    private int playerIndex = 0;

    private GameObject levelManager;

    void Start()
    {

        levelManager = GameObject.FindWithTag("Level Manager");

        charArray = new GameObject[transform.childCount];

        // fill array with children
        for(int i=0; i<transform.childCount; i++){
            charArray[i] = transform.GetChild(i).gameObject;
        }

        // set them all to false
        foreach (GameObject i in charArray){
            i.SetActive(false);
        }

        charArray[0].SetActive(true);
    }

    public void NextPlayer()
    {   
        charArray[playerIndex].SetActive(false);

        playerIndex++;
        if(playerIndex > charArray.Length-1){
            playerIndex = 0;
        }

        charArray[playerIndex].SetActive(true);
    }

    public void PreviousPlayer()
    {   
        charArray[playerIndex].SetActive(false);

        playerIndex--;
        if(playerIndex < 0){
            playerIndex = charArray.Length-1;
        }

        
        charArray[playerIndex].SetActive(true);
    }

    public void PlayGame()
    {
        // save player data
        levelManager.GetComponent<LevelManager>().playerIndex = playerIndex;
        levelManager.GetComponent<LevelManager>().SaveLevel();
        

        SceneManager.LoadScene("LEVEL 1");
    }

}
