using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //basics for physics
    public float playerSpeed;
    public float jumpForce;
    public Rigidbody2D playerRigidbody;
    public Boolean jumpFlag = true;

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

        //user presses jump -> move player up
        if (Input.GetButtonDown("Jump"))
        {
            //no double jumps? I could add a hardcoded jump check by multiplying jumpForce by jumpCheck here.
            movementVector.y = jumpForce;
            jumpFlag = false;
        }
        //user presses left/right -> move player left/right
        if (0 != Input.GetAxis("Horizontal"))
        {
            movementVector.x = Input.GetAxis("Horizontal") * playerSpeed;
        }

        return movementVector;
    }
}
