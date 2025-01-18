using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrogBehaviour : MonoBehaviour
{
    public float frogSpeed;
    public float frogRestTime;
    public float frogCheckPathTime;
    public float checkDistance;
    public LayerMask groundLayers;
    public GameObject triggerBoxCollider;
    public GameObject physicsBoxCollider;
    public GameObject spriteRenderer;
    public Transform groundCheckPoint;
    [Range(0f, 100f)]public float chanceForLoot;
    public GameObject droppedLoot;

    private BoxCollider2D frogBoxCollider2D;
    private BoxCollider2D frogTriggerBox2D;
    private RaycastHit2D verticalRaycast, horizontalRaycast;
    private SpriteRenderer frogSpriteRenderer;
    private Animator frogAnimator;
    private Rigidbody2D frogRigidbody2D;
    private LayerMask layersOfGroundBehaviour;
    private float timerMovementRest;
    private float timerRayCasts;
    private bool isResting;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        frogBoxCollider2D = physicsBoxCollider.GetComponent<BoxCollider2D>();
        frogTriggerBox2D = triggerBoxCollider.GetComponent<BoxCollider2D>();
        frogSpriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        frogRigidbody2D = GetComponent<Rigidbody2D>();
        frogAnimator = GetComponent<Animator>();
        StartCoroutine(frogMovement());

    }

    void Update()
    {
        timerRayCasts += Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, groundLayers);
        if (frogSpriteRenderer.flipX)
        {
            frogTriggerBox2D.offset = new Vector2(0.1f, frogTriggerBox2D.offset.y);
        }
        else
        {
            frogTriggerBox2D.offset = new Vector2(0f, frogTriggerBox2D.offset.y);
        }
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

        horizontalRaycast = Physics2D.Raycast(originHorizontal, directionHorizontal, checkDistance, groundLayers);
        verticalRaycast = Physics2D.Raycast(originVertical, directionVertical, checkDistance, groundLayers);
        
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

    private void OnDisable()
    {
        AudioManager.Instance.PlaySFX(3);
        if (chanceForLoot >= Random.Range(0f, 100f))
        {
            Instantiate(droppedLoot, transform.position, transform.rotation);
        }

    }
}
