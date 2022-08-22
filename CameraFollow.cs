using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 10f;
    public Vector3 offset;

    private GameObject player;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        
        player = GameObject.FindWithTag("Player");

        target = player.transform;

        Vector3 desiredPosition = target.position + offset;

        transform.position = desiredPosition;


        // smooth movement looks weird for now

        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //transform.position = smoothedPosition;

        //transform.LookAt(target);


    }
}
