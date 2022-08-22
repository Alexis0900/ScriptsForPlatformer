using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimator : MonoBehaviour
{
    private Animator animator; 

    private string currentState;

    const string FADE_IN = "Fade_In";
    const string FADE_OUT = "Fade_Out";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

    public void Fade_In()
    {
        ChangeAnimationState(FADE_IN);
    }

    public void Fade_Out()
    {
        ChangeAnimationState(FADE_OUT);
    }


}
