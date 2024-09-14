using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;
    
    public int currentHealth, maxHealth;

    public float invinciblePeriod;

    private float invincibleCounter;

    private SpriteRenderer playerSpriteRenderer;

    public GameObject DeathEffect;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
     
            if (invincibleCounter <= 0)
            {
                playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g,
                    playerSpriteRenderer.color.b, 1f );
            }
        }
    }
    
    public void DamagePlayer(int damageAmount = 1)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Instantiate(DeathEffect, transform.position, transform.rotation);
                LevelManager.Instance.RespawnPlayer();
            }
            invincibleCounter = invinciblePeriod;
            playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g,
                playerSpriteRenderer.color.b, 0.5f );
            PlayerController.Instance.KnockBack();
        }
        UIController.Instance.UpdateHealthDisplay();
    }
}
