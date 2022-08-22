using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    
    private GameObject player;
    public GameObject text;
    public TeleportDoor destination;


    private GameObject fadeGameObj;
    private FadeAnimator fadeAnimator;



    private bool inside;
    private bool isActivated;



    public void SetDestination (TeleportDoor dest)
    {
        destination = dest;
    }



    void Start()
    {
        //Debug.Log("works");
        inside = false;
        isActivated = false;


        player = GameObject.FindWithTag("Player");

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
                isActivated = true;
                Debug.Log("Activate Door Teleport");
                StartCoroutine(DoTransition());
            }
            
            if(Input.GetKeyUp("e")){
                isActivated = false;
            }
        }
        

    }

    void FixedUpdate()
    {
        if(isActivated == true){
            //Debug.Log("Activate Door Teleport");

            //
           // player.transform.position = destination.transform.position;

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


    IEnumerator DoTransition()
    {
        fadeAnimator.Fade_Out();
        yield return new WaitForSecondsRealtime(0.4f);

        player.transform.position = destination.transform.position;

        yield return new WaitForSecondsRealtime(0.1f);
        fadeAnimator.Fade_In();
    }

    

}
