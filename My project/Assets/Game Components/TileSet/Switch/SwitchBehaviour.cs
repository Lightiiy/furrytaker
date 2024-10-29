using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchBehaviour : MonoBehaviour
{
    public Sprite switchStateOn;
    public Sprite switchStateOff;
    public UnityEvent switchFunctionTrigger;

    private bool switchState = false;
    private SpriteRenderer switchSpriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        switchSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (switchState)
        {
            case true:
                switchSpriteRenderer.sprite = switchStateOn;
                break;
            case false:
                switchSpriteRenderer.sprite = switchStateOff;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switchFunctionTrigger.Invoke();
            switchState = !switchState;
        }
    }


    public void SwitchTriggerFunctionTest()
    {
        Debug.Log("the switch has been triggered" + switchState);
    }
}
