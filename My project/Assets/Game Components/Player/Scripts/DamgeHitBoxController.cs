using System;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(other.tag);
        if (other.CompareTag("BossEnemy"))
        {
            BossHealthController bossHealthController = other.gameObject.GetComponent<BossHealthController>();
            bossHealthController.dealDamageToBoss(1);
        }
        //compare tag Bosses logic
    }
}
