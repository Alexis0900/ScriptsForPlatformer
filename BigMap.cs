using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMap : MonoBehaviour
{   
    public GameObject map;

    private bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        //HideMap();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")){
           if(isActivated == false){
               isActivated = true;
               ShowMap();
           }
           else{
               isActivated = false;
               HideMap();
           }
        }
        
       
    }

    /*
    void FixedUpdate()
    {
        if(isActivated == true){
            //Debug.Log("MapOpen");
            map.SetActive(true);
        }
        else{
            //Debug.Log("MapClosed");
            map.SetActive(false);
        }
    }
    */

    public void ShowMap()
    {
        map.SetActive(true);
    }

    public void HideMap()
    {
        map.SetActive(false);
    }


}
