using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    //basics for physics
    public float playerSpeed = 10;
    public float jumpForce = 20;
    public float reboundForceStrength = 20;
    public Rigidbody2D playerRigidbody;
    public GameObject playerDamageTrigger;
    
    public bool jumpFlag = true;
    public Transform groundCheckPoint;
    public LayerMask[] groundLayers;

    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;
    
    public float knockBackLength, knockBackForce, impactLength;
    private float knockBackCounter, impactCounter;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.Instance.isPaused)
        {
            return;
        }
        //create a coroutine for knockback
        if (knockBackCounter >= 0 || impactCounter >= 0)
        {
            playerDamageTrigger.SetActive(false);
            knockBackCounter -= Time.deltaTime;
            impactCounter -= Time.deltaTime;
            return;
        }
        Vector2 velocity = BasePlayerMovement();
        playerDamageTrigger.SetActive(true);


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

        LayerMask layersOfGroundBehaviour = 0;

        foreach (LayerMask layer in groundLayers)
        {
            layersOfGroundBehaviour |= layer;
        }
         
        jumpFlag = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, layersOfGroundBehaviour);

        //user presses jump -> move player up
        if(Input.GetButtonDown("Jump") && jumpFlag)
        {
            //no double jumps? I could add a hardcoded jump check by multiplying jumpForce by jumpCheck here.
            movementVector.y = jumpForce;
            jumpFlag = false;
            AudioManager.Instance.PlaySFX(10);
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

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        impactCounter = impactLength;
        float flingOppositeToHazard = playerSpriteRenderer.flipX ? knockBackForce : -knockBackForce;
        
        playerRigidbody.velocity = new Vector2(flingOppositeToHazard, knockBackForce);
        playerAnimator.SetTrigger("PlayerHurt");


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

    public void reboundForce(Vector2 direction)
    {
        playerRigidbody.velocity = new Vector2(direction.x * reboundForceStrength, direction.y * reboundForceStrength);
    }
}
