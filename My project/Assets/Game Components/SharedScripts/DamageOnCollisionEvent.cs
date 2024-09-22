using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollisionEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.Instance.DamagePlayer();

            UIController.Instance.UpdateHealthDisplay();
            
        }
    }
}
