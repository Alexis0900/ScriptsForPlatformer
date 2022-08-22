using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRoom : MonoBehaviour
{
    private GameObject player;
    public GameObject text;
    public GameObject activeStone;


    private GameObject fadeGameObj;
    private FadeAnimator fadeAnimator;

    private GameObject mapObj;
    private BigMap map;



    private bool inside;
    private bool isActivated;


    void Start()
    {
        //Debug.Log("works");
        inside = false;
        isActivated = false;


        player = GameObject.FindWithTag("Player");

        fadeGameObj = GameObject.FindWithTag("FadeAnimator");
        fadeAnimator = fadeGameObj.GetComponent<FadeAnimator>();

        mapObj = GameObject.FindWithTag("BigMap");
        map = mapObj.GetComponent<BigMap>();

        if(fadeAnimator != null){
            //Debug.Log("Great Success");
        }
        else{
            Debug.Log("no fade animator");
        }

        if(fadeAnimator != null){
            //Debug.Log("Great Success");
        }
        else{
            Debug.Log("no big map");
        }

    }

    void Update()
    {
        if(inside == true)
        {
            if(Input.GetKeyDown("e")){
                if(isActivated == false){
                    isActivated = true;
                    map.ShowMap();
                }
                else{
                    isActivated = false;
                    map.HideMap();
                }
            }
        }
        

    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            text.SetActive(true);
            inside = true;
            activeStone.SetActive(true);
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


    IEnumerator DoTransition()
    {
        fadeAnimator.Fade_Out();
        yield return new WaitForSecondsRealtime(0.4f);

        player.transform.position = transform.position;
        map.HideMap();

        yield return new WaitForSecondsRealtime(0.1f);
        fadeAnimator.Fade_In();
    }

    public void TeleportHere()
    {
        StartCoroutine(DoTransition());
    }
    
}
