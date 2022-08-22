using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{   
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Vector3 offset = new Vector3(0, 1, 0);
        player.transform.position = transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
