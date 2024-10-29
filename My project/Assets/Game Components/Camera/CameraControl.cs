using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraFocusTarget, farBackground, middleBackground;

    public float parallaxSpeed, minHeight, maxHeight;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cameraFocusTarget.position.x, Mathf.Clamp(cameraFocusTarget.position.y, minHeight, maxHeight), transform.position.z);

        
        Vector3 middleBacgrkoundTargetPosition = new Vector3(transform.position.x, transform.position.y, middleBackground.position.z);

        farBackground.position = new Vector3(transform.position.x, transform.position.y, farBackground.position.z);
        middleBackground.position = Vector3.Lerp(middleBackground.position, middleBacgrkoundTargetPosition, parallaxSpeed * Time.deltaTime);
    }
}
