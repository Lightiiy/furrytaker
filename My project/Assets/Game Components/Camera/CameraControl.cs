using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraFocusTarget, farBackground;
    public Transform[] middleBackground; 

    public float paralaxSpeed, heightOffset;

    private float segmentWidth;
    private Vector3 lastCameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastCameraPosition = cameraFocusTarget.position;
        if (middleBackground.Length > 0)
        {
            SpriteRenderer segmentSpriteRenderer = middleBackground[0].GetComponent<SpriteRenderer>();
            if (segmentSpriteRenderer != null)
            {
                segmentWidth = segmentSpriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.Log("Error while getting component: Sprite Renderer for middleBackground Segment");
            }
        }
        else
        {
            Debug.Log("Error while getting reference to: middleBackground segment");
        }
    }

    // Update is called once per frame
    void Update()
    {
        farBackground.position = new Vector3(transform.position.x, transform.position.y, farBackground.position.z);

        foreach (var segment in middleBackground)
        {
            
            
            float segmentYPosition = (cameraFocusTarget.position.y - lastCameraPosition.y) * paralaxSpeed;

            float segmentXPosition = (cameraFocusTarget.position.x - lastCameraPosition.x) * paralaxSpeed;
            
            segment.position = new Vector3(segment.position.x + segmentXPosition,segment.position.y + segmentYPosition, segment.position.z);
            
            CheckAndLoopSegments(segment);
        }
        

        transform.position = new Vector3(cameraFocusTarget.position.x, cameraFocusTarget.position.y, transform.position.z);
        lastCameraPosition = cameraFocusTarget.position;
    }


    private void CheckAndLoopSegments(Transform segment)
    {
        float halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float leftBound = cameraFocusTarget.position.x - halfScreenWidth;
        float rightBound = cameraFocusTarget.position.x + halfScreenWidth;

        float segmentMargin = segmentWidth * 0.5f;

        float cameraMovementDirection = cameraFocusTarget.position.x - lastCameraPosition.x;

        if (segment.position.x + segmentWidth / 2 < leftBound - segmentMargin && cameraMovementDirection > 0.1f)
        {
            MoveSegmentToRight(segment);
        }
        else if (segment.position.x - segmentWidth / 2 > rightBound + segmentMargin && cameraMovementDirection < -0.1f)
        {
            MoveSegmentToLeft(segment);
        }
    }

    private void MoveSegmentToRight(Transform segment)
    {
        float maxX = float.MinValue;
        foreach (Transform seg in middleBackground)
        {
            if (seg.position.x > maxX)
            {
                maxX = seg.position.x;
            }
        }

        segment.position = new Vector3(maxX + segmentWidth, segment.position.y, segment.position.z);

    }

    private void MoveSegmentToLeft(Transform segment)
    {
        float minX = float.MaxValue;
        foreach (Transform seg in middleBackground)
        {
            if (seg.position.x < minX)
            {
                minX = seg.position.x;
            }
        }

        segment.position = new Vector3(minX - segmentWidth, segment.position.y, segment.position.z);

    }

}
