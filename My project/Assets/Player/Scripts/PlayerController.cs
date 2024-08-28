using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //basics for physics
    public float playerSpeed = 10;
    public float jumpForce = 20;
    public Rigidbody2D playerRigidbody;
        //jump restriction
    public bool jumpFlag = true;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;

    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    
    //advanced
    public int keyCount;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = BasePlayerMovement();

        //calculate velocity and at the end append the value to velocity
        playerRigidbody.velocity = velocity;

        playerAnimator.SetBool("PlayerIsGrounded",jumpFlag);
        playerAnimator.SetFloat("PlayerMovementSpeed", Mathf.Abs(playerRigidbody.velocity.x));
        playerAnimator.SetFloat("PlayerVerticalSpeed",playerRigidbody.velocity.y);

        RotatePlayerSprite();
    }

    Vector2 BasePlayerMovement()
    {
        Vector2 movementVector = playerRigidbody.velocity;

        jumpFlag = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);

        //user presses jump -> move player up
        if(Input.GetButtonDown("Jump") && jumpFlag)
        {
            //no double jumps? I could add a hardcoded jump check by multiplying jumpForce by jumpCheck here.
            movementVector.y = jumpForce;
            jumpFlag = false;
        }
        //user presses left/right -> move player left/right
        movementVector.x = Input.GetAxisRaw("Horizontal") * playerSpeed;
        

        return movementVector;
    }

    private Boolean RotatePlayerSprite()
    {
        if (playerRigidbody.velocity.x >= -0.1 && playerRigidbody.velocity.x <= 0.1)
        {
            return playerSpriteRenderer.flipX;
        }
        return playerSpriteRenderer.flipX = playerRigidbody.velocity.x < -0.1;
    }
}
