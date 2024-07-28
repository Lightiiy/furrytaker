using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //basics for physics
    public float playerSpeed;
    public float jumpForce;
    public Rigidbody2D rigidbody;

    //advanced
    public int keys;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = movePlayersOnInput();


        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }


    Vector2 movePlayersOnInput()
    {
        Vector2 movementVector = rigidbody.velocity;

        if(Input.GetAxis("Horizontal") == 0)
        {
            return movementVector;
        }
        movementVector.x = Input.GetAxis("Horizontal") * playerSpeed;
        return movementVector;
    }
}
