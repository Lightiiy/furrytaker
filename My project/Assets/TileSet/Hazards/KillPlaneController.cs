using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlaneController : MonoBehaviour
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
        Debug.Log("Trigger event");
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.RespawnPlayer();
        }
    }
}
