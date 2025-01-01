using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemies;
using Unity.VisualScripting;
using UnityEngine;

public class DamgeHitBoxController : MonoBehaviour
{
    public GameObject deathEffect;    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            other.transform.parent.GameObject().SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            Vector2 direction = PlayerController.Instance.playerRigidbody.velocity.normalized;
            PlayerController.Instance.reboundForce(new Vector2(direction.x, direction.y * -1));
        }

        if (other.CompareTag("BossEnemy"))
        {
            IEnemy bossEnemy = other.GetComponent<IEnemy>();
            if (bossEnemy != null)
            {
                Vector2 direction = PlayerController.Instance.playerRigidbody.velocity.normalized;
                float bossReboundDirection = direction.x >= 0 ? 1 : -1;
                PlayerController.Instance.reboundForce(new Vector2(bossReboundDirection * -1, direction.y * -1));
                
                bossEnemy.DealDamage(1);
                StartCoroutine(bossEnemy.HandleCollisionRebound(new Vector2(bossReboundDirection, direction.y * -1)));
            }

            // BossHealthController bossHealthController = other.gameObject.GetComponent<BossHealthController>();
            // bossHealthController.dealDamageToBoss(1);
            // Vector2 directionPlayer = PlayerController.Instance.playerRigidbody.velocity.normalized;
            // Vector2 playerReboundForce = new Vector2(directionPlayer.x * -1, directionPlayer.y * -1);
            //
            // Rigidbody2D bossRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            //
            // Vector2 directionBoss = bossRigidbody2D.velocity.normalized;
            // Vector2 bossReboundForce = new Vector2(directionBoss.x * -1, directionBoss.y * -1);
            //
            // PlayerController.Instance.reboundForce(playerReboundForce);
            //
        }
        //compare tag Bosses logic
    }
}
