// This script controls the animations of the Robbie player character. Normally, most
// of this functionality would be added to the PlayerMovement script instead of having
// its own script since that would be more efficient. For the purposes of learning,
// however, this functionality was separated out.

using UnityEngine;
using System.Collections;
public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement movement;    //Reference to the PlayerMovement script component
    Rigidbody2D rigidBody;      //Reference to the Rigidbody2D component
    PlayerInput input;          //Reference to the PlayerInput script component
    Animator anim;              //Reference to the Animator component

    
    int groundParamID;          //ID of the isOnGround parameter
    int speedParamID;           //ID of the speed parameter
    int fallParamID;            //ID of the verticalVelocity parameter


    void Start()
    {
        //Get the integer hashes of the parameters. This is much more efficient
        //than passing strings into the animator
       
        groundParamID = Animator.StringToHash("isOnGround");
        
        speedParamID = Animator.StringToHash("speed");
        fallParamID = Animator.StringToHash("verticalVelocity");

        //Grab a reference to this object's parent transform
        Transform parent = transform.parent;

        //Get references to the needed components
        movement = parent.GetComponent<PlayerMovement>();
        rigidBody = parent.GetComponent<Rigidbody2D>();
        input = parent.GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();

        //If any of the needed components don't exist...
        if (movement == null || rigidBody == null || input == null || anim == null)
        {
            //...log an error and then remove this component
            Debug.LogError("A needed component is missing from the player");
            Destroy(this);
        }
        StartCoroutine(SpeedUp());
    }

    void Update()
    {
        //Update the Animator with the appropriate values
        
        anim.SetBool(groundParamID, movement.isOnGround);
        
        anim.SetFloat(fallParamID, rigidBody.velocity.y);

        //Use the absolute value of speed so that we only pass in positive numbers
        anim.SetFloat(speedParamID, Mathf.Abs(input.horizontal));
    }
    IEnumerator SpeedUp()
    {
        while (anim.speed < 2f)
        {
            anim.speed += 0.05f;
            yield return new WaitForSeconds(2f);
        }
    }
    
}
