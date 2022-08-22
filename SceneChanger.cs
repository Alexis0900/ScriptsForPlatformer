using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    private GameObject player;
    private GameObject levelManager;
    public GameObject text;
    

    public string nextScene;


    private GameObject fadeGameObj;
    private FadeAnimator fadeAnimator;



    private bool inside;


    void Start()
    {
        inside = false;

        player = GameObject.FindWithTag("Player");

        levelManager = GameObject.FindWithTag("Level Manager");

        fadeGameObj = GameObject.FindWithTag("FadeAnimator");
        fadeAnimator = fadeGameObj.GetComponent<FadeAnimator>();

        if(fadeAnimator != null){
            //Debug.Log("Great Success");
        }
        else{
            Debug.Log("No fade animator");
        }

    }

    void Update()
    {
        if(inside == true)
        {
            if(Input.GetKeyDown("e")){
                Debug.Log("Activate Scene Changer");
                
                fadeAnimator.Fade_Out();
                Debug.Log("Changing to scene " + nextScene);

                player.GetComponent<PlayerCombatController>().SavePlayer();
                levelManager.GetComponent<LevelManager>().SaveLevel();


                SceneManager.LoadScene(nextScene);
            }
            
        }

    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            text.SetActive(true);
            inside = true;
            //Debug.Log("player entered");
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            text.SetActive(false);
            inside = false;
            //Debug.Log("player left");
        }
    }


    public void SetNextScene(string next)
    {
        nextScene = next;
    }

}
