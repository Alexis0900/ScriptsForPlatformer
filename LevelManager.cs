using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public bool buildsLevel = false;

    public bool firstLevel = false;

    public string currentScene;
    public int playerIndex;

    public GameObject playerContainer;
    //public DungeonGenerator generator;
    

    void Start()
    {
        if(buildsLevel == true)
        {   
            LoadLevel();

            StartLevel();
        }
    }

    

    public void StartLevel()
    {
        GameObject player = Instantiate(playerContainer.transform.GetChild(playerIndex).gameObject);
        player.SetActive(true);

        if(firstLevel == false){
            StartCoroutine(LoadPlayer(player));
        }

        //playerContainer.transform.GetChild(playerIndex).gameObject.SetActive(true);

        //generator.GenerateLevel();

        currentScene = SceneManager.GetActiveScene().name;


        StartCoroutine(Save(player));
    }

    IEnumerator LoadPlayer(GameObject player)
    {   
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerCombatController>().LoadPlayer();
    }


    IEnumerator Save(GameObject player)
    {   
        yield return new WaitForSeconds(4);
        player.GetComponent<PlayerCombatController>().SavePlayer();
        SaveLevel();
    }


    public void SaveLevel()
    {
        SaveSystem.SaveLevel(this);
    }

    public void LoadLevel()
    {
        LevelData data = SaveSystem.LoadLevel();

        currentScene = data.currentScene;

        playerIndex = data.playerIndex;
    }



}
