using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    // ----------------------------------------------
    // Variable Parameters

    private float gravity = 5f;
    private float jumpSpeed = 1.6f;


    public float moveSpeed = 10f;
    public float maxJumps = 2;

    // ----------------------------------------------
    // Objects we need to reference

    private CharacterController myController;
    public Animator animator;
    //public PlayerAnimationController animationChild;

    // ----------------------------------------------
    // Variables used in computing

    private float height;
    private float remainingJumps;

    private Vector3 moveInput;

    // ----------------------------------------------
    // Animation States

    private string currentState;

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Running";
    const string PLAYER_JUMP = "Jumping";
    
    
    const string PLAYER_LAND = "";
    const string PLAYER_ATTACK = "";

    // ----------------------------------------------
    // Animation Booleans

    private bool isAirborne;


    // ----------------------------------------------
    // Methods

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<CharacterController>();    
        //animator = animationChild.GetAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 

        Vector3 direction = new Vector3(horizontalInput, 0, 0);

        // reset jump count
        if(myController.isGrounded){
            remainingJumps = maxJumps;
            isAirborne = false;
        }

        // jumping
        if(myController.isGrounded || remainingJumps > 0){

            if(Input.GetButtonDown("Jump") || Input.GetKeyDown("w")){
                height = jumpSpeed;
                remainingJumps -= 1;
                isAirborne = true;

                ChangeAnimationState(PLAYER_JUMP);
            }
        }

        // look in the direction we move
        if(horizontalInput > 0){
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(horizontalInput < 0){
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
       

        if(!isAirborne){
            if(horizontalInput != 0){
                ChangeAnimationState(PLAYER_RUN);
            }
            else{
                ChangeAnimationState(PLAYER_IDLE);
            }
    
        }

        height -= gravity * Time.deltaTime;

        direction.y = height;

        myController.Move(direction * Time.deltaTime * moveSpeed);

    }

    // for movement and physics??
    void FixedUpdate()
    {

    }

    void ChangeAnimationState(string newState)
    {
        // dont self interrupt
        if(currentState == newState)
            return;

        //play the animation
        animator.Play(newState);

        //save new state
        currentState = newState;
    }

    

}
