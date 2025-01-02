using System;
using System.Collections;
using Game.Enemies;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

enum AttackType
{
    basicTackleAttack = 1,
    throwTrashAttack = 2,
}

public class EnemyPossumBehaviour : MonoBehaviour, IEnemy
{
    [Header("OpossumMovement")]
    public float opossumSpeed;
    
    [Header("OpossumAttack")]
    public float originalAttackPeriod;
    private float timerAttackRest;
    private float attackPeriod;
    private bool isAttacking;
    public int slowDownAfterTackle;
    public float testPlayerSearchRange;

    [Header("OpossumGameObjectComponents")]
    private Rigidbody2D opossumRigidyBody;
    public GameObject triggerBoxCollider;
    private BoxCollider2D opossumTrigger;
    public GameObject spriteRenderer;
    private SpriteRenderer opossumSpriteRenderer;
    public opossumProjectileHandler projectileHolder;
    private BossHealthController healthController;
    private ForceRebound forceReboundController;
    private Animator opossumAnimator;
    public GameObject UIHealthCanvas;
    private CanvasRenderer HealthCanvasRenderer;
    
    [Header("WIP")]
    public int[] attackTypeRatio = {33, 33, 33, 0};
    public GameObject physicsBoxCollider;
    
    void Awake()
    {
        opossumRigidyBody = GetComponent<Rigidbody2D>();
        opossumTrigger = triggerBoxCollider.GetComponent<BoxCollider2D>();
        opossumSpriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        healthController = GetComponent<BossHealthController>();
        forceReboundController = GetComponent<ForceRebound>();
        opossumAnimator = GetComponent<Animator>();
        HealthCanvasRenderer = UIHealthCanvas.GetComponent<CanvasRenderer>();
        timerAttackRest = 0f;
        attackPeriod = originalAttackPeriod;
        triggerBoxCollider.SetActive(false);
    }

    void Update()
    {
        
        HealthCanvasRenderer.SetAlpha( isPlayerInRange() ? 1 : 0);
        if ((calculateDirectionOfAttack().x >= 0 && !opossumSpriteRenderer.flipX) ||
            (calculateDirectionOfAttack().x <= 0 && opossumSpriteRenderer.flipX))
        {
            toggleFlippedSprite();
        }
        
        opossumAnimator.SetFloat("OpossumHorizontalSpeed", Mathf.Abs(opossumRigidyBody.velocity.x));
        
        if (healthController.isInvurnable)
        {
            opossumSpriteRenderer.color = new Color(opossumSpriteRenderer.color.r, opossumSpriteRenderer.color.g,
                opossumSpriteRenderer.color.b, 0.5f  );
        }
        else
        {
            opossumSpriteRenderer.color = new Color(opossumSpriteRenderer.color.r, opossumSpriteRenderer.color.g,
                opossumSpriteRenderer.color.b, 1f  );
        }
        
    }

    void FixedUpdate()
    {

        timerAttackRest += Time.deltaTime;
        
        if (timerAttackRest >= attackPeriod && isPlayerInRange())
        {
            int attackType = Random.Range(0, 4);
            switch (attackType)
            {
                case (int)AttackType.basicTackleAttack:
                {
                    StartCoroutine(basicTackleAttack());
                    attackPeriod += attackPeriod / 2;
                    break;
                }
                case (int)AttackType.throwTrashAttack:
                {
                    opossumAnimator.SetTrigger("OpossumThrowTrigger");
                    projectileHolder.throwThreeProjectiles(calculateDirectionOfAttack());
                    attackPeriod += attackPeriod / 2;
                    break;
                }
                default:
                {
                    attackPeriod = originalAttackPeriod;
                    opossumAnimator.SetTrigger("OpossumGriddyTrigger");
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
        opossumAnimator.SetBool("OpossumBreaking", reachedPlayerPosition);

        opossumRigidyBody.drag = slowDownAfterTackle;
        yield return new WaitUntil(() => Mathf.Abs(opossumRigidyBody.velocity.x) < 0.1f);
        opossumAnimator.SetBool("OpossumBreaking", !reachedPlayerPosition);
        opossumRigidyBody.drag = 0f;
        triggerBoxCollider.SetActive(false);
        //play the slow down animation
        //check if running into a wall/pit
    }
    
    private Vector2 calculateDirectionOfAttack()
    {
        return new Vector2(PlayerController.Instance.transform.position.x - transform.position.x, transform.position.y);
    }

    private void toggleFlippedSprite()
    {
        opossumSpriteRenderer.flipX = !opossumSpriteRenderer.flipX;
        opossumTrigger.offset = new Vector2(opossumTrigger.offset.x * -1, opossumTrigger.offset.y);
    }

    private bool isPlayerInRange()
    { 
        bool testValue =Physics2D.OverlapCircle(transform.position, testPlayerSearchRange, LayerMask.NameToLayer("Player"));
        return testValue;
    }

    public void DealDamage(int damage)
    {
        healthController.dealDamageToBoss(damage);
        attackPeriod = originalAttackPeriod;
    }

    public IEnumerator HandleCollisionRebound(Vector2 collisionDirection)
    {
        float originalGravity = opossumRigidyBody.gravityScale;
        opossumRigidyBody.gravityScale = originalGravity * 5;
        forceReboundController.ApplyReboundForce(opossumRigidyBody, collisionDirection, opossumSpeed);
        opossumAnimator.SetBool("OpossumInvurnable", true);
    
        yield return new WaitUntil(() =>
        {
            bool opossumForceReboundApplied = opossumRigidyBody.velocity.magnitude <= 0.1f;
            return opossumForceReboundApplied || !healthController.isInvurnable;
        });
        opossumRigidyBody.velocity = new Vector2(0, 0);
        opossumRigidyBody.gravityScale = originalGravity;
        opossumAnimator.SetBool("OpossumInvurnable", false);
    }
    
    
}
