using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public LevelManager levelManager;

    public GameObject button;

    void Start()
    {
        levelManager.LoadLevel();

        if(levelManager.currentScene.Length > 5){
            button.SetActive(true);
        }
        else{
            button.SetActive(false);
        }

    }

    public void Continue()
    {
        if(levelManager.currentScene.Substring(0,5).Equals("LEVEL")){
            SceneManager.LoadScene(levelManager.currentScene);
        }

    }

    public void NewGame()
    {
        Debug.Log("NewGame");

        SceneManager.LoadScene("CharacterSelect");
    }

    public void Options()
    {

    }

    public void ExitGame()
    {
        Debug.Log("QUIT");

        Application.Quit();
    }

}
