using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossumProjectileBehaviour : MonoBehaviour
{

    public SpriteRenderer projectileSpriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "Player":
                
                PlayerHealthController.Instance.DamagePlayer();
                goto case "Ground";
            case "Ground":
            
                this.gameObject.SetActive(false);
                break;
        }
        
    }
}
