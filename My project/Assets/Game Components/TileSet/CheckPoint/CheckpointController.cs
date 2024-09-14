using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private SpriteRenderer checkpointSpriteRenderer;

    public Sprite checkpointOn, checkpointOff;

    public Vector3 playerSpawnPoint = new Vector3(0, -1, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointEntityController.Instance.deactivateCheckpoints();
            
            checkpointSpriteRenderer.sprite = checkpointOn;
            CheckpointEntityController.Instance.SetPlayerSpawnPoint(transform.position);
            
        }
    }

    public void ResetCheckpoint()
    {
        checkpointSpriteRenderer.sprite = checkpointOff;
    }


}
