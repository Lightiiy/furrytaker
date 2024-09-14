using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CollectibleTypes
{
    Gem,
    Health
}

public class CollectibleController : MonoBehaviour
{
    public CollectibleTypes type;
    
    private Boolean isCollected = false;

    private Animator CollectibleAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        CollectibleAnimator = GetComponent<Animator>();
        CollectibleAnimator.SetInteger("CollectibleType",(int)type);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            HandlePickupType(type);
        }
        
    }

    private void HandlePickupType(CollectibleTypes type)
    {
        switch (type)
        {
            case CollectibleTypes.Gem:
                LevelManager.Instance.PlayerScore++;
                UIController.Instance.UpdateGemCounter();
                break;
            case CollectibleTypes.Health:
                if (PlayerHealthController.Instance.currentHealth >= PlayerHealthController.Instance.maxHealth)
                {
                    return;
                }
                PlayerHealthController.Instance.currentHealth++;
                UIController.Instance.UpdateHealthDisplay();
                break;

        }
        isCollected = true;
        CollectibleAnimator.SetTrigger("CollectiblePickedUp");
        gameObject.layer = LayerMask.NameToLayer("Effects");
    }


    private void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
