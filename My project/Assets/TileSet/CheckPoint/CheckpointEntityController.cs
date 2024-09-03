using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEntityController : MonoBehaviour
{
    public static CheckpointEntityController Instance;

    public GameObject[] checkpointArray;
    
    public Vector3 playerSpawnPoint = new Vector3(0, -1, 0);

    void Start()
    {
        Instance = this;
        checkpointArray = new GameObject[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            checkpointArray[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deactivateCheckpoints()
    {
        foreach (GameObject checkpoint in checkpointArray)
        {
            CheckpointController checkpointController = checkpoint.GetComponent<CheckpointController>();

            if (checkpointController == null)
            {
                Debug.LogError("Checkpoint Controller has been asigned null");
            }
            checkpointController.ResetCheckpoint();
        }
    }
    
    public void SetPlayerSpawnPoint(Vector3 newSpawnPoint)
    {
        playerSpawnPoint = newSpawnPoint;
    }
}
