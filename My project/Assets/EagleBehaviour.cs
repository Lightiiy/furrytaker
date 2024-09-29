using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleBehaviour : MonoBehaviour
{

    public GameObject eagleTriggerCollider;
    public GameObject eagleAttackRange;
    public float eagleAttackRadius;
    public float timeBetweenEnemyAttacks;
    public float attackSpeed;
    

    private CircleCollider2D triggerCollider;
    private Transform attackRange;
    private float timer;
    private bool isPlayerInRange;
    private bool attackingPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = eagleTriggerCollider.GetComponent<CircleCollider2D>();
        attackRange = eagleAttackRange.transform;
        
        timer = timeBetweenEnemyAttacks;
        attackingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackingPlayer)
        {
            return;
        }
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        Debug.Log("Update function");
        
        LayerMask playerMask = LayerMask.GetMask("Player");
        isPlayerInRange = Physics2D.OverlapCircle(attackRange.position, eagleAttackRadius, playerMask);

        if (isPlayerInRange)
        {
            StartCoroutine(attackPlayer());
            isPlayerInRange = false;
            attackingPlayer = true;
            timer = timeBetweenEnemyAttacks;
        }

    }

    private IEnumerator attackPlayer()
    {
        Vector2 playerPosition = new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y);
        Vector2 eaglePosition = new Vector2(transform.position.x,transform.position.y);
        bool reachedPlayerPosition = false;
        bool cameBackToOrigin = false;

        while (!reachedPlayerPosition && !cameBackToOrigin)
        {
            if (!reachedPlayerPosition)
            {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, attackSpeed * Time.deltaTime);
            reachedPlayerPosition = Vector2.Distance(transform.position, playerPosition) < .05f;
            }
            
            if (reachedPlayerPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, eaglePosition, attackSpeed * Time.deltaTime);
                cameBackToOrigin = Vector2.Distance(transform.position, eaglePosition) < .05f;
            }

        }

        attackingPlayer = false;
        yield return null;
    }
    
}
