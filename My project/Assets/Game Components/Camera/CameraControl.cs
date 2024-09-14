using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraFocusTarget, farBackground, middleBackground;

    public float minHeight, maxHeight;

    private Vector2 lastPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(cameraFocusTarget.position.x, Mathf.Clamp(cameraFocusTarget.position.y, minHeight, maxHeight), transform.position.z);
        

        farBackground.position = new Vector3(transform.position.x, farBackground.position.y, farBackground.position.z);
        middleBackground.position += new Vector3((transform.position.x - lastPosition.x) * 0.5f, (transform.position.y - lastPosition.y) * 0.1f, 0f);

        lastPosition = transform.position;
    }
}
