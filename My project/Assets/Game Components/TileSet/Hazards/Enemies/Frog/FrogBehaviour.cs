using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogBehaviour : MonoBehaviour
{
    public float frogSpeed;
    public float frogRestTime;
    public float frogCheckPathTime;
    public float checkDistance;
    public LayerMask groundLayer;
    // public GameObject triggerBoxCollider;
    public GameObject physicsBoxCollider;
    public GameObject spriteRenderer;
    public Transform groundCheckPoint;

    private BoxCollider2D frogBoxCollider2D;
    private RaycastHit2D verticalRaycast, horizontalRaycast;
    private SpriteRenderer frogSpriteRenderer;
    private Animator frogAnimator;
    private Rigidbody2D frogRigidbody2D;
    private float timerMovementRest;
    private float timerRayCasts;
    private Boolean isResting;
    private Boolean isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        frogBoxCollider2D = physicsBoxCollider.GetComponent<BoxCollider2D>();
        frogSpriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        frogRigidbody2D = GetComponent<Rigidbody2D>();
        frogAnimator = GetComponent<Animator>();
        StartCoroutine(frogMovement());

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayer);
        timerRayCasts += Time.deltaTime;
        if (timerRayCasts >= frogCheckPathTime)
        {
            StartCoroutine(checkForWallsAndFloorGaps());
            timerRayCasts = 0f;
        }

        if (isResting)
        {
            timerMovementRest += Time.deltaTime;
            if (timerMovementRest > frogRestTime)
            {
                isResting = false;
                timerMovementRest = 0f;
                StartCoroutine(frogMovement());
            }
        }

        frogAnimator.SetFloat("frogVerticalSpeed", frogRigidbody2D.velocity.y);
        frogAnimator.SetBool("isGrounded", isGrounded);
    }

    private IEnumerator checkForWallsAndFloorGaps()
    {
        Bounds bounds = frogBoxCollider2D.bounds;

        Vector2 bottomLeft = new Vector2(bounds.min.x - 1 , bounds.min.y);
        Vector2 bottomRight = new Vector2(bounds.max.x + 1, bounds.min.y);

        Vector2 middleLeft = new Vector2(bounds.min.x, bounds.center.y);
        Vector2 middleRight = new Vector2(bounds.max.x, bounds.center.y);


        Vector2 originHorizontal = frogSpriteRenderer.flipX ? middleRight : middleLeft;
        Vector2 directionHorizontal = frogSpriteRenderer.flipX ? new Vector2(1f, 0f) : new Vector2(-1f, 0f);

        Vector2 originVertical = frogSpriteRenderer.flipX ? bottomRight : bottomLeft;
        Vector2 directionVertical = new Vector2(0f, -1f);

        horizontalRaycast = Physics2D.Raycast(originHorizontal, directionHorizontal, checkDistance, groundLayer);
        verticalRaycast = Physics2D.Raycast(originVertical, directionVertical, checkDistance, groundLayer);
        
        if (horizontalRaycast.collider != null || verticalRaycast.collider == null)
        {
            frogRigidbody2D.velocity = new Vector2(0f, 0f);
            frogSpriteRenderer.flipX = !frogSpriteRenderer.flipX;
        }
        yield return null;
    }

    private IEnumerator frogMovement()
    {

        frogRigidbody2D.velocity = frogSpriteRenderer.flipX
            ? new Vector2(frogSpeed, frogSpeed)
            : new Vector2(frogSpeed * -1, frogSpeed);
        
        while (Mathf.Abs(frogRigidbody2D.velocity.x) > 0.1f) 
        {
            yield return null;
        }

        frogRigidbody2D.velocity = new Vector2(0, frogRigidbody2D.velocity.y);
        isResting = true;
        
        frogAnimator.SetFloat("frogVerticalSpeed", frogRigidbody2D.velocity.y);
        frogAnimator.SetBool("isGrounded", isGrounded);
    }
}
