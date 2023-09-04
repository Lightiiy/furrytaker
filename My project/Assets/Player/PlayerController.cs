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

    //advanced
    public int keyCount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = basePlayerMovement();

        //calculate velocity and at the end append the value to velocity
        playerRigidbody.velocity = velocity;

    }

    Vector2 basePlayerMovement()
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
        movementVector.x = Input.GetAxis("Horizontal") * playerSpeed;
        

        return movementVector;
    }
}
