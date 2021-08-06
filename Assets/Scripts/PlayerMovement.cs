// This script controls the player's movement and physics within the game
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool drawDebugRaycasts = true;	//Should the environment checks be visualized

    public float mxht = 0;

    [Header("Jump Properties")]
    public float jumpHeight = 5;     //jump Height
    public float jumpGravity = -10;  //Player effective gravity
    public float jumpSpeed = 1;      //Speed of Jump 

    [Header("Environment Check Properties")]
    public float footOffset = .4f;          //X Offset of feet raycast
    public float groundDistance = .1f;      //Distance player is considered to be on the ground
    public LayerMask groundLayer;           //Layer of the ground
    public int groundLayerNumber;
    [SerializeField]
    public int remainingLights;

    [Header("Status Flags")]
    public bool isOnGround;                 //Is the player on the ground?
    public bool isJumping;                  //Is player jumping?
    public bool isHanging;                  //Is player hanging?

    PlayerInput input;                      //The current inputs for the player
    BoxCollider2D bodyCollider;             //The collider component
    Rigidbody2D rigidBody;                  //The rigidbody component

    float jumpTime = 0;                         //Variable to hold jump duration
    float coyoteTime;                       //Variable to hold coyote duration
    float playerHeight;                     //Height of the player

    float originalXScale;                   //Original scale on X axis
    int direction = 1;                      //Direction player is facing
    float initialGravity = -10;             //Initial gravity
    float initialVelocity;

    Vector2 colliderStandSize;              //Size of the standing collider
    Vector2 colliderStandOffset;            //Offset of the standing collider


    void Start()
    {
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        originalXScale = transform.localScale.x;

        playerHeight = bodyCollider.size.y;

        colliderStandSize = bodyCollider.size;
        colliderStandOffset = bodyCollider.offset;

        FindObjectOfType<GameSession>().setLightScore(remainingLights);

        initialGravity = jumpGravity;
        jumpTime = Time.time;
    }


    void FixedUpdate()
    {
        PhysicsCheck();

        MidAirMovement();

        if (transform.position.y > mxht)
        {
            mxht = transform.position.y;
        }
    }


    void PhysicsCheck()
    {
        isOnGround = false;

        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, bodyCollider.offset.y - bodyCollider.size.y / 2), Vector2.down, groundDistance);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, bodyCollider.offset.y - bodyCollider.size.y / 2), Vector2.down, groundDistance);

        if (leftCheck || rightCheck)
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, groundLayerNumber, false);
            isOnGround = true;
            isJumping = false;
            if (rigidBody.velocity.y == 0)
            {
                initialVelocity = 0;
                jumpTime = Time.time;
            }
        }
        else
        {
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, groundLayerNumber, true);
        }
    }

    void MidAirMovement()
    {
        if (input.jumpPressed && !isJumping && (isOnGround || coyoteTime > Time.time))
        {
            jumpGravity = jumpSpeed * initialGravity;
            isOnGround = false;
            isJumping = true;
            mxht = 0;
            rigidBody.velocity = new Vector2(0, Mathf.Sqrt(-2f * jumpGravity * jumpHeight));
            jumpTime = Time.time;
            initialVelocity = rigidBody.velocity.y;
        }

        if (!isOnGround)
        {
            if (rigidBody.velocity.y < 0)
            {
                if (rigidBody.velocity.y <= -Mathf.Sqrt(-2f * jumpGravity * jumpHeight))
                {
                    rigidBody.velocity = new Vector2(0, -Mathf.Sqrt(-2f * jumpGravity * jumpHeight));
                }
                else
                {
                    jumpGravity = jumpSpeed * initialGravity * 1.05f;
                    rigidBody.velocity = new Vector2(0, initialVelocity + jumpGravity * (Time.time - jumpTime));
                }
            }
            else
            {
                jumpGravity = jumpSpeed * initialGravity;
                rigidBody.velocity = new Vector2(0, initialVelocity + jumpGravity * (Time.time - jumpTime));
            }
        }
    }


    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        return Raycast(offset, rayDirection, length, groundLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        if (drawDebugRaycasts)
        {
            Color color = hit ? Color.red : Color.green;
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        return hit;
    }

    public void AddRemainingLight(int n)
    {
        remainingLights += n;
    }

    public void SubRemainingLight(int n)
    {
        remainingLights -= n;
    }

}
