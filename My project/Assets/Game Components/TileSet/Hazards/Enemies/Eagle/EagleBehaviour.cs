using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBehaviour : MonoBehaviour
{

    public GameObject eagleTriggerCollider;
    public GameObject eagleAttackRange;
    public GameObject eagleSpriteRenderer;
    public float eagleAttackRadius;
    public float timeBetweenEnemyAttacks;
    public float attackSpeed;
    

    private CircleCollider2D triggerCollider;
    private Transform attackRange;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isPlayerInRange;
    private bool attackingPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = eagleTriggerCollider.GetComponent<CircleCollider2D>();
        attackRange = eagleAttackRange.transform;
        spriteRenderer = eagleSpriteRenderer.GetComponent<SpriteRenderer>();
        
        timer = timeBetweenEnemyAttacks;
        attackingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackingPlayer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                LayerMask playerMask = LayerMask.GetMask("Player");
                isPlayerInRange = Physics2D.OverlapCircle(attackRange.position, eagleAttackRadius, playerMask);

                if (isPlayerInRange)
                {
                    attackingPlayer = true;
                    isPlayerInRange = false;
                    timer = timeBetweenEnemyAttacks;
                    StartCoroutine(attackPlayer());
                }
                else
                {
                    timer = timeBetweenEnemyAttacks;
                }
            } 
        }

    }
    private IEnumerator attackPlayer()
    {
        Vector2 playerPosition = new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
        Vector2 eaglePosition = new Vector2(transform.position.x, transform.position.y);
        bool reachedPlayerPosition = false;
        bool cameBackToOrigin = false;

        while (!(reachedPlayerPosition && cameBackToOrigin))
        {
            Vector2 endGoalDirection = reachedPlayerPosition ?  eaglePosition : playerPosition;
            transform.position = Vector2.MoveTowards(transform.position, endGoalDirection, attackSpeed * Time.deltaTime);
            spriteRenderer.flipX = ((Vector2)transform.position - endGoalDirection).x <= -0.1f;

            if (!reachedPlayerPosition)
            {
                reachedPlayerPosition = Vector2.Distance(transform.position, playerPosition) < .05f;
            }
            if (!cameBackToOrigin)
            {
                cameBackToOrigin = Vector2.Distance(transform.position, eaglePosition) < .05f;
            }
            yield return null;
        }

        attackingPlayer = false;
    }
    
}
