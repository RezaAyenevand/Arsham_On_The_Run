using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float moveVelocity;
    public float mxht;

    bool grounded;

    //This is for 2D not 3D, I am just taking a wild guess this is a 2D game
    private Rigidbody2D rigid;

    void Start()
    {

        //Get the rigidbody of character 
        rigid = GetComponent<Rigidbody2D>();

        //Set grounded to true or false (Depending if your character starts in the 
        //air
        grounded = true;
    }

    private void FixedUpdate()
    {
        if (transform.position.y > mxht)
        {
            mxht = transform.position.y;
        }
    }
    void Update()
    {


        moveVelocity = 0;

        // If the character moves right 
        if (Input.GetKeyDown(KeyCode.D))
        {

            moveVelocity = moveSpeed;

        }
        // If the character moves left;
        if (Input.GetKeyDown(KeyCode.A))
        {

            moveVelocity = -moveSpeed;

        }

        //If you press spacebar and your are on the ground then you can jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            //I am not to sure about the rigid.velocity code play around with it
            //I think in 2D you can set it to a Vector2 instead of Vector3 otherwise just 
            //change it
            rigid.velocity = new Vector2(moveVelocity, jumpHeight);

            //After your character jumps grounded has to change to false since the 
            //character is not on the ground anymore
            grounded = false;

        }

        // If your character is not falling nor jumping then change grounded true
        //Since he has landed.
        if (rigid.velocity.y == 0)
        {

            grounded = true;
            mxht = 0;
        }

    }
}
