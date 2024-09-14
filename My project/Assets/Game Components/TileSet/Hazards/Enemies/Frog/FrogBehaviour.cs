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
    public LayerMask groundLayer;
    // public GameObject triggerBoxCollider;
    public GameObject physicsBoxCollider;

    private BoxCollider2D frogBoxCollider2D;
    private RaycastHit2D verticalRaycast, horizontalRaycast;
    private SpriteRenderer frogSpriteRenderer;
    private Rigidbody2D frogRigidbody2D;
    private float timerMovementRest;
    private float timerRayCasts;
    private Boolean isResting;


    // Start is called before the first frame update
    void Start()
    {
        frogBoxCollider2D = physicsBoxCollider.GetComponent<BoxCollider2D>();
        frogSpriteRenderer = GetComponent<SpriteRenderer>();
        frogRigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(frogMovement());

    }

    // Update is called once per frame
    void Update()
    {
        timerRayCasts += Time.deltaTime;
        if (timerRayCasts >= 1f)
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

    }

    private IEnumerator checkForWallsAndFloorGaps()
    {
        Bounds bounds = frogBoxCollider2D.bounds;

        Vector2 bottomLeft = bounds.min;
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        Vector2 middleLeft = new Vector2(bounds.min.x, bounds.center.y);
        Vector2 middleRight = new Vector2(bounds.max.x, bounds.center.y);


        Vector2 originHorizontal = frogSpriteRenderer.flipX ? middleRight : middleLeft;
        Vector2 directionHorizontal = frogSpriteRenderer.flipX ? new Vector2(1f, 0f) : new Vector2(-1f, 0f);

        Vector2 originVertical = frogSpriteRenderer.flipX ? bottomRight : bottomLeft;
        Vector2 directionVertical = new Vector2(0f, -1f);

        horizontalRaycast = Physics2D.Raycast(originHorizontal, directionHorizontal, 1f, groundLayer);
        verticalRaycast = Physics2D.Raycast(originVertical, directionVertical, 1f, groundLayer);

        if (horizontalRaycast.collider != null || verticalRaycast.collider == null)
        {
            frogSpriteRenderer.flipX = !frogSpriteRenderer.flipX;
        }

        yield return null;
    }

    private IEnumerator frogMovement()
    {
        // Apply burst speed
        frogRigidbody2D.velocity = frogSpriteRenderer.flipX
            ? new Vector2(frogSpeed, frogSpeed)
            : new Vector2(frogSpeed * -1, frogSpeed);

        // Wait until the frog has stopped moving
        while (Mathf.Abs(frogRigidbody2D.velocity.x) > 0.1f) // Use a small threshold to determine when to stop
        {
            yield return null; // Wait until the next frame
        }

        // Stop the frog (ensure the velocity is exactly zero)
        frogRigidbody2D.velocity = new Vector2(0, frogRigidbody2D.velocity.y);

        // Start the rest period
        isResting = true;
    }
}
