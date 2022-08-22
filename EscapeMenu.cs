using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
     public GameObject menu;

    private bool isActivated;

    void Start()
    {
        isActivated = false;
        HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
           if(isActivated == false){
               isActivated = true;
               ShowMenu();
           }
           else{
               isActivated = false;
               HideMenu();
           }
        }
        
       
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void MainMenu()
    {
        Debug.Log("NewGame");

        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Debug.Log("QUIT");

        Application.Quit();
    }

}
