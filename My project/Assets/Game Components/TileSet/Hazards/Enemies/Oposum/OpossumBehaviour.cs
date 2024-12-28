using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

enum AttackType
{
    basicTackleAttack = 1,
    throwTrashAttack = 2,
}

public class EnemyPossumBehaviour : MonoBehaviour
{
    public float opossumSpeed;
    public float originalAttackPeriod;
    public int breakTest;
    public int[] attackTypeRatio = {33, 33, 33, 0};
    public GameObject triggerBoxCollider;
    public GameObject physicsBoxCollider;
    public GameObject spriteRenderer;
    public opossumProjectileHandler projectileHolder;
    
    private float timerAttackRest;
    private float attackPeriod;
    private bool isAttacking;
    private RaycastHit2D tackleRushTarget;
    private Rigidbody2D opossumRigidyBody;
    private BoxCollider2D opossumTrigger;
    private SpriteRenderer opossumSpriteRenderer;
    
    void Start()
    {
        opossumRigidyBody = GetComponent<Rigidbody2D>();
        opossumTrigger = triggerBoxCollider.GetComponent<BoxCollider2D>();
        opossumSpriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        timerAttackRest = 0f;
        attackPeriod = originalAttackPeriod;
        triggerBoxCollider.SetActive(false);
    }

    void Update()
    {
        if ((calculateDirectionOfAttack().x >= 0 && !opossumSpriteRenderer.flipX) ||
            (calculateDirectionOfAttack().x <= 0 && opossumSpriteRenderer.flipX))
        {
            toggleFlippedSprite();
        }
            
        
    }

    void FixedUpdate()
    {
        timerAttackRest += Time.deltaTime;
        
        if (timerAttackRest >= attackPeriod)
        {
            int attackType = 1;
            // int attackType = Random.Range(0, 4);
            Debug.Log(attackType);
            switch (attackType)
            {
                // case (int)AttackType.basicTackleAttack:
                // {
                //     StartCoroutine(basicTackleAttack());
                //     attackPeriod += attackPeriod / 2;
                //     break;
                // }
                // case (int)AttackType.throwTrashAttack:
                // {
                //     projectileHolder.throwThreeProjectiles(calculateDirectionOfAttack());
                //     attackPeriod += attackPeriod / 2;
                //     break;
                // }
                default:
                {
                    attackPeriod = originalAttackPeriod;
                    Debug.Log("hitting the griddy");
                    // goofy dance
                    break;
                }
            }
            timerAttackRest = 0f;
        }
    }

    private IEnumerator basicTackleAttack()
    {
        triggerBoxCollider.SetActive(true);
        float attackTargetLocation = PlayerController.Instance.transform.position.x;
            
        bool reachedPlayerPosition = false;
        
        opossumRigidyBody.velocity = calculateDirectionOfAttack().normalized * opossumSpeed;
        
        while (!reachedPlayerPosition)
        {
            reachedPlayerPosition = Math.Abs(attackTargetLocation - transform.position.x) <= 1f;
            yield return null;
        }

        opossumRigidyBody.drag = breakTest;
        yield return new WaitUntil(() => Mathf.Abs(opossumRigidyBody.velocity.x) < 0.1f);
        opossumRigidyBody.drag = 0f;
        triggerBoxCollider.SetActive(false);
        //play the slow down animation
        //check if running into a wall/pit
    }

    // private int decideAttackType()
    // {
    //     int chancePull = Random.Range(0, 100);
    //     
    // }

    private Vector2 calculateDirectionOfAttack()
    {
        return new Vector2(PlayerController.Instance.transform.position.x - transform.position.x, transform.position.y);
    }

    private void toggleFlippedSprite()
    {
        opossumSpriteRenderer.flipX = !opossumSpriteRenderer.flipX;
        opossumTrigger.offset = new Vector2(opossumTrigger.offset.x * -1, opossumTrigger.offset.y);
    }

}
