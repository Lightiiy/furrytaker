using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 20f;
    
    private Animator bouncePadAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        bouncePadAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 playerVelocity = PlayerController.Instance.playerRigidbody.velocity;
            PlayerController.Instance.playerRigidbody.velocity = new Vector2(playerVelocity.x, bounceForce);
            
            this.bouncePadAnimator.SetTrigger("EntityBounce");
        }
    }
}
